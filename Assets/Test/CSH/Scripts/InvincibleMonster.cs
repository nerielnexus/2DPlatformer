﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleMonster : Monster
{
    bool isInvincible = true; //무적 처리용 플래그

    Animator _ani;

    RaycastHit2D inMelee;

    new void Awake()
    {
        _ani = GetComponent<Animator>();
    }

    new void Update()
    {

        Sight();

        switch (enemyState)
        {
            case ENEMYSTATE.IDLE:
                {
                    if (patrolRange > 0)
                    {
                        enemyState = ENEMYSTATE.PATROL;
                    }

                    _ani.SetBool("isIdle", true);
                    _ani.SetBool("isWalk", false);
                    _ani.SetBool("isRun", false);
                    _ani.SetBool("isAttack", false);

                    break;
                }
            case ENEMYSTATE.PATROL:
                {
                    if (patrolRange > 0)
                    {
                        Patrol();
                    }

                    _ani.SetBool("isIdle", false);
                    _ani.SetBool("isWalk", true);
                    _ani.SetBool("isRun", false);
                    _ani.SetBool("isAttack", false);

                    break;
                }
            case ENEMYSTATE.FOUND:
                {
                    _ani.SetBool("isIdle", true);
                    _ani.SetBool("isWalk", false);
                    _ani.SetBool("isRun", false);
                    _ani.SetBool("isAttack", false);

                    break;
                }
            case ENEMYSTATE.ATTACK:
                {
                    isInvincible = false;
                    Attack();

                    break;
                }
            case ENEMYSTATE.LOST:
                {
                    if (!isLost)
                    {
                        isInvincible = true;
                        StartCoroutine(Lost());
                    }

                    _ani.SetBool("isIdle", true);
                    _ani.SetBool("isWalk", false);
                    _ani.SetBool("isRun", false);
                    _ani.SetBool("isAttack", false);

                    break;
                }
            case ENEMYSTATE.RETURN:
                {
                    Returning();

                    _ani.SetBool("isIdle", false);
                    _ani.SetBool("isWalk", true);
                    _ani.SetBool("isRun", false);
                    _ani.SetBool("isAttack", false);

                    break;
                }
            case ENEMYSTATE.DAMAGED:
                {
                    //if(!isDamaged) StartCoroutine(Damaged(1));

                    break;
                }
            case ENEMYSTATE.DIED:
                {
                    Debug.Log("골드 " + dropGold + "드랍");
                    InvenUI.invenUI.golds += dropGold;
                    for (int i = 0; i < dropItems.Count; i++)
                    {
                        int rand = Random.Range(0, 10);
                        Debug.Log("작동");

                        //if (rand % 2 == 0)
                        {
                            dropitemz.GetComponent<DropItem>().itemData = dropItems[i];
                            Instantiate(dropitemz, this.transform.position, Quaternion.identity);

                            Debug.Log(dropItems[i].name + "드랍됨");
                        }
                    }

                    Destroy(this.gameObject);

                    break;
                }

        }
    }

    private void OnDrawGizmos() //기즈모 그리기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, 0), new Vector2(rayX, -2));

        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, 0), new Vector2(rayX, 0));

        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + 1, 0), new Vector3(sightRange, 2, 0));
    }

    new void Sight() //플레이어를 발견하는 시야 함수
    {
        //적 시야 처리
        rayHit = Physics2D.BoxCast(new Vector3(transform.position.x, transform.position.y + 1, 0), new Vector3(sightRange, 2, 0), 0f, Vector2.zero, 0f, LayerMask.GetMask("Player"));

        if (rayHit.collider == true && rayHit.collider.tag == "Player")
        {
            if (rayHit.transform.position.x > transform.position.x)
            {
                rayX = 1;
                transform.localScale = new Vector3(0.2f * rayX, 0.2f, 1);
            }
            else if (rayHit.transform.position.x <= transform.position.x)
            {
                rayX = -1;
                transform.localScale = new Vector3(0.2f * rayX, 0.2f, 1);
            }

            if (!isFound) StartCoroutine(Found());
        }
        else
        {
            isFound = false;
        }

        groundRay = Physics2D.RaycastAll(new Vector3(transform.position.x, transform.position.y + 1, 0), new Vector2(rayX, -2f), 2f);
        for (int i = 0; i < groundRay.Length; i++)
        {
            if (groundRay[i].collider.tag == "Ground")
            {
                sawGround = true;
                return;
            }
            else
            {
                sawGround = false;
            }
        }
    }

    new void Patrol() //순찰 행동 함수
    {
        if (Mathf.Abs(transform.position.x - targetPos.x) <= 0.1f || !sawGround)
        {
            if (isCheckLeft)
            {
                isCheckLeft = false;
                startPos = transform.position;
                targetPos = new Vector3(startPos.x - (patrolRange * 2), startPos.y, 0);
            }
            else
            {
                isCheckLeft = true;
                startPos = transform.position;
                targetPos = new Vector3(startPos.x + (patrolRange * 2), startPos.y, 0);
            }
        }
        //Debug.Log(targetPos.x);

        if (!isCheckLeft) //왼쪽으로 이동
        {
            rayX = -1;
            transform.localScale = new Vector3(0.2f * rayX, 0.2f, 1);
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0));
        }
        else //오른쪽으로 이동
        {
            rayX = 1;
            transform.localScale = new Vector3(0.2f * rayX, 0.2f, 1);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0));
        }
    }

    new void Attack() //플레이어를 공격하는 함수
    {
        
        inMelee = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 1), new Vector2(rayX, 0), 1f, LayerMask.GetMask("Player"));

        if (isFound)
        {
            if (inMelee == false)
            {
                transform.Translate(new Vector3(rayX * speed * 4 * Time.deltaTime, 0));

                _ani.SetBool("isIdle", false);
                _ani.SetBool("isWalk", false);
                _ani.SetBool("isRun", true);
                _ani.SetBool("isAttack", false);
            }
            else if (inMelee == true)
            {
                _ani.SetBool("isIdle", false);
                _ani.SetBool("isWalk", false);
                _ani.SetBool("isRun", false);
                _ani.SetBool("isAttack", true);
            }
        }
        else
        {
            enemyState = ENEMYSTATE.LOST;
        }
    }

    new void Returning() //처음 지점으로 되돌아가는 함수
    {
        if (Mathf.Abs(startPos.x - transform.position.x) <= 0.1f)
        {
            if (patrolRange > 0)
            {
                enemyState = ENEMYSTATE.PATROL;
            }
            else
            {
                enemyState = ENEMYSTATE.IDLE;
            }
        }
        else if (startPos.x > transform.position.x)
        {
            rayX = 1;
            transform.localScale = new Vector3(0.2f * rayX, 0.2f, 1);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0));
        }
        else if (startPos.x < transform.position.x)
        {
            rayX = -1;
            transform.localScale = new Vector3(0.2f * rayX, 0.2f, 1);
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0));
        }
    }

    new public void HitAndDamaged(int atk)
    {
        if (!isInvincible)
        {
            if (!isDamaged) StartCoroutine(Damaged(atk));
        }

        
    }

    new IEnumerator Damaged(int atk) //피해를 입었을 때의 함수
    {
        isDamaged = true;
        enemyState = ENEMYSTATE.DAMAGED;
        hp = hp - atk;

        if (hp <= 0) //체력이 다하면 사망
        {
            _ani.SetTrigger("Died");

            yield return new WaitForSeconds(0.8f);

            enemyState = ENEMYSTATE.DIED;
        }
        else //체력이 남았다면 플레이어를 시야에서 놓친 상태로 전환됨
        {
            _ani.SetTrigger("Damaged");

            yield return new WaitForSeconds(0.4f);

            enemyState = ENEMYSTATE.LOST;
            isDamaged = false;
        }

    }

}
