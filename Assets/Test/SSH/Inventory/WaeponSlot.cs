using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaeponSlot : Slot
{
    //장비창
    public EquipInven equipInven;

    public override void AddItemData(ItemData item)
    {
        base.AddItemData(item);
        equipInven.EquipWaepon(item);
    }

    public override void SlotUpdate()
    {
        if(item != null)
        {
        }
        else
        {
            equipInven.DisarmWaepon();
        }
    }    
}
