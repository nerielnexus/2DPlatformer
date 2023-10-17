using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILEFALLTYPE
{
    NOFALL = 0,
    NORESPAWN,
    CANRESPAWN
};

public class GJJ_TileFalling : MonoBehaviour
{
    // public
    public TILEFALLTYPE tileFallingType = TILEFALLTYPE.NOFALL;

    // private
    [SerializeField] private float tileFallingTimeDelay = 1.0f;
    [SerializeField] private float tileFallingTime = 3.0f;
    private Vector2 tileInitialPosition;
    private Rigidbody2D tileRB2D;

    // method
    bool CheckPlayer(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return false;

        int gjj_YouGotLayer = LayerMask.NameToLayer("Player");
        if (col.gameObject.layer != gjj_YouGotLayer) return false;

        if (col.gameObject.name.Equals("PlayerMan"))
            return true;

        return true;
    }

    IEnumerator TileFallingLoop()
    {
        yield return new WaitForSeconds(tileFallingTimeDelay);

        tileRB2D.isKinematic = false;
        foreach (Collider2D col in GetComponents<Collider2D>())
            col.enabled = false;

        yield return new WaitForSeconds(tileFallingTime);

        if (tileFallingType == TILEFALLTYPE.NORESPAWN)
            Destroy(gameObject);
        else if(tileFallingType == TILEFALLTYPE.CANRESPAWN)
        {
            gameObject.SetActive(false);
            tileRB2D.isKinematic = true;
            transform.position = tileInitialPosition;

            foreach (Collider2D col in GetComponents<Collider2D>())
                col.enabled = true;

            gameObject.SetActive(true);
        }
    }

    // unity
    private void Awake()
    {
        tileInitialPosition = transform.position;
        tileRB2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CheckPlayer(collision)) return;

        if(tileFallingType != TILEFALLTYPE.NOFALL)
            StartCoroutine(TileFallingLoop());
    }
}
