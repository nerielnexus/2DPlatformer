using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMonster : Monster
{
    new enum ENEMYSTATE
    {
        SNIPE,
        DAMAGED,
        DIED
    }
    ENEMYSTATE enemyStat = ENEMYSTATE.SNIPE;

    Transform player;


    new private void Awake()
    {
        attackStyle = STYLE.RANGE;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    new void Update()
    {
        AngleCalcul();

        switch (enemyStat)
        {
            case ENEMYSTATE.SNIPE:
                {
                    if (!isShoot) StartCoroutine(Sniping());
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
    { }

    void AngleCalcul()
    {
        float angle = Vector2.Angle(transform.position, player.position);
        //Debug.Log("각도 " + angle);
    }

    IEnumerator Sniping()
    {
        isShoot = true;

        Transform shot = Instantiate(shooter);
        shot.GetComponent<Projectile>().shootRange = shootRange;
        shot.GetComponent<Projectile>().speed = shotSpeed;

        shot.position = transform.position;

        if(player.position.x < transform.position.x)
        {
            shot.localScale = new Vector3(1, 1, 1);
            shot.Rotate(new Vector3(0, 0, Vector2.Angle(transform.position, player.position) * 2));
        }
        else if(transform.position.x < player.position.x)
        {
            shot.localScale = new Vector3(-1, 1, 1);
            shot.Rotate(new Vector3(0, 0, -Vector2.Angle(transform.position, player.position) * 2));
        }

        yield return new WaitForSeconds(shootDelay);

        isShoot = false;
    }

    new IEnumerator Damaged(int atk) //피해를 입었을 때의 함수
    {
        isDamaged = true;
        enemyStat = ENEMYSTATE.DAMAGED;
        hp = hp - atk;

        if (hp <= 0) //체력이 다하면 사망
        {
            //_anime.SetTrigger("Died");

            yield return new WaitForSeconds(0.8f);

            enemyStat = ENEMYSTATE.DIED;
        }
        else //체력이 남았다면 플레이어를 시야에서 놓친 상태로 전환됨
        {
            //_anime.SetTrigger("Damage");

            yield return new WaitForSeconds(0.8f);

            enemyStat = ENEMYSTATE.SNIPE;
            isDamaged = false;
        }
    }
}
