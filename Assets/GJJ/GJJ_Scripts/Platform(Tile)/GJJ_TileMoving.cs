using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TileMoving : MonoBehaviour
{
    // public

    // private
    private List<GameObject> _listWaypoints;
    private float _lifeTime;
    private float _moveSpeed;
    private int _waypointIndex;
    private TILERALLYTYPE _tileRallyType;
    private bool _playerOnTile = false;
    private PlatformEffector2D _tileEffector2D;
    private GameObject _player;
    private bool _tilePingpongStatus = false;

    // method
    public void InitializeTileInfo(List<GameObject> _list, float _life, float _move, TILERALLYTYPE _rally)
    {
        _listWaypoints = _list;
        _lifeTime = _life;
        _moveSpeed = _move;
        _tileRallyType = _rally;

        _waypointIndex = 0;
    }

    void MoveTileAsStraight()
    {
        if (transform.position == _listWaypoints[_waypointIndex].transform.position)
        {
            if (_waypointIndex < _listWaypoints.Count - 1) _waypointIndex++;
            else
            { 
                _waypointIndex = _listWaypoints.Count - 1;
                DestroyTile();
            }
        }

        transform.position = Vector2.MoveTowards(transform.position,
            _listWaypoints[_waypointIndex].transform.position,
            _moveSpeed);
    }

    void MoveTileAsLoop()
    {
        if(transform.position == _listWaypoints[_waypointIndex].transform.position)
        {
            if (_waypointIndex < _listWaypoints.Count - 1) _waypointIndex++;
            else _waypointIndex = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position,
            _listWaypoints[_waypointIndex].transform.position,
            _moveSpeed);
    }

    void MoveTileAsPingpong()
    {
        if(!_tilePingpongStatus)
        {
            if (transform.position == _listWaypoints[_waypointIndex].transform.position)
            {
                if (_waypointIndex < _listWaypoints.Count - 1) _waypointIndex++;
                else _tilePingpongStatus = true;
            }
        }
        else
        {
            if (transform.position == _listWaypoints[_waypointIndex].transform.position)
            {
                if (_waypointIndex > 0) _waypointIndex--;
                else _tilePingpongStatus = false;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position,
            _listWaypoints[_waypointIndex].transform.position,
            _moveSpeed);
    }

    void DestroyTile()
    {
        if(_lifeTime >= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, _lifeTime);
        }
    }

    IEnumerator ActivateCollider()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        GetComponent<Collider2D>().enabled = true;
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
        if (CheckPlayer(collision))
        {
            collision.transform.SetParent(transform);
            _playerOnTile = true;
        } 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CheckPlayer(collision))
        {
            collision.transform.SetParent(null);
            _playerOnTile = false;
        }
    }

    private void Awake()
    {
        transform.SetParent(GameObject.Find("Grid").transform);
        _tileEffector2D = GetComponent<PlatformEffector2D>();
        _player = GameObject.Find("PlayerMan");
    }

    private void Update()
    {

        if (_tileRallyType == TILERALLYTYPE.CIRCLE)
            MoveTileAsLoop();
        else if (_tileRallyType == TILERALLYTYPE.STRAIGHT)
            MoveTileAsStraight();
        else if (_tileRallyType == TILERALLYTYPE.PINGPONG)
            MoveTileAsPingpong();

        if(_playerOnTile && Input.GetKeyDown(KeyCode.X))
            StartCoroutine(ActivateCollider());
    }
}
