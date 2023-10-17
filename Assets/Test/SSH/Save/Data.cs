using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveItemData
{
    // 아이템 주소
    public int address;

    // 아이템 이름
    public string itemName;

    // 아이템 타입
    public int itemType;

    // 아이템 정보
    public string itemInfo;

    // 장비 내구도
    public int durability;

    // 장비 밸류 (공격력, 방어력)
    public int value;

    // 장비 강화 수치
    public int enforceValue;

    // 속성 변수
    public int fire;
    public int water;
    public int light;
    public int dark;

    public List<int> gemData;

    public SaveItemData(ItemData itemData)
    {
        this.address = itemData.address;
        this.itemName = itemData.itemName;
        this.itemType = itemData.itemType;
        this.itemInfo = itemData.itemInfo;
    }


    public SaveItemData(EquipData equipData)
    {
        this.address = equipData.address;
        this.itemName = equipData.itemName;
        this.itemType = equipData.itemType;
        this.itemInfo = equipData.itemInfo;

        this.durability = equipData.durability;
        this.value = equipData.value;
        this.enforceValue = equipData.enforceValue;

        this.fire = equipData.fire;
        this.water = equipData.water;
        this.light = equipData.light;
        this.dark = equipData.dark;

        //소켓 3개
        this.gemData = new List<int>(3);

        if(equipData.gemSocket != null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (equipData.gemSocket.TryGetValue(i, out GemItemData gem))
                {
                    gemData.Add(gem.address);
                }
                else
                {
                    gemData.Add(0);
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                gemData.Add(0);
            }
        }
        

    }

}

[System.Serializable]
public class Data
{
    //인벤장비데이터
    public List<SaveItemData> itemData = new List<SaveItemData>(15);

    public SaveItemData waeponData;
    public SaveItemData armorData;

    // 2022.12.30 김현석 - 추가
    public bool[] stage = new bool[5];
}