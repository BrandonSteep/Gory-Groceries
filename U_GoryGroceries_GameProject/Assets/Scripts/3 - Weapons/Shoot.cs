using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject player;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] private Animator anim;

    [SerializeField] private ParticleSystem shotParticles;
    [SerializeField] private ParticleSystem muzzleParticles;

    [SerializeField] private ParticleSystem shellParticle;

    public AudioClip fireSound;
    public AudioClip rackSound;

    AudioSource aSource;
    GameObject obj;

    [SerializeField] private int[] animations;

    [Header("FirearmVariables")]
    public float force;
    public bool automatic;

    private bool canShoot = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();

        anim = GetComponent<Animator>();

        //Find Audio Manager
        obj = GameObject.Find("AudioManager");
        if (obj != null)
            aSource = obj.GetComponent<AudioSource>();
    }

    void Awake()
    {
        ResetShot();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            Fire();
        }
    }

    void Fire()
    {
        if (!automatic)
        {
            anim.SetTrigger("Shoot");
        }
        else
        {
            int randomAnim = Random.Range(0, 3);
            anim.SetTrigger("Shoot" + randomAnim);
        }
        canShoot = false;
    }

    void FireProjectiles()
    {
        playerMovement.Knockback(force);
        muzzleParticles.Play();
        shotParticles.Play();
        if (fireSound != null)
        {
            //Play Sound
            aSource.clip = fireSound;
            aSource.PlayOneShot(fireSound, 1);
        }
    }

    void SpentCasing()
    {
        //Play Particle
        shellParticle.Play();
    }

    void ResetShot()
    {
        canShoot = true;
        if (automatic)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Fire();
            }
        }
    }

    void RackSound()
    {
        aSource.clip = rackSound;
        aSource.PlayOneShot(rackSound, 0.75f);
    }
}
