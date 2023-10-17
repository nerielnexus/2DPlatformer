using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명 : 소지형 아이템, 포물선으로 폭탄이 날라간 후 폭발

public class KHS_Item_Bomb : UseItem
{
    public float speed;
    GameObject player;
    public GameObject explosion;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        transform.position = player.transform.position + new Vector3(1 * Player.instance.isRight, 1, 0);
        rigid.AddForce(new Vector2(1 * Player.instance.isRight, 1) * speed);

        StartCoroutine(Destroy());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            Instantiate(explosion, gameObject.transform);
            rigid.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;

            Destroy(gameObject,1);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
