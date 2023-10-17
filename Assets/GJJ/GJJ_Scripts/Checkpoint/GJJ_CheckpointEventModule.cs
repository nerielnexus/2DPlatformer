using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_CheckpointEventModule : MonoBehaviour
{
    public delegate void DeathEvent();
    public event DeathEvent playerDeath;

    void GJJ_ImitatePlayerDeathEvent()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            playerDeath.Invoke();
    }

    public void InvokePlayerDeathEvent()
    {
        playerDeath.Invoke();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        GJJ_ImitatePlayerDeathEvent();
    }
}
