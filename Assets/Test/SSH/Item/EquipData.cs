using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipData", menuName = "Item/Equip", order = 1)]
public class EquipData : ItemData
{
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

    //소켓
    public Dictionary<int, GemItemData> gemSocket;

    //새로운 장비 데이터 객체 생성용
    public override ItemData CreateItemData()
    {
        EquipData equipData = CreateInstance<EquipData>();

        equipData.durability = this.durability;
        equipData.value = this.value;
        equipData.enforceValue = this.enforceValue;
        equipData.gemSocket = this.gemSocket;
        equipData.fire = this.fire;
        equipData.water = this.water;
        equipData.light = this.light;
        equipData.dark = this.dark;

        var itemData = equipData as ItemData;

        itemData.itemName = base.itemName;
        itemData.address = base.address;
        itemData.itemType = base.itemType;
        itemData.itemSprite = base.itemSprite;
        itemData.itemInfo = base.itemInfo;

        return itemData;
    }

    public ItemData CreateItemData(string name)
    {
        ItemData item = CreateItemData();

        item.itemName = name;


        return item;
    }

    //장비 아이템 장착
    public override void UseItem()
    {
        //장착
    }


}