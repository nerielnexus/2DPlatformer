using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardMonster : Monster
{
    new enum ENEMYSTATE
    {
        IDLE,
        RUN,
        DAMAGED,
        DIED
    }
    ENEMYSTATE enemyStat = ENEMYSTATE.IDLE;

    Rigidbody2D _rigid;
    Animator _ani;

    RaycastHit2D groundRay;
    RaycastHit2D jumpRay;
    bool isJump = false;

    new private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _ani = GetComponent<Animator>();
    }

    new private void Update()
    {
        //Debug.Log(_rigid.velocity.y);

        Sight();

        switch (enemyStat)
        {
            case ENEMYSTATE.IDLE:
                {
                    _ani.SetBool("isIdle", true);
                    _ani.SetBool("isRun", false);
                    break;
                }
            case ENEMYSTATE.RUN:
                {
                    RunAway();
                    Jump();

                    _ani.SetBool("isIdle", false);
                    _ani.SetBool("isRun", true);
                    _ani.SetFloat("veloY", _rigid.velocity.y);

                    break;
                }
            case ENEMYSTATE.DAMAGED:
                {
                    _ani.SetBool("isIdle", true);
                    _ani.SetBool("isRun", false);

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, new Vector2(sightRange * transform.localScale.x, 0));
        Gizmos.DrawRay(transform.position, new Vector2(transform.localScale.x, -1f).normalized);
    }
    new void Sight()
    {
        rayHit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), sightRange, LayerMask.GetMask("Player"));
        if (rayHit.collider == true && rayHit.collider.tag == "Player")
        {
            
            if (!isFound)
            {
                StartCoroutine(Found());
            }
        }
    }

    new IEnumerator Found()
    {
        isFound = true;

        yield return new WaitForSeconds(0.5f);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        enemyStat = ENEMYSTATE.RUN;
    }

    void RunAway()
    {        
        transform.Translate(new Vector3(transform.localScale.x * speed * Time.deltaTime, 0));
    }

    void Jump()
    {
        groundRay = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, -1f), 1f, LayerMask.GetMask("Ground"));
        jumpRay = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), 1f, LayerMask.GetMask("Ground"));

        if (groundRay.collider == false || jumpRay.collider == true)
        {
            if (!isJump)
            {
                isJump = true;
                Debug.Log("점프");
                _ani.SetBool("isJump", true);
                _rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }

        }
        else if (groundRay.collider == true && jumpRay.collider == false)
        {
            isJump = false;
            _ani.SetBool("isJump", false);
        }
    }

    new void HitAndDamaged(int atk)
    {
        if (!isDamaged) StartCoroutine(Damaged(atk));
    }

    new IEnumerator Damaged(int atk) //피해를 입었을 때의 함수
    {
        isDamaged = true;
        enemyStat = ENEMYSTATE.DAMAGED;
        hp = hp - atk;

        if (hp <= 0) //체력이 다하면 사망
        {
            yield return new WaitForSeconds(0.2f);
            enemyStat = ENEMYSTATE.DIED;
        }
        else //체력이 남았다면 플레이어를 시야에서 놓친 상태로 전환됨
        {
            yield return new WaitForSeconds(0.2f);
            enemyStat = ENEMYSTATE.RUN;
            isDamaged = false;
        }

    }



}
