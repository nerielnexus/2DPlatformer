using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TriggerAndDoor : MonoBehaviour
{
    // private
    [SerializeField] private List<GJJ_Trigger_Howto> listTrigger = new List<GJJ_Trigger_Howto>();
    [SerializeField] private GameObject door;

    // method
    void OpenTheDoor()
    {
        foreach(GJJ_Trigger_Howto tmp in listTrigger)
            if (!tmp.isActivated) return;

        door.SetActive(false);
    }

    // unity
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
            listTrigger.Add(transform.GetChild(i).gameObject.GetComponent<GJJ_Trigger_Howto>());
    }

    private void Update()
    {
        OpenTheDoor();
    }
}
