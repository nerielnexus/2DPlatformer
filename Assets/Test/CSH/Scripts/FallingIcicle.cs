using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIcicle : MonoBehaviour
{
    RaycastHit2D rayhit;
    Rigidbody2D _rigid;

    public int deep = 1;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer())
        {
            _rigid.gravityScale = 1;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 center = new Vector3(transform.position.x, transform.position.y - (deep / 2), transform.position.z);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector3(2, deep, 0));
    }

    bool isPlayer()
    {
        Vector3 center = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);

        rayhit = Physics2D.BoxCast(center, new Vector2(2, 5), 0f, Vector2.zero, 0f, LayerMask.GetMask("Player"));

        return rayhit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground" || collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
