using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Collectible
{
    public GameObject weaponName;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "PlayerCollider")
        {
            //Pick Up
            PickUp();
            Status playerStatus = coll.gameObject.GetComponentInParent<Status>();
            playerStatus.SetActiveWeapon(weaponName);
        }
    }
}
