using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TileSuperJump : MonoBehaviour
{
    // public

    // private
    [SerializeField] private float gjjJumpForce = 30.0f;

    // method
    bool CheckPlayer(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return false;

        int _layerTmp = LayerMask.NameToLayer("Player");
        if (col.gameObject.layer != _layerTmp)
            return false;

        return true;
    }

    bool CheckPlayer(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return false;

        int _layerTmp = LayerMask.NameToLayer("Player");
        if (col.gameObject.layer != _layerTmp)
            return false;

        return true;
    }

    // unity
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CheckPlayer(collision))
            return;

        Rigidbody2D _tmprb = collision.gameObject.GetComponent<Rigidbody2D>();
        _tmprb.velocity += new Vector2(_tmprb.velocity.x, gjjJumpForce);
    }
}
