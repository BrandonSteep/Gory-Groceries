using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    [SerializeField] private int maxHealth = 3;
    public int currentHealth;

    public int damage = 1;

    //references//
    private Status playerStatus;
    private EnemyManager enemyManager;

    void Awake()
    {
        currentHealth = maxHealth;
        playerStatus = GameObject.FindWithTag("Player").GetComponent<Status>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
        playerStatus.IncreaseKillcount();
        enemyManager.enemyCount--;
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
    }
}
