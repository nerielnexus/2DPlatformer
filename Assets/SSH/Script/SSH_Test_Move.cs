using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_Test_Move : MonoBehaviour
{
    [Header("넣어야 할 것들")]
    //플레이어 오브젝트
    public GameObject player;


    //게임 매니저
    public SSH_Test_GameManager gameManager;

    //실제 사용 인벤 UI
    public InvenUI inven;

    //정면 확인
    public Transform front;

    //아이템 사용장소
    public Transform itemUsePos;

    [Header("레이마스크")]
    //Ground
    public LayerMask groundLayer;
    //Item
    public LayerMask itemLayer;


 

    [Header("실제 사용 속도")]
    //속도(실제 행동 속도)
    public float speed;

    [Header("캐릭터 스텟")]
    //기본속도
    public float normalSpeed;

    //달리기속도
    public float runSpeed;

    //최대속도
    public float maxSpeed => stat.speed;

    //점프힘
    public float jumpPower;

    //점프횟수(실제 사용 점프수)
    public int jumpCnt;

    //최대 점프횟수
    public int jumpMaxCnt;

    [Header("확인용")]
    //오른쪽
    public int isRight = 1;


    //경사
    public float slopeAngle;
    public Vector2 perp;

    //경사 확인
    public bool isSlope;

    //착지 확인
    bool isGround = false;

    

    Rigidbody2D rigid;
    Animator ani;
    BoxCollider2D boxCol;

    Stat stat;
    


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        stat = GetComponent<Stat>();
    }


    /// <summary>
    /// 경사체크
    /// 플레이어 앞 지형이 경사인지 체크
    /// </summary>
    void SlopeCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(front.position, Vector2.down, 0.9f, groundLayer);

        perp = Vector2.Perpendicular(hit.normal);

        slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

        if(hit)
        {
            if (slopeAngle != 0 && isGround)
            {
                isSlope = true;
            }
            else
            {
                isSlope = false;
            }
        }
    }



    /*
     * 플레이어
     * 
     * 기본공격시스템
     * 
     * 
     * 이동 관련
     * 
     * 대쉬 * 구르기(회피) * 벽점프
     * 
     * 
     * 시스템 관련
     * 
     * 장비 ( 강화(방어구), 내구도,수량, 신발 )
     * 상점
     * 환경설정(일시정지 기능, 저장,불러오기)
     * 캐릭터 선택? (본 애니메이션 가능여부에 따라 바꾸기)
     * 
     * 
     * 메인UI
     * 
     * 메인화면
     * 
     * 스테이지 선택
     * 
     * 
     * 기믹(장애물)
     * 
     * 움직이는 물체 (상자, 벽)
     * 사다리, 문
     * 
     * 각종 UI
     * 
     * 체력UI(몬스터 위에 UI 띄우기) - 있으면 좋음
     * 로딩씬
     * 점수, 시간제한 시스템
     * 
     * 
     * 중요
     * 
     * 맵 만들기
     */




    /// <summary>
    /// 플레이어 이동함수
    /// </summary>
    void MovePlayer()
    {
        //방향
        float dir = Input.GetAxisRaw("Horizontal");

        //이동방향
        switch (dir)
        {
            case (-1):
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                isRight = -1;
                break;
            case (1):
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                isRight = 1;
                break;
            case (0):
                ani.SetBool("Move", false);
                break;
        }

        // 경사일 경우 이동하는 방식
        if (dir != 0 && isSlope)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            transform.Translate(-perp * dir * speed * Time.deltaTime);

            ani.SetBool("Move", true);
        }

        //이동
        else if (dir != 0)
        {
            rigid.AddForce(Vector2.right * dir, ForceMode2D.Impulse);
            ani.SetBool("Move", true);
        }

        //속도 제한
        if (rigid.velocity.x > speed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        if (rigid.velocity.x < -speed)
        {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }

        //달리기 실행
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
    }

    /// <summary>
    /// 아이템 체크
    /// 현재 raycastAll을 이용 -> 레이어가 정해질 시 레이어 방식으로 바꿀것
    /// </summary>
    void ItemCheck()
    {
        //레이어방식
        //RaycastHit2D boxHit = Physics2D.BoxCast(player.transform.position, new Vector2(0.5f, 0.7f), 0f, new Vector2(isRight, 0), 1f, itemLayer);

        //태그방식
        RaycastHit2D[] boxHit = Physics2D.BoxCastAll(player.transform.position, new Vector2(0.5f, 0.7f), 0f, new Vector2(isRight, 0), 1f);

        if (boxHit != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //태그방식
                for (int i = 0; i < boxHit.Length; i++)
                {
                    if(boxHit[i].collider.tag == "Item")
                    {
                        inven.AddItem(boxHit[i].collider.gameObject);

                        break;
                    }
                }
                //레이어방식
                //Sprite itemSprite = boxHit.collider.GetComponent<SpriteRenderer>().sprite;
                //gameManager.GetItem(itemSprite);
                //itemOn = true;
            }
        }

        //아이템 사용
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("아이템 사용키 입력");
            inven.UseItem();
        }

        //아이템 스왑
        if(Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("퀵슬롯 체인지");
            inven.QuickSlotChange();
        }
    }

    //점프
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumpCnt > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            ani.SetTrigger("Jump");
            ani.SetBool("Fall", false);
            isGround = false;
            jumpCnt--;
        }
    }

    //플레이어 낙하확인
    void PlayerFall()
    {
        if(rigid.velocity.y < -1 && !isGround)
        {
            ani.SetBool("Fall", true);
        }
    }

    //그라운드체크
    void OnGround()
    {
        RaycastHit2D boxHit = Physics2D.BoxCast(player.transform.position, new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0.7f, groundLayer);

        if (boxHit)
        {
            ani.SetBool("OnGround", true);

            isGround = true;

            ani.SetBool("Fall", false);
        }
        else
        {
            isSlope = false;

            isGround = false;

            ani.SetBool("OnGround", false);
        }
    }


    /// <summary>
    /// 임시 충돌
    /// '
    /// ;
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground" && isGround)
        {
            jumpCnt = jumpMaxCnt;
        }
    }


    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        PlayerFall();
        SlopeCheck();
        OnGround();
        ItemCheck();
    }
}

