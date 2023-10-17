using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHS_Item_FootBox : UseItem
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.position = player.transform.position + new Vector3((1 * Player.instance.isRight), 0, 0);
    }
}
