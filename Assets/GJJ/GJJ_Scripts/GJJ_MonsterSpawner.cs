using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJJ_MonsterSpawner : MonoBehaviour
{
    // public

    // private
    [SerializeField] private GameObject monsterToSpawn;
    [SerializeField] private float monsterSpawnInterval = 3.0f;
    [SerializeField] private int monsterMaxSpawnCount = 10;
    
    [Header("단순 Hierarchy 정리용, null 이어도 무관")]
    [SerializeField] private Transform monsterAIOHierarchy = null;

    [Header("READ ONLY")]
    [SerializeField] private List<Transform> monsterSpawnPointList = new List<Transform>();
    [SerializeField] private List<GameObject> monsterSpawnedList = new List<GameObject>();

    private bool isOnLoop = false;
    private int spawnCounter = 0;

    // method
    void SpawnMonster()
    {
        int _spawnRNG = Random.Range(0, monsterSpawnPointList.Count);
        GameObject _spawnedMob = Instantiate(monsterToSpawn, monsterSpawnPointList[_spawnRNG].transform.position, Quaternion.identity);
        monsterSpawnedList.Add(_spawnedMob);

        if (monsterAIOHierarchy != null)
            _spawnedMob.transform.SetParent(monsterAIOHierarchy);
    }

    void DestroyRandomMonster()
    {
        System.Random _sysRNG = new System.Random();
        GameObject _toDelete = monsterSpawnedList[_sysRNG.Next(monsterSpawnedList.Count)];
        monsterSpawnedList.Remove(_toDelete);
        Destroy(_toDelete);

        if(spawnCounter > 0)
            spawnCounter--;
    }

    IEnumerator SpawnMonsterLoop()
    {
        if(!isOnLoop)
        {
            isOnLoop = true;

            if(spawnCounter < monsterMaxSpawnCount)
            {
                SpawnMonster();
                spawnCounter++;
                yield return new WaitForSeconds(monsterSpawnInterval);
            }

            isOnLoop = false;
        }
    }

    IEnumerator AutomaticSpawnMonsterLoop()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        while (spawnCounter < monsterMaxSpawnCount)
        {
            StartCoroutine(SpawnMonsterLoop());
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));
        }   

        yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        int deleteCount = Random.Range(0, monsterMaxSpawnCount);

        while (spawnCounter > deleteCount)
            DestroyRandomMonster();

        yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));
    }

    void PressKeyToSpawnMonster()
    {
        if (Input.GetKeyDown(KeyCode.M))
            StartCoroutine(SpawnMonsterLoop());

        if (Input.GetKeyDown(KeyCode.N))
            DestroyRandomMonster();
    }

    // unity
    private void Awake()
    {
        monsterSpawnPointList = new List<Transform>();
        monsterSpawnedList = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
            monsterSpawnPointList.Add(transform.GetChild(i).transform);
    }

    private void Update()
    {
        // PressKeyToSpawnMonster();

        StartCoroutine(AutomaticSpawnMonsterLoop());
    }
}
