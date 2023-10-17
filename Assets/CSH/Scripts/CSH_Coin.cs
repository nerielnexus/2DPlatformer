using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSH_Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("코인을 얻었습니다");

            Destroy(gameObject);
        }
    }
}
