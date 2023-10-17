using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public bool[] stage;

    public static StageManager instance;

    void Awake()
    {
        instance = this;
    }
}
