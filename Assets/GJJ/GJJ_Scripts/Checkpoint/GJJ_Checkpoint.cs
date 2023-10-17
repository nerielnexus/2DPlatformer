using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CHECKPOINTTYPE
{
    NONE = 0,
    START,
    SAVE,
    GOAL
};

public class GJJ_Checkpoint : MonoBehaviour
{
    // public
    public CHECKPOINTTYPE cpType;
    public Transform cpRespawnPoint;

    // private
    [SerializeField] private GJJ_CheckpointMaster cpMaster;
    [SerializeField] private GJJ_CheckpointEventModule cpEventHandler;
    [SerializeField] private bool _isCheckpointSaved;
    [SerializeField] GameObject cpVisualFlagForActivation;

    public GameObject endUI;
    public GameObject allStageClear;

    // method
    void Checkpoint_SubsrcibeEvent(bool _sub)
    {
        if (_sub) cpEventHandler.playerDeath += CheckpointEvent_OnPlayerDeath;
        else cpEventHandler.playerDeath -= CheckpointEvent_OnPlayerDeath;
    }

    void Checkpoint_EnableSave(bool _enable)
    {
        _isCheckpointSaved = _enable;
    }

    public void Checkpoint_InitializeEntity(bool _enable)
    {
        Checkpoint_EnableSave(_enable);
        Checkpoint_SubsrcibeEvent(_enable);
    }

    void CheckpointEvent_OnPlayerDeath()
    {
        cpMaster.cpPlayer.transform.position = cpRespawnPoint.position;
        // 플레이어의 체력을 다시 리필하는 곳인데......
    }

    void Checkpoint_SetActiveVisual()
    {
        if(cpType != CHECKPOINTTYPE.GOAL)
            cpVisualFlagForActivation.SetActive(_isCheckpointSaved);
        else
            cpVisualFlagForActivation.SetActive(true);
    }

    // unity
    private void Awake()
    {
        cpMaster = GetComponentInParent<GJJ_CheckpointMaster>();
        cpEventHandler = cpMaster.cpPlayer.GetComponent<GJJ_CheckpointEventModule>();
        _isCheckpointSaved = false;
        cpType = CHECKPOINTTYPE.NONE;

        endUI = GameObject.Find("GameCanvas").transform.Find("EndUI").gameObject;
        allStageClear = GameObject.Find("GameCanvas").transform.Find("EndUI2").gameObject;
    }

    private void Update()
    {
        Checkpoint_SetActiveVisual();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == cpMaster.cpPlayer)
        {
            if (cpType == CHECKPOINTTYPE.SAVE)
            {
                _isCheckpointSaved = true;
                cpMaster.Checkpoint_ActivateEntity(gameObject);
            }
            else if (cpType == CHECKPOINTTYPE.GOAL)
            {
                // StageManager 성공표시
                StageManager.instance.stage[SceneManager.GetActiveScene().buildIndex-2] = true;
                if(SceneManager.GetActiveScene().buildIndex != 6)
                {
                    endUI.SetActive(true);
                }
                else
                {
                    allStageClear.SetActive(true);
                }
                Time.timeScale = 0;
            }
        }
    }
}
