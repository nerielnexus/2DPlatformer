using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GemItemData", menuName = "Item/Gem", order = 4)]

public class GemItemData : ItemData
{
    //속성값
    public int value;

    public override ItemData CreateItemData()
    {
        GemItemData gemItemData = CreateInstance<GemItemData>();

        gemItemData.value = this.value;

        var itemData = gemItemData as ItemData;

        itemData.itemName = base.itemName;
        itemData.address = base.address;
        itemData.itemType = base.itemType;
        itemData.itemSprite = base.itemSprite;
        itemData.itemInfo = base.itemInfo;


        return itemData;
    }
}
