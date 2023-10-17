using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    /*
    몬스터 이동 로직
    왼쪽으로 이동한다 -> 목표위치에 도착하거나 지형이 끊긴다 -> 오른쪽으로 이동한다 -> 목표위치에 도착하거나 지형이 끊긴다 -> 왼쪽으로 이동한다

    초기지점 -> 왼쪽으로 range만큼 이동 -> isCheckLeft=true -> 오른쪽으로 range*2만큼 이동 -> isCheckLeft=false -> 왼쪽으로 range*2만큼 이동

     */
    protected enum ENEMYSTATE //적 상태
    {
        IDLE = 0,
        PATROL,
        FOUND,
        ATTACK,
        LOST,
        RETURN,
        DAMAGED,
        DIED
    }
    protected ENEMYSTATE enemyState = ENEMYSTATE.IDLE;

    public enum STYLE //공격타입 결정
    {
        MELEE,
        RANGE
    }
    public STYLE attackStyle;

    public int hp; //적 캐릭터의 체력
    public int speed = 1; //적 캐릭터의 이동속도
    public int patrolRange = 1; //적 캐릭터의 순찰반경
    public float sightRange = 5f; //적 캐릭터의 감지범위

    [Header("드랍 아이템과 골드")] //드랍들
    public List<ItemData> dropItems = new List<ItemData>();
    public int dropGold = 1;
    public GameObject dropitemz;

    protected SpriteRenderer _sprite;
    Animator _anime;

    protected bool isCheckLeft = false; //순찰 제어 플래그
    protected bool isFound = false; //발견 제어 플래그
    protected int rayX; //시야 좌우 제어값
    protected RaycastHit2D rayHit; //시야 판정용 레이캐스트힛
    protected RaycastHit2D[] groundRay; //지면 판정용 레이캐스트힛
    protected bool sawGround;

    protected bool isLost = false; //놓침 제어 플래그

    protected Vector3 startPos; //시작점
    protected Vector3 targetPos; //목표점

    [Header("사격 관련 수치")]
    public Transform shooter; //원거리 공격체 프리팹
    protected bool isShoot = false; //사격 제어 플래그
    public int shotSpeed = 1; //투사체 속도
    public int shootRange = 1; //투사체 사거리
    public float shootDelay = 0.1f; //투사체 딜레이

    [Header("공격력 수치")]
    public int attackValue; //몬스터 공격력
    public int fireAtk; //몬스터 불속성
    public int waterAtk; //몬스터 물속성
    public int lightAtk; //몬스터 빛속성
    public int darkAtk; //몬스터 어둠속성

    [Header("방어력 수치")]    
    public int defenceValue; //몬스터 방어력
    public int fireProof; //몬스터 불내성    
    public int waterProof; //몬스터 물내성    
    public int lightProof; //몬스터 빛내성    
    public int darkProof; //몬스터 어둠내성

    protected bool isDamaged = false;

    protected void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _anime = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        startPos = transform.position;
        targetPos = new Vector3(startPos.x - patrolRange, transform.position.y, 0);
    }

    private void OnDrawGizmos() //기즈모 그리기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, new Vector2(rayX, -2));
        Gizmos.DrawWireCube(transform.position, new Vector3(sightRange, 2, 0));
    }

    // Update is called once per frame
    protected void Update()
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

                    _anime.SetBool("isWalk", false);
                    _anime.SetBool("isIdle", true);
                    _anime.SetBool("isFire", false);
                    break;
                }
            case ENEMYSTATE.PATROL:
                {
                    if(patrolRange > 0)
                    {
                        Patrol();
                    }
                    
                    _anime.SetBool("isWalk", true);
                    _anime.SetBool("isIdle", false);
                    _anime.SetBool("isFire", false);
                    break;
                }
            case ENEMYSTATE.FOUND:
                {
                    _anime.SetBool("isWalk", false);
                    _anime.SetBool("isIdle", true);
                    _anime.SetBool("isFire", false);
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
                        _anime.SetBool("isWalk", false);
                        _anime.SetBool("isIdle", true);
                        _anime.SetBool("isFire", false);

                        StartCoroutine(Lost());
                    }

                    break;
                }
            case ENEMYSTATE.RETURN:
                {
                    Returning();

                    _anime.SetBool("isWalk", true);
                    _anime.SetBool("isIdle", false);
                    _anime.SetBool("isFire", false);
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
                    for(int i = 0; i <  dropItems.Count; i++)
                    {
                        int rand = Random.Range(0, 10);
                        Debug.Log("작동");

                        //if(rand % 2 == 0)
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

    protected void Sight() //플레이어를 발견하는 시야 함수
    {
        //적 시야 처리
        //rayHit = Physics2D.Raycast(transform.position, new Vector2(rayX, 0), sightRange, LayerMask.GetMask("Player")); //감지범위
        rayHit = Physics2D.BoxCast(transform.position, new Vector3(sightRange, 2, 0), 0f, Vector2.zero, 0f, LayerMask.GetMask("Player"));
        
        if ( rayHit.collider == true && rayHit.collider.tag == "Player" )
        {
            if (rayHit.transform.position.x > transform.position.x)
            {
                rayX = 1;
                _sprite.flipX = true;
            }
            else if (rayHit.transform.position.x <= transform.position.x)
            {
                rayX = -1;
                _sprite.flipX = false;
            }

            if (!isFound) StartCoroutine(Found());
        }
        else
        {
            isFound = false;
        }

        groundRay = Physics2D.RaycastAll(transform.position, new Vector2(rayX, -2f), 1f);
        for(int i = 0; i < groundRay.Length; i++)
        {
            if(groundRay[i].collider.tag == "Ground")
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

    protected void Patrol() //순찰 행동 함수
    {
        if( Mathf.Abs(transform.position.x - targetPos.x) <= 0.1f || !sawGround)
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

    protected IEnumerator Found() //플레이어를 발견했을 때 행동하기까지의 함수
    {
        isFound = true;
        enemyState = ENEMYSTATE.FOUND;

        yield return new WaitForSeconds(1.0f);

        if (isFound) enemyState = ENEMYSTATE.ATTACK;
        else
        {
            if (patrolRange > 0) enemyState = ENEMYSTATE.PATROL;
            else enemyState = ENEMYSTATE.IDLE;
        }

    }

    protected void Attack() //플레이어를 공격하는 함수
    {
        if (isFound)
        {
            if(attackStyle == STYLE.MELEE) //근거리형일 때
            {
                _anime.SetBool("isWalk", true);
                _anime.SetBool("isIdle", false);
                _anime.SetBool("isFire", false);
                transform.Translate(new Vector3(rayX * speed * 4 * Time.deltaTime, 0));
            }
            else if(attackStyle == STYLE.RANGE) //원거리형일 때
            {
                ShooterValueMatch();

                if (!isShoot)
                {
                    _anime.SetBool("isWalk", false);
                    _anime.SetBool("isIdle", false);
                    _anime.SetBool("isFire", true);
                    StartCoroutine(Shooting());
                }
            }
        }
        else
        {
            enemyState = ENEMYSTATE.LOST;
        }        
    }

    protected void ShooterValueMatch()
    {
        if(shooter == true)
        {
            shooter.GetComponent<DamageCollider>().attackerAtk = attackValue;
            shooter.GetComponent<DamageCollider>().attackerFire = fireAtk;
            shooter.GetComponent<DamageCollider>().attackerWater = waterAtk;
            shooter.GetComponent<DamageCollider>().attackerLight = lightAtk;
            shooter.GetComponent<DamageCollider>().attackerDark = darkAtk;
        }
    }

    protected IEnumerator Shooting() //발사체를 발사하는 함수
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

    protected IEnumerator Lost() //플레이어를 시야에서 놓쳤을 때 행동하기까지의 함수
    {
        isLost = true;
        yield return new WaitForSeconds(1.0f);

        //기다리는 중

        yield return new WaitForSeconds(1.0f);

        enemyState = ENEMYSTATE.RETURN;
        isLost = false;
    }

    protected void Returning() //처음 지점으로 되돌아가는 함수
    {
        if( Mathf.Abs(startPos.x - transform.position.x) <= 0.1f)
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
        else if(startPos.x > transform.position.x)
        {
            rayX = 1;
            _sprite.flipX = true;
            transform.Translate(new Vector3(speed * Time.deltaTime, 0));
        }
        else if(startPos.x < transform.position.x)
        {
            rayX = -1;
            _sprite.flipX = false;
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0));
        }
    }

    public void HitAndDamaged(int atk)
    {
        if(!isDamaged) StartCoroutine(Damaged(atk));
    }

    protected IEnumerator Damaged(int atk) //피해를 입었을 때의 함수
    {
        isDamaged = true;
        enemyState = ENEMYSTATE.DAMAGED;
        hp = hp - atk;

        if(hp <= 0) //체력이 다하면 사망
        {
            _anime.SetTrigger("Died");

            yield return new WaitForSeconds(0.8f);

            enemyState = ENEMYSTATE.DIED;
        }
        else //체력이 남았다면 플레이어를 시야에서 놓친 상태로 전환됨
        {
            _anime.SetTrigger("Damage");

            yield return new WaitForSeconds(0.8f);

            enemyState = ENEMYSTATE.LOST;
            isDamaged = false;
        }
    }


}
