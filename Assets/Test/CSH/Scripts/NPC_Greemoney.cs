using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Greemoney : MonoBehaviour
{
    public Transform greemoneyUI;

    // Start is called before the first frame update
    void Start()
    {
        greemoneyUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UIOnOff();
    }

    void UIOnOff()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (greemoneyUI.gameObject.activeSelf)
            {
                greemoneyUI.gameObject.SetActive(false);
            }
            else if(!greemoneyUI.gameObject.activeSelf)
            {
                greemoneyUI.gameObject.SetActive(true);
            }
        }
    }
}
