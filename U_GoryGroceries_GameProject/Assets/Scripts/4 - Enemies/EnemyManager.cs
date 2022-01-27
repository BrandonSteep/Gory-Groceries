using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    
    public GameObject player;

    public int enemyCount;
    private int enemiesToSpawn;

    [Header("Spawn Stats")]
    [SerializeField]
    public int spawnLimit = 25;
    private int spawnTime = 5;
    [SerializeField]
    private bool continueCoroutine = true;

    void Start()
    {
        SpawnNewEnemy();
        enemyCount = transform.childCount;
    }

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(SpawnCheck());
    }

    //void OnEnable()
    //{
    //    Enemy.OnEnemyKilled += SpawnNewEnemy;
    //}

    void SpawnNewEnemy()
    {
        Debug.Log("Spawn Called");

        while (enemiesToSpawn > 0)
        {
            int randomNumber = Mathf.RoundToInt(Random.Range(0f, spawnPoints.Length - 1));
            float dist = Vector3.Distance(player.transform.position, spawnPoints[randomNumber].transform.position);

            if (dist > 35f)
            {
                Instantiate(enemyPrefab, spawnPoints[randomNumber].transform.position, Quaternion.identity);
                enemyCount++;
                enemiesToSpawn--;
                Debug.Log("Enemy Spawned");
            }
        }
    }

    public void ThreatLevelIncrease()
    {
        if (spawnLimit < 500)
        {
            spawnLimit += 50;
        }
    }

    private IEnumerator SpawnCheck()
    {
        while (continueCoroutine)
        {
            enemiesToSpawn = spawnLimit - enemyCount;
            if (enemiesToSpawn != 0)
            {
                SpawnNewEnemy();
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
