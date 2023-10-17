using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SSH_MonMove : ScriptableObject
{
    public Transform pos;

    public abstract void Move();
}
