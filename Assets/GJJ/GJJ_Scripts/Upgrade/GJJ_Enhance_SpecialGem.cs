using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GJJ_UpgradeGemItemData", menuName = "Item/GJJ_UpgradeGem", order = 5)]
public class GJJ_Enhance_SpecialGem : ItemData, System.ICloneable
{
    public int value;
    public int enhanceLevel;

    public object Clone()
    {
        GJJ_Enhance_SpecialGem ret = CreateInstance<GJJ_Enhance_SpecialGem>();

        ret.value = this.value;
        ret.enhanceLevel = this.enhanceLevel;
        ret.itemName = this.itemName;
        ret.address = this.address;
        ret.itemType = this.itemType;
        ret.itemSprite = this.itemSprite;
        ret.itemInfo = this.itemInfo;

        return ret;
    }

    public override ItemData CreateItemData()
    {
        GJJ_Enhance_SpecialGem gjjGemData = CreateInstance<GJJ_Enhance_SpecialGem>();

        gjjGemData.value = this.value;
        gjjGemData.enhanceLevel = this.enhanceLevel;

        var itemData = gjjGemData as ItemData;

        itemData.itemName = base.itemName;
        itemData.address = base.address;
        itemData.itemType = base.itemType;
        itemData.itemSprite = base.itemSprite;
        itemData.itemInfo = base.itemInfo;

        return itemData;
    }
}
