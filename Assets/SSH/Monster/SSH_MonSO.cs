using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Monster", menuName = "Enemy/Monster(고블린)")]
public class SSH_MonSO : ScriptableObject
{
    [System.Serializable]
    public class MonsterDataEx
    {
        public int address;
        public string name;
        public Sprite monSprite;
        public SSH_MonMove monMove;
        public Animator monAni;
    }

    public MonsterDataEx[] monDataEx;
}
