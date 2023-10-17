using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_FakeGround : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            //Debug.Log(gameObject.name + " 도착함");
        }
    }
}
