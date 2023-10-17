using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSlot : Slot
{
    //장비창
    public EquipInven equipInven;

    public override void AddItemData(ItemData item)
    {
        base.AddItemData(item);
        equipInven.EquipArmor(item);
    }

    public override void SlotUpdate()
    {
        if (item != null)
        {

        }
        else
        {
            equipInven.DisarmArmor();
        }
    }
}
