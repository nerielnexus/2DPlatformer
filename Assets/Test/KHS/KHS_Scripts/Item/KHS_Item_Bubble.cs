using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명 : 소지형 아이템, 사용하면 버블에 둘러쌓여 천천히 하늘로 오라감

// 레이어로 서로 충돌을 방지해야하는데.. 

public class KHS_Item_Bubble : UseItem
{
    GameObject player;
    public float upSpeed;
    public float moveSpeed;

    bool enemyTouch = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rigid.AddForce((Vector2.right * Player.instance.isRight) * moveSpeed * Time.deltaTime);
            
        StartCoroutine(BubbleDestroy());
        transform.position = player.transform.position + new Vector3(0, 0.5f, 0);
    }

    
    void Update()
    {
        // 적에게 물방울이 붙으면 떠오른다.
        if (enemyTouch == true)
        {
            transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
        }
    }

    // 적과 닿으면 떠 오르고 2초뒤 소멸됨
    IEnumerator BubbleInEnemy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    // 적과 부딪치지 않으면 4초뒤 소멸됨
    IEnumerator BubbleDestroy()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.layer==9 && other.transform.parent == null && transform.childCount == 0) // (name 대신 tag로 변경)
        {
            StopAllCoroutines();
            StartCoroutine(BubbleInEnemy());
            enemyTouch = true;
            rigid.velocity = Vector2.zero;
            transform.position = other.transform.position;
            other.transform.SetParent(this.transform);
            other.gameObject.GetComponent<Collider2D>().isTrigger = true;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // (만약 몬스터 움직임이 Rigidbody가 아니면 수정)
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
}
