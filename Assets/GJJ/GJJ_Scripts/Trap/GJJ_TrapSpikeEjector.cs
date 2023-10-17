using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TrapSpikeEjector : MonoBehaviour
{
    /* Spike Ejector Trap
     * 
     * 1. 뭐 하는 트랩이냐
     * 가시를 품고 있는 사각형 모양의 트랩
     * 플레이어가 일정 거리 안으로 다가오면 가시를 뱉을랑말랑 하며 간 보다가,
     * 더 가까이 다가오면 가시를 내밀어서 플레이어를 공격함
     * 스펠렁키의 Tiki Trap
     * 
     * 2. 필요한게 무엇이냐
     * 
     * 2-1. 사각형 박스 모양의 물체
     * 가시를 품고 있어야함
     * 플레이어가 발판으로 이용할 수 있어야 함
     * 
     * 2-2. 가시
     * 사각형 박스 안에 들어가 있음
     * 사각형 박스에서 절대로 떨어지지 않음
     * 플레이어가 가시에 닿은 경우 가시->박스 순으로 플레이어와 충돌했다고 전달함
     * 
     * 2-3. ray 객체 2개
     * 플레이어가 얼마나 다가왔는지 체크해야함
     * ray_warn (긴 ray, 플레이어가 다가오면 가시를 뱉을랑말랑 함)
     * ray_kill (짧은 ray, 플레이어가 가까이 다가오면 가시를 뱉는데 사용함)
     * 
     * 
     */

    // public
    public SpriteRenderer _signal;
    public Transform _rayPoint;
    public Transform[] _spikes;

    // private
    [SerializeField] private int _activeSignalValue = 0;
    [SerializeField] private int _activeSignalValuePrev = 0;
    [SerializeField] private float _rayWarnLength = 1.0f;
    [SerializeField] private float _rayKillLengthRatio = 0.4f;
    [SerializeField] private Vector2 _spikeWarnPos;
    [SerializeField] private Vector2 _spikeKillPos;

    // ray 2d
    [SerializeField] private Ray2D _rayWarn;
    [SerializeField] private RaycastHit2D[] _raycastWarn;

    [SerializeField] private Vector2[] _spikesInitPos;

    // method
    void ChangeColor()
    {
        if (_activeSignalValue == 0)
            _signal.color = new Color(1, 0, 0);
        else if (_activeSignalValue == 1)
            _signal.color = new Color(1, 1, 0);
        else if (_activeSignalValue == 2)
            _signal.color = new Color(0, 1, 0);
    }

    void FireRayToCheckPlayer()
    {
        Vector2 origin = _rayPoint.transform.position;
        Vector2 dest = GameObject.Find("Player").transform.position;
        Vector2 norm = ((dest + new Vector2(0, 1.0f)) - origin).normalized;

        _rayWarn = new Ray2D(origin, norm);
        Debug.DrawRay(origin, norm * _rayWarnLength, Color.red);

        _raycastWarn = Physics2D.RaycastAll(origin, norm, _rayWarnLength, LayerMask.GetMask("Player"));

        foreach(RaycastHit2D _tmpHit in _raycastWarn)
        {
            GameObject _tmpObj = _tmpHit.collider.gameObject;
            float _dist = Vector2.Distance(origin, _tmpObj.transform.position);

            if (_tmpObj.CompareTag("Player") && _tmpObj.name.Equals("Player"))
            {
                if (_dist > _rayWarnLength * _rayKillLengthRatio && _dist <= _rayWarnLength)
                    _activeSignalValue = 1;
                else if (_dist <= _rayWarnLength * _rayKillLengthRatio)
                    _activeSignalValue = 2;
                else
                    _activeSignalValue = 0;
            }
        }
    }

    void SetSignalValue(int value)
    {

    }

    void MoveSpike()
    {
        float moveSteb = 0.01f;
        for(int i=0; i<_spikes.Length; i++)
        {
            Vector2 _dir = ((Vector2)_spikes[i].position - (_spikesInitPos[i] * moveSteb * _activeSignalValue)).normalized;
            _spikes[i].Translate(_dir);
        }
    }

    // unity
    private void Awake()
    {
        _spikesInitPos = new Vector2[_spikes.Length];
        for (int i = 0; i < _spikes.Length; i++)
            _spikesInitPos[i] = _spikes[i].position;
    }
    private void Update()
    {
        ChangeColor();
        FireRayToCheckPlayer();

        MoveSpike();
    }
}
