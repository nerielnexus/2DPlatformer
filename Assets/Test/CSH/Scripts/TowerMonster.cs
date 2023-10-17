using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMonster : Monster
{
    // Start is called before the first frame update
    //void Start() { }

    // Update is called once per frame
    new void Update()
    {
        Sight();

        switch (enemyState)
        {
            case ENEMYSTATE.IDLE:
                {
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
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawWireCube(transform.position, new Vector2(sightRange * 2, sightRange));
    }
    new void Sight()
    {
        //rayHit = Physics2D.CircleCast(transform.position, sightRange, transform.position, 0f, LayerMask.GetMask("Player"));
        rayHit = Physics2D.CapsuleCast(transform.position, new Vector2(sightRange * 2, sightRange), CapsuleDirection2D.Horizontal, 0f, Vector2.zero, 0f, LayerMask.GetMask("Player"));


        if (rayHit.collider == true && rayHit.collider.tag == "Player")
        {
            //Debug.Log("플레이어 감지됨");

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
        else if (rayHit.collider == false)
        {
            isFound = false;
        }
    }

    new void Attack()
    {
        if (isFound)
        {
            if (!isShoot)
            {
                ShooterValueMatch();

                StartCoroutine(Shooting());
            }
        }
        else
        {
            enemyState = ENEMYSTATE.IDLE;
        }
    }

    new IEnumerator Shooting()
    {
        isShoot = true;

        Transform shot = Instantiate(shooter);
        shot.GetComponent<Projectile>().shootRange = this.shootRange;
        shot.GetComponent<Projectile>().speed = shotSpeed;

        shot.localScale = new Vector3(-rayX, 1, 1);
        shot.position = transform.position;

        yield return new WaitForSeconds(shootDelay);
        isShoot = false;
    }

    new void HitAndDamaged(int atk)
    {
        if (!isDamaged) StartCoroutine(Damaged(atk));
    }

    new IEnumerator Damaged(int atk) //피해를 입었을 때의 함수
    {
        isDamaged = true;
        enemyState = ENEMYSTATE.DAMAGED;
        hp = hp - atk;

        if (hp <= 0) //체력이 다하면 사망
        {
            _sprite.color = new Vector4(1, 1, 1, 0.5f);

            yield return new WaitForSeconds(0.2f);

            _sprite.color = new Vector4(1, 1, 1, 1f);
            enemyState = ENEMYSTATE.DIED;
        }
        else //체력이 남았다면 플레이어를 시야에서 놓친 상태로 전환됨
        {
            _sprite.color = new Vector4(1, 1, 1, 0.5f);

            yield return new WaitForSeconds(0.2f);

            _sprite.color = new Vector4(1, 1, 1, 1f);
            enemyState = ENEMYSTATE.IDLE;
            isDamaged = false;
        }

    }


}
