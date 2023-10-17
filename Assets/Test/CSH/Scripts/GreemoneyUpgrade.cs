using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreemoneyUpgrade : MonoBehaviour
{
    public Slot weaponSlot;
    public Button pay100;
    public Button pay1000;
    public Button pay10000;
    public Text describle;

    // Start is called before the first frame update
    void Start()
    {
        ButtonDisable();
        DescribleReset();
    }

    // Update is called once per frame
    void Update()
    {
        ItemOnOff();
    }

    void ItemOnOff()
    {
        if(weaponSlot.item != null)
        {
            ButtonEnable();
        }
        else
        {
            ButtonDisable();
        }
    }

    void ButtonDisable()
    {
        pay100.enabled = false;
        pay1000.enabled = false;
        pay10000.enabled = false;
    }

    void ButtonEnable()
    {
        pay100.enabled = true;
        pay1000.enabled = true;
        pay10000.enabled = true;
    }

    public void Paid100()
    {       
        if (InvenUI.invenUI.golds >= 100)
        {
            InvenUI.invenUI.golds -= 100;

            int rand = Random.Range(0, 10);

            if (rand > 7)
            {
                EquipData weapon = weaponSlot.item as EquipData;

                weapon.value += 5;
                StartCoroutine(SuccessedDescrible());
            }
            else
            {
                weaponSlot.RemoveItem();
                StartCoroutine(FailedDescrible());
            }
        }
        else
        {
            Debug.Log("돈이 없잖아!");
        }
    }

    public void Paid1000()
    {
        if (InvenUI.invenUI.golds >= 1000)
        {
            InvenUI.invenUI.golds -= 1000;

            int rand = Random.Range(0, 10);

            if (rand > 4)
            {
                EquipData weapon = weaponSlot.item as EquipData;

                weapon.value += 3;
                StartCoroutine(SuccessedDescrible());
            }
            else
            {
                weaponSlot.RemoveItem();
                StartCoroutine(FailedDescrible());
            }
        }
        else
        {
            Debug.Log("돈이 없잖아!");
        }                
    }

    public void Paid10000()
    {
        if(InvenUI.invenUI.golds >= 10000)
        {
            InvenUI.invenUI.golds -= 10000;

            EquipData weapon = weaponSlot.item as EquipData;

            weapon.value += 1;
            StartCoroutine(SuccessedDescrible());
        }
        else
        {
            Debug.Log("돈이 없잖아!");
        }
    }

    void DescribleReset()
    {
        describle.text = "성공확률\n100골드 25%   1000골드 50%   10000골드 100%";
    }

    IEnumerator SuccessedDescrible()
    {
        StopCoroutine(FailedDescrible());
        describle.text = "성공!";

        yield return new WaitForSeconds(1.0f);

        DescribleReset();
    }

    IEnumerator FailedDescrible()
    {
        StopCoroutine(SuccessedDescrible());
        describle.text = "박살!";

        yield return new WaitForSeconds(1.0f);

        DescribleReset();
    }
}
