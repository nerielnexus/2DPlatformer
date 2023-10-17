using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TrapSpinSpikeBall_Ballhead : MonoBehaviour
{
    // method
    bool CheckPlayer(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return false;

        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            return false;

        if (!col.gameObject.name.Equals("PlayerMan"))
            return false;   

        return true;
    }

    // unity
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckPlayer(collision))
        {
            Debug.Log("Spikeballhead - player detected");
        }
    }
}
