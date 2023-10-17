using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UseItemData", menuName = "Item/Use", order = 2)]
public class UseItemData : ItemData
{
    //최대 소지 수
    public int possession;

    public UseItem item;

    public override ItemData CreateItemData()
    {
        UseItemData useItemData = CreateInstance<UseItemData>();

        useItemData.item = this.item;
        useItemData.possession = this.possession;

        var itemData = useItemData as ItemData;

        itemData.itemName = base.itemName;
        itemData.address = base.address;
        itemData.itemType = base.itemType;
        itemData.itemSprite = base.itemSprite;
        itemData.itemInfo = base.itemInfo;

        return itemData;
    }
 
    
    public override void UseItem()
    {
        Instantiate(item, Player.instance.transform.position, Quaternion.identity);
    }
    
}
