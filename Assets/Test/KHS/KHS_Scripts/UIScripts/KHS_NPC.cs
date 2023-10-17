using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KHS_NPC : MonoBehaviour
{
    public string[] talk;

    void OnMouseDown()
    {
        if (!(EventSystem.current.IsPointerOverGameObject()))
        {
            TalkManager.instance.OnTalk(talk);
        }
        else
        {
            Debug.Log("??");
        }
    }
}
