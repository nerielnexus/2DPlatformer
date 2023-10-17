using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallButton : MonoBehaviour
{
    DataManager gameManger;
    public Button button;

    void Start()
    {
        gameManger = GameObject.Find("GameManager").GetComponent<DataManager>();

        Click();
    }

    void Click()
    {
        button.onClick.AddListener(() => gameManger.LoadData());
    }
}
