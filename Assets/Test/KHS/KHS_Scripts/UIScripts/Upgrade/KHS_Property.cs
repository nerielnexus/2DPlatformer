using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_Property : MonoBehaviour
{
    public Slot equip;
    public Slot jem;
    public Slot result;

    public void Property()
    {
        int success = Random.Range(1, 101);

        if(equip.itemOn && jem.itemOn && !result.itemOn)
        {
            if(success <= 50)
            {
                result.AddItemData(UpProperty(equip.item as EquipData, jem.item.address));
            }
            else
            {
                result.AddItemData(equip.item);
            }

            equip.RemoveItem();
            jem.RemoveItem();
        }
    }

    ItemData UpProperty(EquipData equip, int address)
    {
        int value = Random.Range(50, 101);

        if(address == 300)
        {
            equip.fire += value;
        }
        else if(address == 301)
        {
            equip.water += value;
        }
        else if(address == 302)
        {
            equip.light += value;
        }
        else if(address == 303)
        {
            equip.dark += value;
        }
        
        return equip;
    }


}
