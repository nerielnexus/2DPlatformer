using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSH_GoalCollider : MonoBehaviour
{
    bool isTagged = false;
    public Transform gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isTagged)
        {
            Debug.Log("골인지점에 도착했습니다");
            isTagged = true;
            CSH_GameManager.instance.GoalIn();
        }
    }
}
