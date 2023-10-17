using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Night : MonoBehaviour
{
    //원래 시야
    public GameObject dark;

    //밝게 보여주기용
    public GameObject fireLight;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            dark.SetActive(true);
            fireLight.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            dark.SetActive(false);
            fireLight.SetActive(true);
        }
    }
}
