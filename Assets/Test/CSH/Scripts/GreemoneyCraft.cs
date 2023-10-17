using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreemoneyCraft : MonoBehaviour
{
    public Slot stuffSlot;
    public Slot woodSlot;
    public Slot resultSlot;

    public Button bakeBtn;

    // Start is called before the first frame update
    void Start()
    {
        ButtonDisable();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonEnable();
    }

    void ButtonDisable()
    {
        bakeBtn.enabled = false;
    }

    void ButtonEnable()
    {
        if (stuffSlot.item != null && stuffSlot.item.itemType == 4 &&
            woodSlot.item != null && woodSlot.item.address == 200)
        {
            bakeBtn.enabled = true;
        }
    }

    public void BakeCraft()
    {
        int rand = Random.Range(201, 217);
        ItemData item = ItemDataBase.itemDataBase.itemList[rand]; //만들어질 아이템(address값 참조)

        resultSlot.AddItemData(item);

        stuffSlot.RemoveItem();
        woodSlot.RemoveItem();
    }
}
