using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    Animator anim;

    public AudioSource footstepSource;
    public AudioClip[] footstepSounds;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip landingSound;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        footstepSource = GetComponent<AudioSource>();
    }

    public void Bob(bool isGrounded/*, float bobSpeed*/)
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 && isGrounded)
        {
            anim.SetInteger("Walk", 1);
        }
        else
        {
            anim.SetInteger("Walk", 0);
        }
    }

    public void PlayFootstep()
    {
        footstepSource.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)], Random.Range(0.15f, 0.5f));
    }

    public void PlayJumpSound()
    {
        footstepSource.PlayOneShot(jumpSound, 1f);
    }

    public void PlayLandingSound()
    {
        footstepSource.PlayOneShot(landingSound, 0.5f);
    }
}
