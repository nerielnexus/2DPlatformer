using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TileCondition : MonoBehaviour
{
    /*  TileCondition
     *      타일(플랫폼)이 특정 조건을 만족하면 특정 행동을 하도록 만드는 스크립트
     */

    enum TILECONDITIONTYPE
    {
        NONE = 0,
        NOT_VISIBLE,
        FALLING
    };

    // public

    // private
    [SerializeField] private GameObject _player;
    [Header("Tile 이 나타나거나 사라지는 조건을 설정합니다.")]
    [SerializeField] private TILECONDITIONTYPE _condition = TILECONDITIONTYPE.NONE;

    // method
    void GJJ_ModifyTileViaCondition()
    {
        if (_condition == TILECONDITIONTYPE.NONE)
            return;

        if(_condition == TILECONDITIONTYPE.NOT_VISIBLE)
        {
            GetComponent<UnityEngine.Tilemaps.TilemapRenderer>().enabled = false;
            return;
        }

        if (_condition == TILECONDITIONTYPE.FALLING)
        {
            GJJ_TileFalling falltmp = GetComponent<GJJ_TileFalling>();

            if (falltmp.tileFallingType == TILEFALLTYPE.NOFALL)
                falltmp.tileFallingType = TILEFALLTYPE.NORESPAWN;

            return;
        }
    }

    void TileConditionResolve_NotVisible()
    {
        if (_condition != TILECONDITIONTYPE.NOT_VISIBLE)
            return;

        if (Input.GetKey(KeyCode.Alpha1))
            GetComponent<UnityEngine.Tilemaps.TilemapRenderer>().enabled = true;
        else
            GetComponent<UnityEngine.Tilemaps.TilemapRenderer>().enabled = false;

        if (transform.childCount <= 0)
            return;

        if(transform.GetChild(0).gameObject == _player)
            GetComponent<UnityEngine.Tilemaps.TilemapRenderer>().enabled = true;
    }

    void DisableTileCondition()
    {
        GetComponent<UnityEngine.Tilemaps.TilemapRenderer>().enabled = true;
        GetComponent<GJJ_TileCondition>().enabled = false;
    }

    // unity
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        GJJ_ModifyTileViaCondition();
    }

    private void Update()
    {
        TileConditionResolve_NotVisible();
    }
}
