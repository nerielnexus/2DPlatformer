using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TrapTombstoneV2_OnTrigger : MonoBehaviour
{
    [SerializeField] private GJJ_TrapTombstoneV2 _parent;

    public void GJJ_InitializeTombstoneInfo(GameObject _obj)
    {
        _parent = _obj.GetComponent<GJJ_TrapTombstoneV2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.name.Equals("Player"))
        {
            _parent.CallFromTombstone();
        }
    }
}
