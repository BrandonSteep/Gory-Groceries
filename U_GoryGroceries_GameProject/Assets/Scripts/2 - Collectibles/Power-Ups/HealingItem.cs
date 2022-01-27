using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Collectible
{
    [SerializeField] private int healAmount = 5;
    private Status playerStatus;

    void OnTriggerEnter(Collider coll)
    {
        if (!playerStatus)
        {
        playerStatus = coll.gameObject.GetComponentInParent<Status>();
        }

        if (coll.gameObject.tag == "PlayerCollider" && playerStatus.currentHealth < playerStatus.maxHealth)
        {
            //Pick Up
            PickUp();
            Status playerStatus = coll.gameObject.GetComponentInParent<Status>();
            playerStatus.currentHealth += healAmount;

            while (playerStatus.currentHealth > playerStatus.maxHealth)
            {
                playerStatus.currentHealth--;
            }

            playerStatus.healthBar.UpdateHealth();
            playerStatus.UpdateCoinCounter();
        }
    }
}
