using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KHS_UI_Store : MonoBehaviour
{
    public InvenUI inven;
    public Text goldText;

    void Start()
    {
        inven = GameObject.Find("GameCanvas").GetComponent<InvenUI>();
    }

    void Update()
    {
        goldText.text = ":     " + inven.golds.ToString();
    }

    public void ClickNextButton(GameObject next)
    {
        next.SetActive(true);

    }

    public void ClickBeforeButton(GameObject now)
    {
        now.SetActive(false);
    }

    public void PurchaseCheapButton(GameObject item)
    {
        if (inven.golds >= 5000)
        {
            inven.AddItem(item);
            inven.golds -= 5000;
        }
    }

    public void PurchaseExpensiveButton(GameObject item)
    {
        if (inven.golds >= 7000)
        {
            inven.AddItem(item);
            inven.golds -= 7000;
        }
    }

}
