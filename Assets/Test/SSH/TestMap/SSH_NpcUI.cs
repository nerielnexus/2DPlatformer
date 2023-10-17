using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_NpcUI : MonoBehaviour
{
    // 강화 UI 창
    GameObject enforceUI;

    // 제작 UI 창
    GameObject craftUI;


    private void Start()
    {
        enforceUI = GameObject.Find("Enforce");
        craftUI = GameObject.Find("CraftUI");
    }

}
