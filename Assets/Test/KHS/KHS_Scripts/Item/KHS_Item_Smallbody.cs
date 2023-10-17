using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 즉발형 아이템, 플레이어의 scale이 절반 크기로 줄어든다.

public class KHS_Item_Smallbody : UseItem
{
    GameObject player;
    Vector3 scale;

    bool smallbody = false;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scale = player.transform.localScale;

        KHS_ItemManager.instance.CoolTime_BigBody(gameObject);
        smallbody = true;
    }

    void Update()
    {
        if(smallbody == true)
        {
            if(scale.y >= 0.5f && scale.z >= 0.5f)
            {
                scale.y -= Time.deltaTime;
                scale.z -= Time.deltaTime;
                player.transform.localScale = scale;
            }
        }
    }
}
