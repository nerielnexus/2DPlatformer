using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SlotUIType
{
    Use,
    Equip,
    Etc,
    Gem
}

public class SlotUI : MonoBehaviour
{
    //슬롯UI
    public GameObject SlotUiWindow;

    //소켓UI
    public SocketUI socketUI;


    //버튼 텍스트
    public Text Btn1;
    public Text Btn2;
    public Text Btn3;

    //슬롯
    Slot slotData;

    //아이템 타입
    int itemType;


    SlotUIType slotUIType;


    /*
     * 아이텝 타입
     * 0 : 사용
     * 1 : 장비
     * 2 : 무기
     * 3 : 방어구
     * 4 : 기타
     */


    public void SetUI(int type, Slot slot)
    {
        SlotUiWindow.SetActive(true);
        slotData = slot;
        itemType = type;

        SlotUiWindow.transform.position = new Vector2(slot.gameObject.transform.position.x + 50.0f, slot.gameObject.transform.position.y);

        switch (type)
        {
            case 0:
                slotUIType = SlotUIType.Use;

                Btn1.text = "사용";
                Btn2.text = "버리기";
                Btn1.gameObject.SetActive(true);
                Btn2.gameObject.SetActive(true);
                Btn3.gameObject.SetActive(false);

                break;

            case 1:
                slotUIType = SlotUIType.Equip;

                Btn1.text = "소켓";
                Btn2.text = "버리기";
                Btn1.gameObject.SetActive(true);
                Btn2.gameObject.SetActive(true);
                Btn3.gameObject.SetActive(false);

                break;

            case 2:
                slotUIType = SlotUIType.Equip;

                Btn1.text = "소켓";
                Btn2.text = "버리기";
                Btn1.gameObject.SetActive(true);
                Btn2.gameObject.SetActive(true);
                Btn3.gameObject.SetActive(false);

                break;

            case 3:
                slotUIType = SlotUIType.Equip;

                Btn1.text = "소켓";
                Btn2.text = "버리기";
                Btn1.gameObject.SetActive(true);
                Btn2.gameObject.SetActive(true);
                Btn3.gameObject.SetActive(false);

                break;

            case 4:
                slotUIType = SlotUIType.Etc;

                Btn1.text = "버리기";
                Btn1.gameObject.SetActive(true);
                Btn2.gameObject.SetActive(false);
                Btn3.gameObject.SetActive(false);

                break;

            case 5:
                slotUIType = SlotUIType.Gem;

                Btn1.text = "버리기";
                Btn1.gameObject.SetActive(true);
                Btn2.gameObject.SetActive(false);
                Btn3.gameObject.SetActive(false);

                break;
        }
    }


    //UI제거
    public void RemoveUI()
    {
        SlotUiWindow.SetActive(false);
        Btn1.text = null;
        Btn2.text = null;
        Btn3.text = null;

        Btn1.gameObject.SetActive(false);
        Btn2.gameObject.SetActive(false);
        Btn3.gameObject.SetActive(false);
    }

    //관련 모든 UI제거
    public void RemoveUIAll()
    {
        socketUI.RemoveUI();
    }

    /// <summary>
    /// 버튼 1 메소드
    /// </summary>
    public void Btn1Method()
    {
        switch(slotUIType)
        {
            case SlotUIType.Use:
                UseItem();
                break;
            case SlotUIType.Equip:
                SocketItem();
                break;
            case SlotUIType.Etc:
            case SlotUIType.Gem:
                DumpItem();
                break;
        }
    }

    /// <summary>
    /// 버튼 2 메소드
    /// </summary>
    public void Btn2Method()
    {
        switch (slotUIType)
        {
            case SlotUIType.Use:
                DumpItem();
                break;
            case SlotUIType.Equip:
                DumpItem();
                break;
            case SlotUIType.Etc:
            case SlotUIType.Gem:
                break;
        }
    }

    /// <summary>
    /// 버튼 3 메소드
    /// </summary>
    public void Btn3Method()
    {
        switch (slotUIType)
        {
            case SlotUIType.Use:
                break;
            case SlotUIType.Equip:
                break;
            case SlotUIType.Etc:
            case SlotUIType.Gem:
                break;
        }
    }


    /// <summary>
    /// 아이템 사용 메소드
    /// </summary>
    void UseItem()
    {
        Debug.Log("아이템사용");
        slotData.item.UseItem();
        slotData.RemoveItem();
        RemoveUI();
    }


    /// <summary>
    /// 소켓 메소드
    /// </summary>
    void SocketItem()
    {
        socketUI.SetUI(slotData);

        RemoveUI();
    }


    /// <summary>
    /// 버리기 메소드
    /// </summary>
    void DumpItem()
    {
        slotData.RemoveItem();

        RemoveUI();
    }


}
