using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearCheck : MonoBehaviour
{
    public GameObject[] stars;
    public Sprite fullStar;
    public Sprite emptyStar;
    StageManager stagemanager;

    void Start()
    {
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {
        for (int i=0; i<stars.Length; i++)
        {
            if (stagemanager.stage[i] == true)
            {
                stars[i].GetComponent<Image>().sprite = fullStar;
            }
            else
            {
                stars[i].GetComponent<Image>().sprite = emptyStar;
            }
        }
    }
}
