using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KHS_Create : MonoBehaviour
{
    public Slot item;
    public Slot result;
    float time = 5f;
    bool startTime = false;
    public Text timeText;

    public UseItemData[] useItem;

    void Update()
    {
        if (!item.itemOn)
        {
            timeText.text = 5 + "초";
            StopAllCoroutines();
        }
    }

    public void Create()
    {
        int num = Random.Range(0, useItem.Length);

        if (item.itemOn)
        {
            StartCoroutine(Timer(num));
        }
    }

    IEnumerator Timer(int num)
    {
        for(int i=0; i<5; i++)
        {
            yield return new WaitForSeconds(1f);
            timeText.text = (4-i) + "초";

            if(4 == i)
            {
                result.AddItemData(useItem[num]);
                timeText.text = 5 + "초";
                item.RemoveItem();
            }
        }        
    }

}
