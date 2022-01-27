using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public Status playerStatus;
    [SerializeField] HealthController healthBar;

    void Awake()
    {
        playerStatus = GetComponent<Status>();
    }

    public void OnTriggerStay(Collider coll)
    {
        EnemyCheck(coll);
    }

    void EnemyCheck(Collider coll)
    {
        if (coll.tag == "Enemy")
        {
            playerStatus.TakeDamage(coll);
            healthBar.UpdateHealth();
        }
    }
}
