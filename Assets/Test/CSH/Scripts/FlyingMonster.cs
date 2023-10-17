using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : Monster
{
    //RaycastHit2D rayHit;

    // Start is called before the first frame update
    //void Start()    {     }

    // Update is called once per frame
    new void Update()
    {
        

        Sight();

        switch (enemyState)
        {
            case ENEMYSTATE.IDLE:
                {
                    if(patrolRange > 0)
                    {
                        enemyState = ENEMYSTATE.PATROL;
                    }
                    
                    break;
                }
            case ENEMYSTATE.PATROL:
                {
                    if(patrolRange > 0)
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

                    Destroy(gameObject);

                    break;
                }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    //시야
    new void Sight()
    {
        rayHit = Physics2D.CircleCast(transform.position, sightRange, transform.position, 0f, LayerMask.GetMask("Player"));
        if(rayHit.collider == true && rayHit.collider.tag == "Player")
        {
            //Debug.Log("플레이어 감지됨");
            if(rayHit.transform.position.x > transform.position.x)
            {
                rayX = 1;
                _sprite.flipX = true;
            }
            else if(rayHit.transform.position.x <= transform.position.x)
            {
                rayX = -1;
                _sprite.flipX = false;
            }
            if (!isFound) StartCoroutine(Found());
        }
        else if(rayHit.collider == false)
        {
            //Debug.Log("플레이어 감지 안됨");
            isFound = false;
        }        
    }

    //순찰
    new void Patrol()
    {
        if (Mathf.Abs(transform.position.x - targetPos.x) <= 0.1f)
        {
            if (isCheckLeft)
            {
                isCheckLeft = false;
                targetPos = new Vector3(transform.position.x - (patrolRange * 2), transform.position.y, 0);
            }
            else
            {
                isCheckLeft = true;
                targetPos = new Vector3(transform.position.x + (patrolRange * 2), transform.position.y, 0);
            }
        }
        if (!isCheckLeft) //왼쪽으로 이동
        {
            rayX = -1;
            _sprite.flipX = false;
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0));
        }
        else //오른쪽으로 이동
        {
            rayX = 1;
            _sprite.flipX = true;
            transform.Translate(new Vector3(speed * Time.deltaTime, 0));
        }
    }



    //플레이어를 공격
    new void Attack()
    {
        if (isFound)
        {
            if (attackStyle == STYLE.MELEE)
            {
                Vector3 targeting = (rayHit.transform.position - transform.position).normalized;

                transform.Translate(targeting * speed * 1.5f * Time.deltaTime);
            }
            else if(attackStyle == STYLE.RANGE)
            {
                ShooterValueMatch();

                if (!isShoot)
                {
                    StartCoroutine(Shooting());                    
                }
                
            }
        }
        else enemyState = ENEMYSTATE.LOST;        
    }

    //발사
    new IEnumerator Shooting()
    {
        isShoot = true;

        Transform shot = Instantiate(shooter);
        shot.GetComponent<Projectile>().shootRange = this.shootRange;
        shot.GetComponent<Projectile>().speed = shotSpeed;

        shot.localScale = new Vector3(-rayX, 1, 1);
        shot.position = transform.position;

        shot.Rotate(Vector3.forward, 30f * -rayX); //발사각

        yield return new WaitForSeconds(shootDelay);
        isShoot = false;
    }

    //복귀
    new void Returning()
    {
        Vector3 returnPos = (startPos - transform.position).normalized;

        if(  Mathf.Abs(startPos.x- transform.position.x) <= 0.1f && Mathf.Abs(startPos.y - transform.position.y) <= 0.1f)
        {
            transform.position = startPos;

            if (patrolRange > 0) enemyState = ENEMYSTATE.PATROL;
            else enemyState = ENEMYSTATE.IDLE;
        }
        else
        {
            if(transform.position.x > startPos.x)
            {
                rayX = 1;
                _sprite.flipX = true;
            }
            else if(transform.position.x <= startPos.x)
            {
                rayX = -1;
                _sprite.flipX = false;
            }            

            transform.Translate(returnPos * speed * Time.deltaTime);
        }        
    }


}
