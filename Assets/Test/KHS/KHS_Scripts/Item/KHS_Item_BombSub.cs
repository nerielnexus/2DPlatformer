using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_Item_BombSub : MonoBehaviour
{
    public Rigidbody2D rigid;
    public GameObject bomb;
    Vector2 bombPos;
    Vector2 target;
    bool isTarget = false;

    void Update()
    {
        if (isTarget == true)
        {
            bombPos = Vector2.MoveTowards(bomb.transform.position, target, Time.deltaTime * 30f);
            rigid.MovePosition(bombPos);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // (변경필요)
        if (other.gameObject.tag == "Enemy" && isTarget == false)
        {
            rigid.velocity = Vector2.zero;
            target = other.transform.position;

            isTarget = true;
        }
    }
}
