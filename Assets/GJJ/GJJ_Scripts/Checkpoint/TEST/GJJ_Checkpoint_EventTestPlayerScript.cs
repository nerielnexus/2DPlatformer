using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_Checkpoint_EventTestPlayerScript : MonoBehaviour
{
    // public
    /// <summary>
    /// 플레이어의 체력 변수입니다.
    /// </summary>
    public int gjjHealth;

    // method
    /// <summary>
    /// 플레이어가 대미지를 받았을 때, '대미지를 받음' 혹은 '죽음' 애니메이션을 재생하는 메서드입니다.
    /// </summary>
    public void GJJ_PlayHitAnim()
    {
        if (gjjHealth > 0)
            Debug.Log("플레이어가 \'대미지를 받음\' 애니메이션을 재생함");
        else
            Debug.Log("플레이어가 \'죽음\' 애니메이션을 재생함");
    }

    // unity
    private void Awake()
    {
        gjjHealth = 10;
    }
}
