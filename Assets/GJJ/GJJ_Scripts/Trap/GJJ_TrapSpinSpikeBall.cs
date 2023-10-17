using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TrapSpinSpikeBall : MonoBehaviour
{
    // public

    // private
    [SerializeField] private GameObject _spikeBall;
    [SerializeField] private Transform _anchor;
    [SerializeField] private float _spinTime = 100.0f;
    [SerializeField] private float _spikeMinimumArmLength = 1.0f;
    [SerializeField] private int _spikeBallCount = 4;
    [SerializeField] private List<GameObject> _listSpikeBalls;

    // method

    void CreateSpikes()
    {
        for (int i = 0; i < _spikeBallCount; i++)
        {
            GameObject tmp = Instantiate(_spikeBall, _anchor.position
                + new Vector3(_spikeMinimumArmLength * i, 0, 0) , Quaternion.identity);
            tmp.transform.SetParent(transform);
            _listSpikeBalls.Add(tmp);
        }
    }

    void MoveSpikes()
    {
        foreach(GameObject tmp in _listSpikeBalls)
        {
            tmp.transform.RotateAround(_anchor.position, Vector3.forward, _spinTime * Time.deltaTime);
        }
    }
    
    // unity
    private void Awake()
    {
        _listSpikeBalls = new List<GameObject>();

        CreateSpikes();
    }

    private void Update()
    {
        MoveSpikes();
    }
}
