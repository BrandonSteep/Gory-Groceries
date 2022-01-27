using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //Sound
    [SerializeField] protected AudioClip pickupSound;
    protected AudioSource aSource;
    protected GameObject obj;

    //Particle
    public GameObject pickupSplash;

    void Start()
    {
        //Find Audio Manager
        obj = GameObject.Find("AudioManager");
        if (obj != null)
            aSource = obj.GetComponent<AudioSource>();
    }

    public void PickUp()
    {
        //Play Sound
        aSource.clip = pickupSound;
        aSource.PlayOneShot(pickupSound, 1);

        if (pickupSound != null && gameObject.tag != ("HealingItem"))
        {
            //Spawn Particles//
            GameObject thisSplash = Instantiate(pickupSplash, this.gameObject.transform.position, Quaternion.identity) as GameObject;
            ParticleSystem parts = thisSplash.GetComponent<ParticleSystem>();
            Destroy(thisSplash, 2.5f);
        }
        else
        {
            pickupSplash.GetComponent<ParticleSystem>().Play();
        }

        //Destroy
        Destroy(this.gameObject);
    }
}
