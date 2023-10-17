using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TilemapFog : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.name == "Player")
            gameObject.SetActive(false);
    }
}
