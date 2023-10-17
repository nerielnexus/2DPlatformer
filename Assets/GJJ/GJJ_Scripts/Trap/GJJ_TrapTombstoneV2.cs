using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TrapTombstoneV2 : MonoBehaviour
{
    // public
    public Transform _tombstoneImpact;
    public Transform _tombstoneReady;
    public GameObject _tombstonePrefab;

    // private
    [SerializeField] private float _tombstoneMoveSpeed = 0.04f;
    [SerializeField] private float _tombstoneDelayTime = 1.0f;
    [SerializeField] private bool _isTombstoneReady = false;
    [SerializeField] private GameObject _tombstoneInstance;
    [SerializeField] private int _tombstoneDamage = 2;

    private bool _loopControl = true;

    // method
    IEnumerator TombstoneAtReady()
    {
        if(_loopControl)
        {
            _loopControl = false;
            yield return new WaitForSeconds(_tombstoneDelayTime);
            _isTombstoneReady = true;
            _loopControl = true;
        }
    }

    IEnumerator TombstoneAtImpact()
    {
        if (_loopControl)
        {
            _loopControl = false;
            yield return new WaitForSeconds(_tombstoneDelayTime);
            _isTombstoneReady = false;
            _loopControl = true;
        }
    }

    void MoveTombstone()
    {
        if(_tombstoneInstance.transform.position == _tombstoneImpact.transform.position)
        {
            // _isTombstoneReady = false;
            StartCoroutine(TombstoneAtImpact());
        }
        else if(_tombstoneInstance.transform.position == _tombstoneReady.transform.position)
        {
            // _isTombstoneReady = true;
            StartCoroutine(TombstoneAtReady());
        }

        if(_isTombstoneReady)
            _tombstoneInstance.transform.position = Vector3.MoveTowards(_tombstoneInstance.transform.position,
                                                _tombstoneImpact.transform.position,
                                                _tombstoneMoveSpeed);
        else
            _tombstoneInstance.transform.position = Vector3.MoveTowards(_tombstoneInstance.transform.position,
                                                            _tombstoneReady.transform.position,
                                                            _tombstoneMoveSpeed);
    }

    public void CallFromTombstone()
    {
        // 대충 플레이어 체력을 깎는다는 내용
        Debug.Log("Tombstone OnTrigger");
    }

    // unity
    private void Start()
    {
        _tombstoneInstance = Instantiate(_tombstonePrefab, _tombstoneImpact.transform.position,Quaternion.identity);
        _tombstoneInstance.transform.SetParent(transform);
        _tombstoneInstance.GetComponent<GJJ_TrapTombstoneV2_OnTrigger>().GJJ_InitializeTombstoneInfo(gameObject);
    }

    private void Update()
    {
        // StartCoroutine(ActivateTombstone());
        MoveTombstone();
    }
}
