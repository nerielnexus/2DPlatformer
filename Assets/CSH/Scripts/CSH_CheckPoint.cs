using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSH_CheckPoint : MonoBehaviour
{
    bool isTagged = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isTagged)
        {
            Debug.Log("체크포인트");
            isTagged = true;
            CSH_GameManager.instance.SpawnPointOverload();
        }
    }
}
