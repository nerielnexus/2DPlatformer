using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InvenUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerExitHandler
{
    //골드
    public int golds;

    //인벤토리 캔버스
    public Canvas canvas;

    //인벤토리 오브젝트
    public GameObject inven;
    //장비창 오브젝트
    public GameObject equipUI;
    //슬롯UI창
    public SlotUI slotUI;

    //인벤토리 슬롯리스트
    public Slot[] slotList;


    //인벤토리UI 시작 위치
    Vector2 startInvenPos;

    //장비창UI 시작 위치
    Vector2 startEquipPos;

    //인벤토리UI 이동 위치
    Vector2 dragInvenPos;



    //클릭한 슬롯
    Slot clickSlot;

    //클릭 슬롯 ui위치 인덱스
    int uiSlotIndex;

    //클릭 슬롯 ui 부모 트랜스폼
    Transform slotParent;

    //클릭 슬롯 처음 위치
    Vector2 firstSlotPos;

    //클릭 슬롯 위치
    Vector2 moveSlotPos;

    //드래그 확인
    bool onDrag = false;


    //그래픽레이캐스터
    GraphicRaycaster graphicRay;

    //레이 결과값
    List<RaycastResult> rayResults = new List<RaycastResult>();

    public static InvenUI invenUI;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        // 2022/12/29 김현석 수정(오브젝트가 2개 생성되는 오류 수정)
        var obj = FindObjectsOfType<InvenUI>();

        if (obj.Length == 1)
        {
            invenUI = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        graphicRay = canvas.GetComponent<GraphicRaycaster>();
    }

    /// <summary>
    /// 인벤토리 아이템 추가
    /// </summary>
    public void AddItem(GameObject item)
    {
        ItemData itemData;
        if(item.GetComponent<Item>())
        {
            itemData = item.GetComponent<Item>().itemData;
        }
        else if (item.GetComponent<DropItem>())
        {
            itemData = item.GetComponent<DropItem>().itemData;
        }
        else
        {
            itemData = null;
        }

        Sprite itemImage = itemData.itemSprite;

        int slotIndex = NullSlot();

        if(slotIndex == -1)
        {
            Debug.Log("빈슬롯 존재 X");
            return;
        }
        else
        {
            SetItem(slotIndex, itemData);
        }

        //아이템이 없는 슬롯 인덱스 찾기 메소드
        int NullSlot()
        {
            for (int i = 0; i < slotList.Length; i++)
            {
                if (slotList[i].itemOn != true)
                {
                    return i;
                }
            }
            return -1;
        }
    }


    /// <summary>
    /// 인벤토리 아이템(이미지, 데이터) 셋팅
    /// </summary>
    /// <param name="index"></param>
    /// <param name="itemImage"></param>
    /// <param name="itemData"></param>
    public void SetItem(int index, ItemData itemData)
    {
        slotList[index].FirstAddItemData(itemData);
    }


    /// <summary>
    /// 인벤토리 온오프
    /// </summary>
    void OnOffUI()
    {
        //인벤토리
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inven.activeSelf)
            {
                Debug.Log("인벤 닫음");
                inven.SetActive(false);
                ItemInfoUI.itemInfoUI.RemoveItemInfo();
            }
            else
            {
                Debug.Log("인벤 열음");
                inven.SetActive(true);
            }
        }
        //장비창
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(equipUI.activeSelf)
            {
                Debug.Log("장비창 닫음");
                equipUI.SetActive(false);
                ItemInfoUI.itemInfoUI.RemoveItemInfo();
            }
            else
            {
                Debug.Log("장비창 열음");
                equipUI.SetActive(true);
            }
        }
    }



    /// <summary>
    /// 클릭시작
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        startInvenPos = inven.transform.position;
        dragInvenPos = eventData.position;

        startEquipPos = equipUI.transform.position;
        dragInvenPos = eventData.position;

        onDrag = false;

        rayResults.Clear();

        graphicRay.Raycast(eventData, rayResults);

        //좌클릭
        if(Input.GetMouseButtonDown(0))
        {
            //슬롯UI창 제거
            slotUI.RemoveUI();

            //슬롯 클릭의 시작 -> 아이템이 존재시에만 작동하도록 변경예정(test중이니 그냥 작동)
            if (rayResults[0].gameObject.layer == LayerMask.NameToLayer("Slot"))    // ==> ray결과 : Slot layer일 경우 슬롯스크립트 가져옴
            {
                clickSlot = rayResults[0].gameObject.GetComponent<Slot>();

                if (clickSlot.itemOn && !clickSlot.dontUse)
                {
                    //UI 관련
                    slotParent = clickSlot.transform.parent;

                    clickSlot.transform.SetParent(canvas.transform);

                    uiSlotIndex = clickSlot.transform.GetSiblingIndex();

                    clickSlot.transform.SetAsLastSibling();

                    firstSlotPos = clickSlot.itemRect.position;
                    moveSlotPos = eventData.position;
                }
            }
        }

        //우클릭
        if (Input.GetMouseButtonDown(1) && !onDrag)
        {
            rayResults.Clear();
            graphicRay.Raycast(eventData, rayResults);

            //슬롯검사
            if (rayResults[0].gameObject.layer == LayerMask.NameToLayer("Slot"))
            {
                Slot slot = rayResults[0].gameObject.GetComponent<Slot>();

                //슬롯아이템
                if (slot.itemOn && !slot.dontUse)
                {
                    slotUI.SetUI(SlotItemType(slot), slot);
                }
            }
        }
    }


    /// <summary>
    /// 드래그중
    /// </summary>
    /// <param name="eventData"></param>
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        onDrag = true;

        //switch문이 효과적 -> 바꿀것
        if (rayResults[0].gameObject.name == "Inventory")    // 현재 이름으로 비교중
        {
            inven.transform.position = startInvenPos + (eventData.position - dragInvenPos);
        }

        if(rayResults[0].gameObject.name == "Equipment")
        {
            equipUI.transform.position = startEquipPos + (eventData.position - dragInvenPos);

        }

        if (rayResults[0].gameObject.layer == LayerMask.NameToLayer("Slot") && clickSlot != null)    
        {
            if(clickSlot.itemOn && !clickSlot.dontUse)
            {
                clickSlot.itemRect.position = firstSlotPos + (eventData.position - moveSlotPos);
            }
        }
    }


    /// <summary>
    /// 클릭 종료
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        onDrag = false;

        if(clickSlot != null && clickSlot.itemOn && !clickSlot.dontUse)
        {
            if (rayResults[0].gameObject.layer == LayerMask.NameToLayer("Slot"))
            {
                clickSlot.itemRect.position = firstSlotPos;
                clickSlot.transform.SetSiblingIndex(uiSlotIndex);

                //레이결과 초기화 및 레이캐스트
                rayResults.Clear();
                graphicRay.Raycast(eventData, rayResults);

                //인벤 바깥일시 이미지 제거
                if (rayResults.Count == 0)
                {
                    if (clickSlot.itemOn)
                    {
                        DumpItem(clickSlot);        // 현재 아이템 버리기
                    }
                }
                //슬롯 이미지,데이터 교체
                else if (rayResults[0].gameObject.layer == LayerMask.NameToLayer("Slot"))
                {
                    Slot changeSlot = rayResults[0].gameObject.GetComponent<Slot>();

                    if(clickSlot != changeSlot && !changeSlot.dontUse)
                    {
                        if (CanChange(clickSlot, changeSlot))
                        {
                            clickSlot.SwapItemSlot(changeSlot);
                            changeSlot.SlotUpdate();
                        }
                    }
                }
            }

            clickSlot.SlotUpdate();
            clickSlot.transform.SetParent(slotParent);

            rayResults.Clear();

            clickSlot = null;
        }
    }

    /// <summary>
    /// 커서 벗어남
    /// </summary>
    /// <param name="CanChange"></param>
    /// <param name=""></param>
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        slotUI.RemoveUI();
    }



    /// <summary>
    /// 슬롯에 들어갈 수 있는 아이템인지 검사
    /// </summary>
    /// <param name="firstSlot"></param>
    /// <param name="lastSlot"></param>
    /// <returns></returns>
    bool CanChange(Slot slotItem, Slot slot)
    {
        if (SlotItemType(slotItem) == SlotType(slot))
        {
            //슬롯의 아이템과 교체할 슬롯의 타입이 동일
            //또는 교체할 슬롯의 타입이 아이템
            //또는 교체할 슬롯의 타입이 장비, 그리고 슬롯의 아이템이 장비 아이템

            return true;
        }
        else if(SlotType(slot) == 0)
        {
            //모든아이템 들어갈 수 있음

            return true;
        }
        else if(SlotType(slot) == 1 && (SlotItemType(slotItem) == 2 || SlotItemType(slotItem) == 3))
        {
            //장비아이템 들어 갈 수 있음

            return true;
        }

        return false;
    }

    /// <summary>
    /// 슬롯 검사
    /// 무기,방어구 등 슬롯에 들어가야할 정보가 맞는지 확인 (아이템 종류가 늘어날 시 증가) 
    /// 
    /// 아이템        : 0          어떤 아이템이든 들어갈 수 있음
    /// 장비          : 1          장비 아이템만
    /// 장비 (무기)   : 2          무기 아이템만
    /// 장비 (방어구) : 3          방어구 아이템만
    /// 기타아이템    : 4          기타 아이템(현재 강화재료만)
    /// 젬 아이템     : 5          젬
    /// 없음(오류)    : -1
    int SlotType(Slot slot)
    {
        string slotType = slot.gameObject.tag;

        switch (slotType)
        {
            case ("Slot"):
                return 0;

            case ("Slot(Equip)"):
                return 1;

            case ("Slot(Waepon)"):
                return 2;

            case ("Slot(Armor)"):
                return 3;

            case ("Slot(Etc)"):
                return 4;

            case ("Slot(Gem)"):
                return 5;
        }

        Debug.Log("오류");
        return -1;
    }




    /// <summary>
    /// 슬롯 아이템 검사 (장비, 사용 아이템 외의 종류가 늘어날 시 추가)
    /// 현재 슬롯의 아이템이 장비인지만 검사
    /// </summary>
    int SlotItemType(Slot slot)
    {
        return slot.item.itemType;
    }


    //퀵슬롯 교체
    public void QuickSlotChange()
    {
        slotList[0].SwapItemSlot(slotList[1]);
    }

    //슬롯 교체
    void SwapItem(Slot slot)
    {
        clickSlot.SwapItemSlot(slot);
    }

    //아이템 버리기
    void DumpItem(Slot slot)
    {
        slot.RemoveItem();

        //임시 플레이어 위치에 아이템 생성
        //Instantiate(slot.item, Player.instance.transform);
    }

    //아이템 사용 (현재 이미지제거만)
    public void UseItem()
    {
        if(slotList[0].itemOn)
        {
            slotList[0].item.UseItem();
            slotList[0].RemoveItem();
        }
    }

   

    private void Update()
    {
        OnOffUI();
    }
}