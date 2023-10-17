using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocketSlot : Slot
{
    //소켓인덱스
    public int index;

    //소켓UI
    public SocketUI socketUI;

    EquipData equipData;

    ItemData tempGem;


    //슬롯 아이템 데이터 추가 오버라이드
    public override void AddItemData(ItemData item)
    {
        Color color = itemImage.GetComponent<Image>().color;
        color.a = 255f;
        itemImage.GetComponent<Image>().color = color;

        if (item != null)
        {
            this.item = item;
            tempGem = item;

            SetItemImage(item);

            //소켓장착
            if (socketUI.EquipSlot != null && socketUI.EquipSlot.itemOn && itemOn)
            {
                ApplyGem();
            }
        }
    }

    //슬롯 아이템 데이터 제거 오버라이드
    public override void SlotUpdate()
    {
        if(!itemOn)
        {
            DeleteGem();
            RemoveItem();

            tempGem = null;
        }
        else
        {

        }
    }

    //소켓확인
    public void SocketCheck(ItemData item)
    {
        this.item = item;
        SetItemImage(item);
    }

    //소켓 젬 추가
    void ApplyGem()
    {
        equipData = socketUI.EquipSlot.item as EquipData;

        //소켓확인
        if (equipData.gemSocket == null)
        {
            equipData.gemSocket = new Dictionary<int, GemItemData>();

            equipData.gemSocket.Add(index, item as GemItemData);
        }
        else
        {
            equipData.gemSocket[index] = item as GemItemData;
        }

        CheckGem(item, true, equipData);

        socketUI.EquipSlot.item = equipData;
    }

    //소켓 젬데이터 제거
    void DeleteGem()
    {
        equipData = socketUI.EquipSlot.item as EquipData;

        //소켓확인
        if (equipData.gemSocket == null)
        {

        }
        else
        {
            equipData.gemSocket.Remove(index);
        }

        CheckGem(tempGem, false, equipData);

        socketUI.EquipSlot.item = equipData;
    }

    /// <summary>
    /// 젬 체크 후 장비 속성 강화
    /// </summary>
    /// <param name="item">젬 아이템</param>
    /// <param name="up">속성 강화인지 확인, true = +, false = - </param>
    /// <param name="euqip">강화할 장비 아이템</param>
    void CheckGem(ItemData item, bool up, EquipData equip)
    {
        switch (item.address)
        {
            //불속성 젬
            case 300:
                if(up)
                {
                    equip.fire += 100;
                }
                else
                {
                    equip.fire -= 100;
                }
                break;
            //얼음속성 젬
            case 301:
                if (up)
                {
                    equip.water += 100;
                }
                else
                {
                    equip.water -= 100;
                }
                break;
            //전기속성 젬
            case 302:
                if (up)
                {
                    equip.light += 100;
                }
                else
                {
                    equip.light -= 100;
                }
                break;
            //독속성 젬
            case 303:
                if (up)
                {
                    equip.dark += 100;
                }
                else
                {
                    equip.dark -= 100;
                }
                break;
        }

    }





}
