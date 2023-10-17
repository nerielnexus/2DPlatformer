using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 설명 : 소지형 아이템, 플레이어가 바라보는 방향으로 일자로 날아감 

public class KHS_Item_Lance : UseItem
{
    public float speed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(TimeDestroy());

        rigid.AddForce((Vector2.right * Player.instance.isRight) * speed, ForceMode2D.Impulse);

        if (Player.instance.isRight == -1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    } 
    
    IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
