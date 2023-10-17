using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SSH_InvenUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    //캔버스
    public Canvas canvas;

    //인벤오브젝트
    public GameObject inven;

    //인벤스크립트
    public SSH_Inventory invenScript;

    //그래픽레이
    GraphicRaycaster graphicRay;

    //마우스이벤트데이터
    PointerEventData pad;

    //선택 슬롯
    SSH_Slot slot;

    //선택 슬롯 인덱스
    int uiSlotIndex;

    /// <summary>
    /// 슬롯 이미지 배열
    /// </summary>
    public SSH_Slot[] slotImg;


    //선택 슬롯 이미지 트랜스폼
    Transform slotTrans;

    //슬롯 처음 위치
    Vector3 startPosSlot;

    //슬롯 마지막 위치
    Vector3 lastPosSlot;

    //레이 결과값
    List<RaycastResult> rayResults = new List<RaycastResult>();

    // Start is called before the first frame update
    void Start()
    {
        graphicRay = canvas.GetComponent<GraphicRaycaster>();
        pad = new PointerEventData(null);
    }

    //클릭시 호출함수
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        rayResults.Clear();
        graphicRay.Raycast(pad, rayResults);

        if (rayResults.Count > 0 && rayResults.Count != 0)
        {
            slot = rayResults[0].gameObject.GetComponent<SSH_Slot>();
            uiSlotIndex = slot.transform.GetSiblingIndex();
            slotTrans = slot.rectTransform.transform;
            startPosSlot = slotTrans.position;
            slot.transform.SetAsLastSibling();
        }
    }

    //드래그시 호출 함수
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (slot != null)
        {
            slotTrans.position = pad.position;
        }
    }

    //클릭 종료시 호출 함수
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (rayResults.Count >= 0)
        {
            rayResults.Clear();

            graphicRay.Raycast(pad, rayResults);

            //아이템 버리기
            if(rayResults.Count == 1)
            {
                RemoveItem();
            }

            //다른 슬롯 위에서 종료될 시
            if (rayResults.Count > 1)
            {
                SSH_Slot lastSlot = rayResults[1].gameObject.GetComponent<SSH_Slot>();

                SwapItem(slot, lastSlot);
            }
        }

        if (slot != null)
        {
            slot.transform.position = startPosSlot;
            slot.transform.SetSiblingIndex(uiSlotIndex);
        }


        //초기화
        slot = null;
        slotTrans = null;
        rayResults.Clear();
    }

    /// <summary>
    /// 인벤토리 아이템 추가
    /// </summary>
    public void AddItem(GameObject item)
    {
        invenScript.AddItemData(item);
    }

    //아이템 스왑
    void SwapItem(SSH_Slot first, SSH_Slot last)
    {
        if (first != last)
        {
            first.SwapItemSlot(last);
            invenScript.SwapItemIndex(first.slotIndex, last.slotIndex);
        }
    }

    /// <summary>
    /// 인벤토리 온오프 함수
    /// </summary>
    void OnOffInven()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inven.activeSelf)
            {
                Debug.Log("인벤 닫음");
                inven.SetActive(false);
            }
            else
            {
                Debug.Log("인벤 열음");
                inven.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 인벤토리 아이템 사전 업데이트(정보 초기화)
    /// </summary>
    /// <param name="index"> 인벤토리 아이템 사전 키 값 </param>
    public void SetItem(int index, GameObject item)
    {
        slotImg[index].itemImg.sprite = item.GetComponent<SpriteRenderer>().sprite;
        slotImg[index].slotIndex = index;
    }

    /// <summary>
    /// 아이템 이미지 제거
    /// </summary>
    public void RemoveItem()
    {
        slot.RemoveItem();
        invenScript.RemoveIndex(slot.slotIndex);
    }

    /// <summary>
    /// 인벤토리 아이템 업데이트
    /// </summary>
    void UpdateInven()
    {
    }



    void Update()
    {
        //마우스 위치 업데이트
        pad.position = Input.mousePosition;

        OnOffInven();

        Debug.Log("슬롯 카운트 : " + rayResults.Count);
    }

}