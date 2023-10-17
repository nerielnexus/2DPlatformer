using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSH_Slot : MonoBehaviour
{
    //아이템 이미지
    public Image itemImg;

    //슬롯 인덱스
    public int slotIndex;

    //아이템 소지 여부
    public bool ItemOn => itemImg.sprite != null;

    public RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    //슬롯 교환
    public void SwapItemSlot(SSH_Slot otherSlot)
    {
        if (otherSlot == null)
        {
            return;
        }
        if (otherSlot == this)
        {
            return;
        }

        Sprite swapSlot = itemImg.sprite;

        if (otherSlot.ItemOn)
        {
            SetItem(otherSlot.itemImg.sprite);
        }
        else
        {
            RemoveItem();
        }

        otherSlot.SetItem(swapSlot);
    }

    //슬롯 아이팀 이미지 셋팅
    public void SetItem(Sprite itemSprite)
    {
        if (itemSprite != null)
        {
            itemImg.sprite = itemSprite;
        }
        else
        {
            RemoveItem();
        }
    }

    //슬롯 아이템 이미지 제거
    public void RemoveItem()
    {
        itemImg.sprite = null;
    }

}
