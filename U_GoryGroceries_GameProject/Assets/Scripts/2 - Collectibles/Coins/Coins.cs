using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectible
{
    [SerializeField] private int coinValue = 1;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "PlayerCollider")
        {
            //Pick Up
            PickUp();
            Status playerStatus = coll.gameObject.GetComponentInParent<Status>();
            playerStatus.coinCount += coinValue;
            playerStatus.UpdateCoinCounter();
        }
    }

    
}
