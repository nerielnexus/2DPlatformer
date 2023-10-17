using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_Player : MonoBehaviour
{
    Animator ani;
    SpriteRenderer sprite;
    Rigidbody2D rigid;

    [Header("-- 움직임 --")]
    public float speed;
    float dir;

    [Header("-- 점프 --")]
    public int jumpCnt;
    public float jumpPower;

    [Header("-- 그라운드 확인 --")]
    public LayerMask layer;
    RaycastHit2D hit;

    [Header("-- 키 확인 --")]
    static public int keyCnt;

    [Header("-- (확인용) --")]
    public int hp;
    public GameObject item;
    public GameObject bombPos;

    public static KHS_Player instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }

    void Start()
    {
        jumpCnt = 1;
        keyCnt = 0;

        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dir = Input.GetAxis("Horizontal");
        
        GroundCheck();
        Jump();

        //Debug.Log(jumpCnt);

        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(item);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(dir != 0)
        {
            transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
            ani.SetBool("KHS_isRun", true);

            if(dir < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        else
        {
            ani.SetBool("KHS_isRun", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && jumpCnt > 0 && hit.collider != null)
        {
            rigid.velocity = Vector2.zero;
            jumpCnt--;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            ani.SetBool("KHS_isRun", false);
        }
    }
   

    void GroundCheck()
    {
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.55f), Vector2.down, 0.2f, layer);

        if (hit.collider != null)
        {
            ani.SetBool("KHS_isJump", false);
            if(jumpCnt == 0)
            {
                jumpCnt++;
            }
        }
        else
        {
            ani.SetBool("KHS_isJump", true);
        }
    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Key")
        {
            other.gameObject.SetActive(false);
            keyCnt++;
        }
    }


    void OnDrawGizmos()
    {
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.55f), Vector2.down * 0.3f, Color.red);
    }
}
