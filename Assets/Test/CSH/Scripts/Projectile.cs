using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    float scaleX;
    [HideInInspector]
    public int shootRange;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        scaleX = transform.localScale.x;

        startPos = transform.position;
        //Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * scaleX * Time.deltaTime);

        if((startPos - transform.position).magnitude > shootRange)
        {
            DeleteThisShot();
        }
    }

    void RangeCalcul()
    {
        float nowRange = (startPos - transform.position).magnitude;
        Debug.Log("현재 거리: " + nowRange);
        Debug.Log("사거리: " + shootRange);
    }

    void DeleteThisShot()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
