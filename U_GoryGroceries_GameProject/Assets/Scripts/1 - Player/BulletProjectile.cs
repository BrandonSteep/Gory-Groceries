using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] int projDamage = 1;

    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("EnemyShot");
            other.GetComponentInParent<Enemy>().currentHealth -= projDamage;
        }
        else if(other.tag == "Head")
        {
            Debug.Log("Headshot!");
            other.GetComponentInParent<Enemy>().currentHealth -= projDamage * 3;
        }
    }
}
