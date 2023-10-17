using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreemoneyUI : MonoBehaviour
{
    public Transform upgradeUI;
    public Transform materialUI;
    public Transform craftUI;

    // Start is called before the first frame update
    void Start()
    {
        MainboardReset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MainboardReset()
    {
        upgradeUI.gameObject.SetActive(false);
        materialUI.gameObject.SetActive(false);
        craftUI.gameObject.SetActive(false);
    }

    public void UpgradeUIOn()
    {
        upgradeUI.gameObject.SetActive(true);
        materialUI.gameObject.SetActive(false);
        craftUI.gameObject.SetActive(false);
    }

    public void MaterialUIOn()
    {
        upgradeUI.gameObject.SetActive(false);
        materialUI.gameObject.SetActive(true);
        craftUI.gameObject.SetActive(false);
    }

    public void CraftUIOn()
    {
        upgradeUI.gameObject.SetActive(false);
        materialUI.gameObject.SetActive(false);
        craftUI.gameObject.SetActive(true);
    }

    public void CloseAll()
    {
        MainboardReset();

        this.gameObject.SetActive(false);
    }

}
