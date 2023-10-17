using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform fireEffect;
    public Transform waterEffect;
    public Transform lightEffect;
    public Transform darkEffect;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

}
