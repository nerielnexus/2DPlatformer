using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_TrapSlowdownBushes : MonoBehaviour
{
    private class PlayerData
    {
        public float playerSpeedWalk;
        public float playerSpeedSprint;
        public float playerSpeedLimit;
        public float playerJumpPower;
        public int playerJumpCount;
        public int playerJumpMaxCount;

        public PlayerData(Player _him)
        {
            playerSpeedWalk = _him.normalSpeed;
            playerSpeedSprint = _him.runSpeed;
            playerSpeedLimit = _him.maxSpeed;

            playerJumpPower = _him.jumpPower;
            playerJumpCount = _him.jumpCnt;
            playerJumpMaxCount = _him.jumpMaxCnt;
        }

        public PlayerData(float playerSpeedWalk, float playerSpeedSprint, float playerSpeedLimit,
            float playerJumpPower, int playerJumpCount, int playerJumpMaxCount)
        {
            this.playerSpeedWalk = playerSpeedWalk;
            this.playerSpeedSprint = playerSpeedSprint;
            this.playerSpeedLimit = playerSpeedLimit;
            this.playerJumpPower = playerJumpPower;
            this.playerJumpCount = playerJumpCount;
            this.playerJumpMaxCount = playerJumpMaxCount;
        }
    }

    // public

    // private
    [SerializeField] private PlayerData originalPlayerData;
    [SerializeField] private PlayerData debuffedPlayerData;
    [SerializeField] private Player player;

    [Header("Bush Debuff")]
    [SerializeField] private float SpeedWalk = 1.5f;
    [SerializeField] private float SpeedSprint = 1.5f;
    [SerializeField] private float SpeedLimit = 1.5f;
    [SerializeField] private float JumpPower = 1.5f;
    [SerializeField] private int JumpCount = 1;
    [SerializeField] private int JumpMaxCount = 1;

    // method
    void GJJ_ReplacePlayerData(Player _him, PlayerData _data)
    {
        _him.normalSpeed = _data.playerSpeedWalk;
        _him.runSpeed = _data.playerSpeedSprint;
        _him.maxSpeed = _data.playerSpeedLimit;
        _him.jumpPower = _data.playerJumpPower;
        _him.jumpCnt = _data.playerJumpCount;
        _him.jumpMaxCnt = _data.playerJumpMaxCount;
    }

    bool CheckPlayer(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return false;

        if (col.gameObject.layer != LayerMask.NameToLayer("Player"))
            return false;

        return true;
    }

    // unity
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        debuffedPlayerData = new PlayerData(SpeedWalk, SpeedSprint, SpeedLimit, JumpPower, JumpCount, JumpMaxCount);
        originalPlayerData = new PlayerData(player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CheckPlayer(collision))
            return;

        GJJ_ReplacePlayerData(player, debuffedPlayerData);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!CheckPlayer(collision))
            return;

        GJJ_ReplacePlayerData(player, originalPlayerData);
    }
}
