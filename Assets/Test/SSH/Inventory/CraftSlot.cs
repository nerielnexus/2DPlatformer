using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSlot : Slot
{
    public CraftUI craftUI;

    public override void AddItemData(ItemData itemData)
    {
        base.AddItemData(itemData);

        craftUI.ReadyCraft();
    }

    public override void SlotUpdate()
    {
        if (!itemOn)
        {
            RemoveItem();
            craftUI.ReadyCraft();
        }
        else
        {

        }

    }
}
