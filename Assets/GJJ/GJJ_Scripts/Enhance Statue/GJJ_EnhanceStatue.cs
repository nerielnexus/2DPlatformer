using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GJJ_EnhanceStatue : MonoBehaviour
{
    [SerializeField] private string[] statueTextList;

    void OnMouseDown()
    {
        if (!(EventSystem.current.IsPointerOverGameObject()))
        {
            TalkManager.instance.OnTalk(statueTextList);
        }
        else
        {
            Debug.Log("??");
        }
    }
}
