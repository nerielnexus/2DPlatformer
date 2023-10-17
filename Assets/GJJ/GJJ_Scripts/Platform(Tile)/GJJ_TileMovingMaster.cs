using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILERALLYTYPE
{
    NONE = 0,
    STRAIGHT,
    PINGPONG,
    CIRCLE
};

public enum TILESPAWNTYPE
{
    NONE = 0,
    ONLYONE,
    INFINITE
};

public class GJJ_TileMovingMaster : MonoBehaviour
{
    // public
    public GameObject _tile;
    public TILERALLYTYPE _tileRallyType = TILERALLYTYPE.NONE;
    public TILESPAWNTYPE _tileSpawnType = TILESPAWNTYPE.NONE;

    // private
    [SerializeField] private float _tileMoveSpeed = 0.2f;
    [SerializeField] private float _tileLifeTime = 1.0f;
    [SerializeField] private float _tileSpawnInterval = 1.0f;

    private List<GameObject> _listWaypoints;
    private bool _tileSpawnOnLoop = true;

    // method
    void AddWaypoints()
    {
        for (int i = 0; i < transform.childCount; i++)
            _listWaypoints.Add(transform.GetChild(i).gameObject);
    }

    IEnumerator SpawnTileLoop()
    {
        if (_tileSpawnOnLoop)
        {
            _tileSpawnOnLoop = false;
            Instantiate(_tile, _listWaypoints[0].transform.position, Quaternion.identity)
                .GetComponent<GJJ_TileMoving>()
                .InitializeTileInfo(_listWaypoints, _tileLifeTime, _tileMoveSpeed, _tileRallyType);
            Debug.Log("tile spawn");
            yield return new WaitForSeconds(_tileSpawnInterval);
            _tileSpawnOnLoop = true;
        }
    }

    void SpawnTile()
    {
        if (_tileSpawnType == TILESPAWNTYPE.ONLYONE)
        {
            Instantiate(_tile, _listWaypoints[0].transform.position, Quaternion.identity)
                .GetComponent<GJJ_TileMoving>()
                .InitializeTileInfo(_listWaypoints, _tileLifeTime, _tileMoveSpeed, _tileRallyType);
            _tileSpawnType = TILESPAWNTYPE.NONE;
        }
        else if (_tileSpawnType == TILESPAWNTYPE.INFINITE)
        {
            StartCoroutine(SpawnTileLoop());
        }
    }

    // unity
    private void Awake()
    {
        _listWaypoints = new List<GameObject>();
    }

    private void Start()
    {
        AddWaypoints();
    }

    private void Update()
    {
        SpawnTile();
    }
}
