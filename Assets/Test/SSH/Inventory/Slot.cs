using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //아이템 이미지
    public Image itemImage;

    //아이템 수량 텍스트
    public Text itemQuantity;

    //아이템 이미지 렉트 트랜스폼
    public RectTransform itemRect;

    //아이템이 들어있는지 여부
    public bool itemOn => itemImage.sprite != null && item != null;

    //저장아이템데이터
    public ItemData item;

    //우클릭여부
    public bool dontUse;

    //목적 new 최소화
    //슬롯으로 최초 한 번 들어올때 new
    //

    //아이템 데이터 최초로 들어올 때 객체 생성
    public void FirstAddItemData(ItemData item)
    {
        if (item != null)
        {
            ItemData itemData = item.CreateItemData();
            this.item = itemData;

            SetItemImage(item);
        }
    }


    //아이템데이터 저장, 이미지 교체
    public virtual void AddItemData(ItemData item)
    {
        if (item != null)
        {
            this.item = item;
            SetItemImage(item);
        }

        SlotUpdate();
    }

    //아이템 이미지 저장
    public void SetItemImage(ItemData item)
    {
        Color color = itemImage.GetComponent<Image>().color;
        color.a = 255f;
        itemImage.GetComponent<Image>().color = color;

        itemImage.sprite = item.itemSprite;
    }

    //슬롯 교환
    public void SwapItemSlot(Slot otherSlot)
    {
        //슬롯이 없거나 같은 슬롯일 경우
        if (otherSlot == null || otherSlot == this)
        {
            return;
        }

        //임시 아이템저장
        ItemData tempSlot = this.item;

        //다른 슬롯에 아이템이 존재할 시 데이터, 이미지 교환
        if (otherSlot.itemOn)
        {
            AddItemData(otherSlot.item);

            // 김현석 추가 - 23/01/09 
            KHS_Enforce.onlyOne = false;
        }
        else
        {
            RemoveItem();
        }

        if(tempSlot != null)
        {
            otherSlot.AddItemData(tempSlot);
        }
        else
        {

        }

        otherSlot.SlotUpdate();

    }


    //슬롯 아이템 제거
    public void RemoveItem()
    {
        itemImage.sprite = null;

        Color color = itemImage.GetComponent<Image>().color;
        color.a = 0f;
        itemImage.GetComponent<Image>().color = color;

        item = null;
    }


    //슬롯 업데이트
    public virtual void SlotUpdate()
    {
        if (!itemOn)
        {
            RemoveItem();
        }
        else
        {

        }
    }

    //아이템 인포 윈도우용
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(itemOn)
        {
            ItemInfoUI.itemInfoUI.SetItemInfo(item, this.transform.position);
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        ItemInfoUI.itemInfoUI.RemoveItemInfo();
    }

}