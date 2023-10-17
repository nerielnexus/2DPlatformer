using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KHS_Scene_UI : MonoBehaviour
{
    public Button enforceBtn;
    public Button propertyBtn;

    GameObject enforce;
    GameObject property;

    void Start()
    {
        enforce = GameObject.Find("KHS").transform.Find("KHS_EnforceItem").gameObject;
        property = GameObject.Find("KHS").transform.Find("KHS_PropertyItem").gameObject;

        enforceBtn.onClick.AddListener(() => OpenUI(enforce));
        propertyBtn.onClick.AddListener(() => OpenUI(property));
    }

    void Update()
    {
        
    }

    public void OpenUI(GameObject ui)
    {
        ui.SetActive(true);
    }
    

}
