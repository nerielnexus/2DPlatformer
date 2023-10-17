using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//주제객체(Slot)
public interface ISlotUpdate
{
    //슬롯을 가지고 있는 창을 리스트에 등록
    void ResisterSlotWindow(ISlotWindow IslotWindow);

    //슬롯을 가지고 있는 창을 리스트에 해제
    void RemoveSlotWindow(ISlotWindow IslotWindow);

    //슬롯의 아이템 업데이트 전달
    void NotifyItemData(Item item);
}

//옵저버객체(인벤,장비,강화창 등)
public interface ISlotWindow
{
    void UpdateItemData(Item item);
}

public class SlotObserver : MonoBehaviour, ISlotUpdate
{
    public List<ISlotWindow> slotWindows = new List<ISlotWindow>();

    public static SlotObserver slotObserver;

    void Awake()
    {
        slotObserver = this;
    }

    //인벤, 장비, 강화 등 슬롯을 가지고 있는 옵저버 등록
    public void ResisterSlotWindow(ISlotWindow IslotWindow)
    {
        slotWindows.Add(IslotWindow);
    }
    
    //옵저버 해지
    public void RemoveSlotWindow(ISlotWindow IslotWindow)
    {
        slotWindows.Remove(IslotWindow);
    }

    //슬롯이 아이템On 시 아이템 정보를 전달
    public void NotifyItemData(Item item)
    {
        foreach(ISlotWindow IslotWindow in slotWindows)
        {
            IslotWindow.UpdateItemData(item);
            Debug.Log("슬롯 업데이트");
        }
    }
}