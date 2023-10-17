using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EtcItemData", menuName = "Item/Etc", order = 3)]

public class EtcItemData : ItemData
{
    //강화값
    public int value;

    public override ItemData CreateItemData()
    {
        EtcItemData etcItemData = CreateInstance<EtcItemData>();

        etcItemData.value = this.value;

        var itemData = etcItemData as ItemData;

        itemData.itemName = base.itemName;
        itemData.address = base.address;
        itemData.itemType = base.itemType;
        itemData.itemSprite = base.itemSprite;
        itemData.itemInfo = base.itemInfo;


        return itemData;
    }
}
