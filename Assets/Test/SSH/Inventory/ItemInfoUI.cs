using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    //싱글톤
    public static ItemInfoUI itemInfoUI;

    //아이템 정보 창
    public GameObject itemInfoWindow;

    //아이템 이름
    public Text itemName;
    //아이템 타입(장비, 사용, 재료)
    public Text itemType;
    //아이템 밸류타입(공격력, 방어력)
    public Text itemValueType;
    //아이템 밸류
    public Text itemValue;
    //아이템 내구도(장비만)
    public Text itemDurability;
    //아이템 정보
    public Text itemInfo;
    //아이템 속성
    public Text itemEle;

    //아이템 강화수치 (장비만)
    int itemEnforceValue = 0;


    void Start()
    {
        itemInfoUI = this;
    }

    //UI 아이템 셋팅
    public void SetItemInfo(ItemData data, Vector2 pos)
    {
        itemInfoWindow.SetActive(true);
        itemInfoWindow.transform.position = new Vector2(pos.x + 60f, pos.y);

        //위치 조정 스크립트 짤것

        SetItemData(data);
    }

    //UI 아이템 정보 셋팅
    public void SetItemData(ItemData data)
    {
        EquipData equipData = data as EquipData;

        switch (data.itemType)
        {
            case 0:
                this.itemType.text = "사용 아이템";
                this.itemValueType.text = "";
                this.itemValue.text = "";
                this.itemDurability.text = "";

                break;
            case 2:
                this.itemValue.text = equipData.value.ToString();
                this.itemEnforceValue = equipData.enforceValue;
                this.itemDurability.text = "내구도 : " + equipData.durability.ToString();
                this.itemType.text = "무기";
                this.itemValueType.text = "공격력";
                this.itemEle.text = "Fire : " + equipData.fire.ToString() + " " + "Ice : " + equipData.water.ToString() + "\n" + "Elect : " + equipData.light.ToString() + " " + "Pos : " + equipData.dark.ToString();

                break;
            case 3:
                this.itemValue.text = equipData.value.ToString();
                this.itemEnforceValue = equipData.enforceValue;
                this.itemDurability.text = "내구도 : " + equipData.durability.ToString();
                this.itemType.text = "방어구";
                this.itemValueType.text = "방어력";
                this.itemEle.text = "Fire : " + equipData.fire.ToString() + " " + "Ice : " + equipData.water.ToString() + "\n" + "Elect : " + equipData.light.ToString() + " " + "Pos : " + equipData.dark.ToString();

                break;
            case 4:
                EtcItemData etcItemData = data as EtcItemData;
                this.itemValue.text = "";
                this.itemType.text = "기타 아이템";
                break;
            case 5:
                GemItemData gemItemData = data as GemItemData;
                this.itemType.text = "젬";
                break;
            case -1:
                Debug.Log("오류");

                break;
        }

        if(itemEnforceValue != 0)
        {
            this.itemName.text = data.itemName + " +" + itemEnforceValue;
        }
        else
        {
            this.itemName.text = data.itemName;
        }

        this.itemInfo.text = data.itemInfo;
    }

    //정보 제거
    public void RemoveItemInfo()
    {
        itemInfoWindow.SetActive(false);

        itemName.text = null;
        itemType.text = null;
        itemValueType.text = null;
        itemValue.text = null;
        itemInfo.text = null;
        itemEle.text = null;
        itemEnforceValue = 0;
        itemDurability.text = null;
    }
}
