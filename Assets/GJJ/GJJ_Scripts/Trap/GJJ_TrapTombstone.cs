using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TrapTombstone : MonoBehaviour
{
    // public

    // private
    [SerializeField] private float _fallSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private GJJ_Checkpoint_EventTestPlayerScript _player;
    [SerializeField] private bool isTombstoneReloaded;

    // method

    void GJJ_MoveTombstone()
    {
        if (!isTombstoneReloaded)
        {
            this.transform.Translate(Vector2.up * _fallSpeed * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(Vector2.up * -_fallSpeed * Time.deltaTime);
        }
    }

    IEnumerator GJJ_SetUpward()
    {
        while (true)
        {
            _fallSpeed = 10.0f;
            isTombstoneReloaded = false;
            yield return new WaitForSeconds(0.5f);
            _fallSpeed = 0.0f;
            yield return new WaitForSeconds(0.5f);
            _fallSpeed = 10.0f;
            isTombstoneReloaded = true;
            yield return new WaitForSeconds(0.5f);
            _fallSpeed = 0.0f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    // unity
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.name == "Player")
        {
            _player.gjjHealth -= _damage;
            _player.GJJ_PlayHitAnim();
        }
    }

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<GJJ_Checkpoint_EventTestPlayerScript>();
        _damage = 5;
        _fallSpeed = 10.0f;
        isTombstoneReloaded = false;
        StartCoroutine(GJJ_SetUpward());
    }

    private void Update()
    {
        GJJ_MoveTombstone();
    }
}
