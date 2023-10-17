using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GJJ_CheckpointMaster : MonoBehaviour
{
    // public
    [Header("Hierarchy 에서 맞는 객체를 찾아 등록해주세요")]
    public GameObject cpPlayer;

    // private
    [SerializeField] GameObject cpStartPoint;
    [SerializeField] GameObject cpGoalPoint;
    [Header("Checkpoint 목록 확인용 (ReadOnly)")]
    [SerializeField] private List<GameObject> cpList;
    [SerializeField] private List<bool> cpSaveList = new List<bool>();

    // method
    void Checkpoint_InitializeEntity()
    {
        for (int i = 0; i < transform.childCount; i++)
            cpList.Add(transform.GetChild(i).gameObject);

        foreach (GameObject _obj in cpList)
        {
            GJJ_Checkpoint _tmpCP = _obj.GetComponent<GJJ_Checkpoint>();

            if (_obj == cpStartPoint)
            {
                _tmpCP.cpType = CHECKPOINTTYPE.START;
                _tmpCP.Checkpoint_InitializeEntity(true);
            }
            else if (_obj == cpGoalPoint)
            {
                _tmpCP.cpType = CHECKPOINTTYPE.GOAL;
                _tmpCP.Checkpoint_InitializeEntity(false);
            }
            else
            {
                _tmpCP.cpType = CHECKPOINTTYPE.SAVE;
                _tmpCP.Checkpoint_InitializeEntity(false);
            }
        }
    }

    public void Checkpoint_ActivateEntity(GameObject _obj)
    {
        foreach (GameObject _iter in cpList)
        {
            GJJ_Checkpoint _tmpCP = _iter.GetComponent<GJJ_Checkpoint>();

            if (_iter == _obj)
            {
                _tmpCP.Checkpoint_InitializeEntity(true);
            }
            else
            {
                _tmpCP.Checkpoint_InitializeEntity(false);
            }
        }
    }

    // unity
    private void Awake()
    {
        cpList = new List<GameObject>();
        cpPlayer.transform.position = cpStartPoint.GetComponent<GJJ_Checkpoint>().cpRespawnPoint.transform.position;
    }

    private void Start()
    {
        Checkpoint_InitializeEntity();
    }
}
