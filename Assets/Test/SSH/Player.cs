using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    [Header("장착한 무기")]
    public EquipData weapon;
    [Header("장착한 방어구")]
    public EquipData armor;

    [Header("캐릭터 스텟")]
    //기본속도
    public float normalSpeed;

    //달리기속도
    public float runSpeed;

    //최대속도
    public float maxSpeed;

    //점프힘
    public float jumpPower;

    //점프횟수(실제 사용 점프수)
    public int jumpCnt;

    //최대 점프횟수
    public int jumpMaxCnt;

    //캐릭터 체력
    public int playerHp;
    int playerMaxHp; //최대 체력 저장용

    //캐릭터 방어력
    public int playerDef;

    //캐릭터 불내성
    public int fireProof;
    //캐릭터 물내성
    public int waterProof;
    //캐릭터 빛내성
    public int lightProof;
    //캐릭터 어둠내성
    public int darkProof;

    //캐릭터 추가체력
    public int playerShield;

    //캐릭터 목숨
    public int playerLife;

    [Header("확인용")]
    //오른쪽
    public int isRight = 1;

    //착지 확인
    bool isGround = false;


    Rigidbody2D rigid;
    Animator ani;
    BoxCollider2D boxCol;

    Stat stat;

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        inven = InvenUI.invenUI;
        playerMaxHp = playerHp;

        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        stat = GetComponent<Stat>();

        if (EquipInven.equipInven.waeponSlot.itemOn)
        {
            MatchWeaponValue();
        }
        else
        {
            weapon = null;
        }

        if(EquipInven.equipInven.armorSlot.itemOn)
        {
            armor = EquipInven.equipInven.armorSlot.item as EquipData;
            MatchArmorValue(armor);
        }
        else
        {
            armor = null;
        }
    }

    public void MatchWeaponValue()
    {
        weapon = EquipInven.equipInven.waeponSlot.item as EquipData;

        if(weapon == true)
        {
            transform.GetComponent<PlayerAttackable>().EquipWeapon(weapon);
        }
        else
        {
            transform.GetComponent<PlayerAttackable>().UnEquipWeapon();
        }
        
    }

    //아머의 방어 관련 수치를 적용
    public void MatchArmorValue(EquipData armorItem)
    {
        if(armorItem == true)
        {
            armor = armorItem;

            playerDef = armorItem.value;
            fireProof = armorItem.fire;
            waterProof = armorItem.water;
            lightProof = armorItem.light;
            darkProof = armorItem.dark;

            playerShield = armorItem.durability;
        }
        else
        {
            armor = null;

            playerDef = 0;
            fireProof = 0;
            waterProof = 0;
            lightProof = 0;
            darkProof = 0;

            playerShield = 0;
        }
    }


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
        

        //이동
        if (dir != 0)
        {
            rigid.AddForce(Vector2.right * dir, ForceMode2D.Impulse);
            ani.SetBool("Move", true);
        }

        //속도 제한
        if (dir != 0 && rigid.velocity.x > speed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        if (dir != 0 && rigid.velocity.x < -speed)
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                //태그방식
                for (int i = 0; i < boxHit.Length; i++)
                {
                    if (boxHit[i].collider.tag == "Item")
                    {
                        inven.AddItem(boxHit[i].collider.gameObject);
                        boxHit[i].collider.gameObject.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("아이템 사용키 입력");
            inven.UseItem();
        }

        //아이템 스왑
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("퀵슬롯 체인지");
            inven.QuickSlotChange();
        }
    }

    //점프
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            isGround = false;
            jumpCnt--;
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

            //ani.SetBool("Fall", false);
        }
        else
        {
            isGround = false;

            ani.SetBool("OnGround", false);
        }
    }

    //플레이어가 체력이 다했을 때의 함수
    void PlayerLostLife()
    {
        if(playerHp <= 0)
        {
            transform.GetComponent<GJJ_CheckpointEventModule>().InvokePlayerDeathEvent(); //플레이어를 체크포인트로
            playerHp = playerMaxHp;
            playerLife--;
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
        if (other.gameObject.tag == "Ground" && isGround)
        {
            jumpCnt = jumpMaxCnt;
        }
    }


    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        OnGround();
        ItemCheck();
        PlayerLostLife();
       
    }
}
