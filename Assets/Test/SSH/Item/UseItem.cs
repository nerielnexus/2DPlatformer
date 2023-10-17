using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseItem : Item
{
    //아이템 리지드바디
    public Rigidbody2D rigid;


    public virtual void Use()
    {
    }
}