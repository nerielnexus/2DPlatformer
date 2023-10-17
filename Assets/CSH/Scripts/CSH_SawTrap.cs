using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSH_SawTrap : MonoBehaviour
{
    Transform spot00;
    Transform spot01;
    Transform saw;

    bool spot0tag = false;

    private void Awake()
    {
        spot00 = transform.GetChild(0);
        spot01 = transform.GetChild(1);
        saw = transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        SawSpin();
        SawMove();
    }

    void SawMove()
    {
        if (!spot0tag)
        {
            Vector3 dir = spot00.position - saw.position;
            saw.position = Vector3.MoveTowards(saw.position, spot00.position, 1f * Time.deltaTime);
            if (dir == Vector3.zero) spot0tag = true;            
        }
        else
        {
            Vector3 dir = spot01.position - saw.position;
            saw.position = Vector3.MoveTowards(saw.position, spot01.position, 1f * Time.deltaTime);
            if (dir == Vector3.zero) spot0tag = false;
        }
    }

    void SawSpin()
    {
        saw.Rotate(new Vector3(0, 0, -360 * Time.deltaTime));
    }
}
