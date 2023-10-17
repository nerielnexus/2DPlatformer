using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 설명 : 무적 물약을 적용시.. 플레이어 무적상태..
public class KHS_Player_Invin : MonoBehaviour
{
    public Tilemap tile;
    public bool isInvi = false;

    public static KHS_Player_Invin instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(!isInvi)
        {
            Collider2D overCol = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 1.5f), 0, LayerMask.GetMask("Water"));

            if (overCol != null)
            {
                Vector3Int cell = tile.WorldToCell(transform.position);

                tile.SetTile(cell, null);
            }
        }
    }
}
