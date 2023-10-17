using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public int exp;

    // Start is called before the first frame update
    void Start()
    {
        exp = 3000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int gain)
    {
        exp = exp + gain;
    }

    public void LostExp(int lost)
    {
        exp = exp - lost;
        if (exp < 0) exp = 0;
    }

}
