using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreemoneyMaterial : MonoBehaviour
{
    public Slot weaponSlot;
    public Slot fireGemSlot;
    public Slot iceGemSlot;
    public Slot thunderGemSlot;
    public Slot poisonGemSlot;

    public Button doitBtn;

    // Start is called before the first frame update
    void Start()
    {
        ButtonDisable();
    }

    // Update is called once per frame
    void Update()
    {
        CurrectGems();
    }

    void ButtonDisable()
    {
        doitBtn.enabled = false;
    }

    void ButtonEnable()
    {
        doitBtn.enabled = true;
    }

    void CurrectGems()
    {
        if (weaponSlot.item != null &&
            fireGemSlot.item.address == 300 && iceGemSlot.item.address == 301 &&
            thunderGemSlot.item.address == 302 && poisonGemSlot.item.address == 303)
        {
            ButtonEnable();
        }
        else
        {
            ButtonDisable();
        }
    }

    public void MaterialUp()
    {
        EquipData weapon = weaponSlot.item as EquipData;

        int rand = Random.Range(0, 4);

        if(rand == 0)
        {
            weapon.fire += 20;
        }
        else if(rand == 1)
        {
            weapon.water += 20;
        }
        else if(rand == 2)
        {
            weapon.light += 20;
        }
        else
        {
            weapon.dark += 20;
        }

        fireGemSlot.RemoveItem();
        iceGemSlot.RemoveItem();
        thunderGemSlot.RemoveItem();
        poisonGemSlot.RemoveItem();
    }
}
