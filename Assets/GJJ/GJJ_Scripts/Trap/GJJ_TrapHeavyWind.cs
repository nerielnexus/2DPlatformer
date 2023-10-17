using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TrapHeavyWind : MonoBehaviour
{
    // private
    [SerializeField] private float gjj_WindForce = 2.0f;
    [SerializeField] private GameObject gjj_WindEffectPrefab;
    [SerializeField] private float windEffectDuration = 2.0f;
    [SerializeField] private float windEffectInterval = 4.0f;
    [SerializeField] private float windEffectModifier = 100.0f;
    [SerializeField] private WINDEFFECTPOWER windMode = WINDEFFECTPOWER.USER_MADE;
    [SerializeField] private WINDEFFECTDIRECTION windDirection = WINDEFFECTDIRECTION.LEFT;

    private GameObject gjj_ActualWindEffect;
    private RigidbodySleepMode2D playerSleepMode;
    private bool isWindBlowing = false;

    [Header("USE THIS VARIABLE ONLY FOR HEAVYWIND TEST")]
    [SerializeField] private KeyCode gjj_keycode;

    enum WINDEFFECTPOWER
    {
        USER_MADE,
        WEAK_LONG,
        HEAVY_SHORT
    }

    enum WINDEFFECTDIRECTION
    {
        RIGHT,
        LEFT
    };

    // method
    bool CheckPlayer(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return false;

        if (col.gameObject.layer != LayerMask.NameToLayer("Player"))
            return false;

        if (col.gameObject.name.Equals("Weapon-Sword"))
            return false;

        return true;
    }

    GameObject ReturnPlayerGameobject(Collider2D collider)
    {
        return CheckPlayer(collider) ? collider.gameObject : null;
    }

    void SetEffectPositionToCamera()
    {
        Vector3 _cameraPos = Camera.main.transform.position;
        gjj_ActualWindEffect.transform.position = new Vector3(_cameraPos.x, _cameraPos.y, -1.0f);
    }

    IEnumerator DoWindEffect(GameObject _player)
    {
        if (!isWindBlowing)
        {
            isWindBlowing = true;

            float _interval = 0f;
            float _duration = 0f;
            float _force = 0f;

            if(windMode == WINDEFFECTPOWER.USER_MADE)
            {
                _interval = windEffectInterval;
                _duration = windEffectDuration;
                _force = gjj_WindForce;
            }
            else if(windMode == WINDEFFECTPOWER.HEAVY_SHORT)
            {
                _interval = Random.Range(2.0f, 5.0f);
                _duration = Random.Range(0.5f, 1.2f);
                _force = Random.Range(2.0f, 4.0f);
            }
            else if(windMode == WINDEFFECTPOWER.WEAK_LONG)
            {
                _interval = Random.Range(1.0f, 2.5f);
                _duration = Random.Range(2.5f, 4.5f);
                _force = Random.Range(0.2f, 1.2f);
            }

            yield return new WaitForSeconds(_interval);

            gjj_ActualWindEffect.SetActive(true);
            

            if(windDirection == WINDEFFECTDIRECTION.RIGHT)
            {
                gjj_ActualWindEffect.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                _player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _force * windEffectModifier);
            }
            else
            {
                gjj_ActualWindEffect.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                _player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _force * windEffectModifier);
            }

            yield return new WaitForSeconds(_duration);

            gjj_ActualWindEffect.SetActive(false);

            isWindBlowing = false;
        }
    }

    void DoWindEffect_TEST(GameObject player)
    {
        if (Input.GetKey(gjj_keycode))
        {
            gjj_ActualWindEffect.SetActive(true);

            player.GetComponent<Rigidbody2D>().velocity
                += new Vector2(-gjj_WindForce, 0);
        }
        else
            gjj_ActualWindEffect.SetActive(false);
    }

    // unity
    private void Awake()
    {
        gjj_ActualWindEffect = Instantiate(gjj_WindEffectPrefab);
        gjj_ActualWindEffect.SetActive(false);
    }

    private void Update()
    {
        SetEffectPositionToCamera();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject _playerTmp = ReturnPlayerGameobject(collision);
        if (_playerTmp == null)
            return;

        StartCoroutine(DoWindEffect(_playerTmp));

        // DoWindEffect_TEST(_playerTmp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _playerTmp = ReturnPlayerGameobject(collision);
        if (_playerTmp == null)
            return;

        playerSleepMode = _playerTmp.GetComponent<Rigidbody2D>().sleepMode;
        _playerTmp.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject _playerTmp = ReturnPlayerGameobject(collision);
        if (_playerTmp == null)
            return;

        _playerTmp.GetComponent<Rigidbody2D>().sleepMode = playerSleepMode;
    }
}
