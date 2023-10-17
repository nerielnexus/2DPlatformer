using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_Item_Missile : UseItem
{
    public GameObject[] enemyArr;   // 적 배열
    GameObject player, targetEnemy; // 플레이어, 목표 적
    float standDis, dis;            // 기준거리, 비교거리
    float speed;                    // 스피드
    bool targetMove;                // 타깃 이동 변수

    void Awake()
    {
        enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.Find("PlayerMan");

        standDis = Vector2.Distance(player.transform.position, enemyArr[0].transform.position);
        targetEnemy = enemyArr[0];
        targetMove = false;
    }


    void Start()
    {
        speed = 5;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        for (int i = 0; i < enemyArr.Length; i++)
        {
            dis = Vector2.Distance(player.transform.position, enemyArr[i].transform.position);

            if (standDis > dis)
            {
                dis = standDis;
                targetEnemy = enemyArr[i];
            }
        }

        StartCoroutine(targetAttack());
    }

    void Update()
    {
        if (targetMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    IEnumerator targetAttack()
    {
        yield return new WaitForSeconds(2f);
        speed = 0;
        transform.rotation = Quaternion.FromToRotation(Vector2.left, transform.position - targetEnemy.transform.position);
        yield return new WaitForSeconds(1f);
        speed = 30;
        targetMove = true;
    }
}
