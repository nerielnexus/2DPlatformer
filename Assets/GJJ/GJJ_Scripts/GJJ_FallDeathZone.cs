using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_FallDeathZone : MonoBehaviour
{
    [SerializeField] private int counter = 0;
    [SerializeField] private GJJ_CheckpointEventModule module;
    [SerializeField] private RigidbodySleepMode2D sleepMode;
    [SerializeField] private GameObject player;

    bool CheckPlayer(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return false;

        if (col.gameObject.layer != LayerMask.NameToLayer("Player"))
            return false;

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CheckPlayer(collision))
            return;

        module = collision.gameObject.GetComponent<GJJ_CheckpointEventModule>();
        sleepMode = collision.gameObject.GetComponent<Rigidbody2D>().sleepMode;

        collision.gameObject.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!CheckPlayer(collision))
            return;

        counter++;

        if(counter >= 50)
        {
            module.InvokePlayerDeathEvent();
            collision.gameObject.GetComponent<Rigidbody2D>().sleepMode = sleepMode;
            counter = 0;
        }
    }
}
