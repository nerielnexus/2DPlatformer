using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TrapLightningMagic : MonoBehaviour
{
    // public

    // private
    [SerializeField] private Transform gjj_TrapTransformStart;
    [SerializeField] private Transform gjj_TrapTransformEnd;
    [SerializeField] private GameObject gjj_TrapEffectCloud;
    [SerializeField] private GameObject gjj_TrapEffectLightningInactive;
    [SerializeField] private GameObject gjj_TrapEffectLightningActive;

    private GameObject gjjInstance_Cloud;
    private GameObject gjjInstance_LightningInactive;
    private GameObject gjjInstance_LightningActive;

    [SerializeField] private float gjj_IntervalTime_Cloud = 2.0f;
    [SerializeField] private float gjj_IntervalTime_Lightning = 1.0f;
    [SerializeField] private float gjj_IntervalTime_Damage = 1.5f;
    [SerializeField] private int gjj_IntervalMultiplier_Lightning = 3;


    private bool loopFlag_LightningTrapLoop = false;
    private bool loopFlag_LightningHit = false;

    [SerializeField] private bool gjj_PlayerInArea = false;
    [SerializeField] private bool gjj_LightningActivated = false;
    [SerializeField] private RigidbodySleepMode2D gjj_PlayerRB2DSleepMode;

    // method
    void Initialize_LightningTrap()
    {
        gjj_TrapEffectCloud.transform.position = gjj_TrapTransformStart.position;
        gjj_TrapEffectCloud.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        gjj_TrapEffectLightningInactive.transform.GetChild(0).position = gjj_TrapTransformStart.position;
        gjj_TrapEffectLightningInactive.transform.GetChild(1).position = gjj_TrapTransformEnd.position;

        gjj_TrapEffectLightningActive.transform.GetChild(0).position = gjj_TrapTransformStart.position;
        gjj_TrapEffectLightningActive.transform.GetChild(1).position = gjj_TrapTransformEnd.position;

        gjjInstance_Cloud = Instantiate(gjj_TrapEffectCloud);
        gjjInstance_LightningInactive = Instantiate(gjj_TrapEffectLightningInactive);
        gjjInstance_LightningActive = Instantiate(gjj_TrapEffectLightningActive);

        gjjInstance_Cloud.transform.SetParent(gameObject.transform);
        gjjInstance_LightningInactive.transform.SetParent(gameObject.transform);
        gjjInstance_LightningActive.transform.SetParent(gameObject.transform);

        gjjInstance_Cloud.SetActive(false);
        gjjInstance_LightningInactive.SetActive(false);
        gjjInstance_LightningActive.SetActive(false);
    }

    IEnumerator LightningTrapLoop()
    {
        if(!loopFlag_LightningTrapLoop)
        {
            loopFlag_LightningTrapLoop = true;

            yield return new WaitForSeconds(Random.Range(1.0f, 10.0f));
            gjjInstance_Cloud.SetActive(true);
            yield return new WaitForSeconds(gjj_IntervalTime_Cloud);
            gjjInstance_LightningInactive.SetActive(true);
            yield return new WaitForSeconds(gjj_IntervalTime_Lightning);
            gjjInstance_LightningInactive.SetActive(false);
            gjjInstance_LightningActive.SetActive(true);
            gjj_LightningActivated = true;
            yield return new WaitForSeconds(gjj_IntervalTime_Lightning * gjj_IntervalMultiplier_Lightning);
            gjjInstance_LightningActive.SetActive(false);
            gjj_LightningActivated = false;
            yield return new WaitForSeconds(gjj_IntervalTime_Cloud);
            gjjInstance_Cloud.SetActive(false);
            yield return new WaitForSeconds(Random.Range(1.0f, 10.0f));

            loopFlag_LightningTrapLoop = false;
        }
    }

    GameObject CheckPlayer(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player"))
            return null;

        if (collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return null;

        if (collider.gameObject.name.Equals("Weapon-Sword"))
            return null;

        return collider.gameObject;
    }

    IEnumerator LightningHit()
    {
        if (!loopFlag_LightningHit)
        {
            loopFlag_LightningHit = true;

            // 플레이어가 번개를 맞고 대미지를 입음
            // 이후 IntervalTime_Damage 동안 코루틴 대기 후 또 그 안에 있으면 번개 대미지를 입음
            Debug.Log("[Lightning] player hit");
            yield return new WaitForSeconds(gjj_IntervalTime_Damage);

            loopFlag_LightningHit = false;
        }
    }

    // unity
    private void Awake()
    {
        Initialize_LightningTrap();
    }

    private void Update()
    {
        StartCoroutine(LightningTrapLoop());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _playerTmp = CheckPlayer(collision);

        if (_playerTmp == null)
            return;

        gjj_PlayerInArea = true;
        gjj_PlayerRB2DSleepMode = _playerTmp.GetComponent<Rigidbody2D>().sleepMode;
        _playerTmp.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject _playerTmp = CheckPlayer(collision);

        if (_playerTmp == null)
            return;

        if (gjj_LightningActivated && gjj_PlayerInArea)
            StartCoroutine(LightningHit());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject _playerTmp = CheckPlayer(collision);

        if (_playerTmp == null)
            return;

        gjj_PlayerInArea = false;
        _playerTmp.GetComponent<Rigidbody2D>().sleepMode = gjj_PlayerRB2DSleepMode;
    }
}
