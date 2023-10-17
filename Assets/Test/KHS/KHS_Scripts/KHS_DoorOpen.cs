using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_DoorOpen : MonoBehaviour
{
    public GameObject door;
    public GameObject nedUI;
    public bool check;
    public static int cntUI;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && check == false)
        {
            door.gameObject.SetActive(false);
            cntUI++;
            check = true;
            if (cntUI == 3)
            {
                nedUI.SetActive(true);
            }
        }
    }
}
