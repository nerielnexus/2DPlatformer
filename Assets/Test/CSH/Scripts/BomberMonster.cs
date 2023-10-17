using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMonster : Monster
{

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

                    break;
                }
            case ENEMYSTATE.PATROL:
                {
                    if (patrolRange > 0)
                    {
                        Patrol();
                    }

                    break;
                }
            case ENEMYSTATE.FOUND:
                {

                    break;
                }
            case ENEMYSTATE.ATTACK:
                {
                    Attack();

                    break;
                }
            case ENEMYSTATE.LOST:
                {
                    if (!isLost)
                    {

                        StartCoroutine(Lost());
                    }

                    break;
                }
            case ENEMYSTATE.RETURN:
                {
                    Returning();


                    break;
                }
            case ENEMYSTATE.DAMAGED:
                {
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

    new void Sight()
    {
        rayHit = Physics2D.BoxCast(transform.position, new Vector3(sightRange, 2, 0), 0f, Vector2.zero, 0f, LayerMask.GetMask("Player"));

        if (rayHit.collider == true && rayHit.collider.tag == "Player")
        {
            if (rayHit.transform.position.x > transform.position.x)
            {
                rayX = 1;
                transform.localScale = new Vector3(-rayX, 1, 1);
            }
            else if (rayHit.transform.position.x <= transform.position.x)
            {
                rayX = -1;
                transform.localScale = new Vector3(-rayX, 1, 1);
            }

            if (!isFound) StartCoroutine(Found());
        }
        else
        {
            isFound = false;
        }

        groundRay = Physics2D.RaycastAll(transform.position, new Vector2(rayX, -2f), 1f);
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

        if (!isCheckLeft) //왼쪽으로 이동
        {
            rayX = -1;
            transform.localScale = new Vector3(-rayX, 1, 1);
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0));
        }
        else //오른쪽으로 이동
        {
            rayX = 1;
            transform.localScale = new Vector3(-rayX, 1, 1);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0));
        }
    }

    new void Attack()
    {
        if (isFound)
        {
            if (!isShoot)
            {
                StartCoroutine(Throwing());
            }
        }
        else
        {
            enemyState = ENEMYSTATE.LOST;
        }        
    }

    IEnumerator Throwing()
    {
        isShoot = true;

        Transform bomb = Instantiate(shooter);
        bomb.position = transform.position;
        bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(-shotSpeed * transform.localScale.x, shootRange), ForceMode2D.Impulse);

        yield return new WaitForSeconds(shootDelay);

        isShoot = false;

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
            transform.localScale = new Vector3(-rayX, 1, 1);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0));
        }
        else if (startPos.x < transform.position.x)
        {
            rayX = -1;
            transform.localScale = new Vector3(-rayX, 1, 1);
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0));
        }
    }
}
