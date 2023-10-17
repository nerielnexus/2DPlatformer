using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KHS_NPC_Button : MonoBehaviour
{
    public Button enfBtn;
    public Button crtBtn;

    GameObject enforce;
    GameObject create;

    void Start()
    {
        enforce = GameObject.Find("GameCanvas").transform.Find("Enforce").gameObject;
        create = GameObject.Find("GameCanvas").transform.Find("CraftUI").gameObject;
        
        enfBtn.onClick.AddListener(() => OpenEnforce());
        crtBtn.onClick.AddListener(() => OpenCreate());
    }

    void OpenEnforce()
    {
        enforce.SetActive(true);
    }

    void OpenCreate()
    {
        create.SetActive(true);
    }

    public void CloseBox(GameObject closeBox)
    {
        closeBox.SetActive(false);
    }

}
