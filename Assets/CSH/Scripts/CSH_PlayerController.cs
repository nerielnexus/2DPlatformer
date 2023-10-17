using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSH_PlayerController : MonoBehaviour
{
    //Rigidbody2D _rigid;

    float speed = 5;

    CharacterController cc;

    private Transform npc = null;

    private void Awake()
    {
        //_rigid = GetComponent<Rigidbody2D>();

        cc = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        //Jump();

        //HitTester();

        Vector3 move = Vector3.zero;
        move.x = Input.GetAxis("Horizontal");
        move.y = 0;
        move.z = Input.GetAxis("Vertical");
        cc.Move(move * Time.deltaTime * 10f);

        if (Input.GetMouseButtonUp(0) && IsNpc())
        {
            //Debug.Log("NPC 추출");
            npc.GetComponent<CSH_NPC>().SayDialog(this.transform);
        }

    }

    //마우스 포지션 검출
    bool IsNpc()
    {
        bool isNPC = false;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if( Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            if(hit.transform.tag == "Enemy")
            {
                isNPC = true;
                npc = hit.transform;
            }
        }


        return isNPC;
    }

    /*
    void Move()
    {
        float keyS = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //_rigid.velocity += new Vector2(keyS, 0);
        //_rigid.AddForce(new Vector2(keyS, 0), ForceMode2D.Force);
        transform.Translate(new Vector2(keyS, 0));
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigid.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down);
    }

    void HitTester()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector3.down);

        for(int i = 0; i < hit.Length; i++)
        {
            Debug.Log(hit[i].collider.gameObject.name);
        }
        


    }

    */
}
