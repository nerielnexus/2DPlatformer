using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Transform explotion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground" || collision.transform.tag == "Player")
        {
            Transform boom = Instantiate(explotion);
            boom.position = transform.position;

            Destroy(gameObject);
        }
    }
}
