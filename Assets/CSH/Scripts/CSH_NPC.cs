using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSH_NPC : MonoBehaviour
{
    public Fungus.Flowchart flowchart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SayDialog( Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance < 5f)
        {
            flowchart.FindBlock("NPC_0011_03").StartExecution();

            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
        else
        {
            Debug.Log("너무 멀다");
        }
    }
}
