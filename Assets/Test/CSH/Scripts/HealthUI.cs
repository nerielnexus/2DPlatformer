using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Player player;

    int maxLife;
    Text lifeText;

    int maxHP;
    Image hpBar;

    int maxShield = 100;
    Image shieldBar;

    Text coins;

    private void Awake()
    {
        lifeText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        maxLife = player.playerLife;
        hpBar = transform.GetChild(1).GetChild(1).GetComponent<Image>();
        maxHP = player.playerHp;
        shieldBar = transform.GetChild(1).GetChild(2).GetComponent<Image>();
        coins = transform.GetChild(2).GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = maxLife.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        lifeText.text = player.playerLife.ToString();
        hpBar.fillAmount = (float)player.playerHp / (float)maxHP;
        shieldBar.fillAmount = (float)player.playerShield / (float)maxShield;
        coins.text = InvenUI.invenUI.golds.ToString();
    }
}
