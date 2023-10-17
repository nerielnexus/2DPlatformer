using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSH_Player : MonoBehaviour
{
    public SpriteRenderer player;

    private void Start()
    {
        player = GetComponent<SpriteRenderer>();
    }


    public void Damaged()
    {
        StartCoroutine(OnDamage());
    }

    IEnumerator OnDamage()
    {

        player.color = new Color(1, 0, 1);

        yield return new WaitForSeconds(2.0f);

        player.color = new Color(1, 1, 1, 1);

    }
}
