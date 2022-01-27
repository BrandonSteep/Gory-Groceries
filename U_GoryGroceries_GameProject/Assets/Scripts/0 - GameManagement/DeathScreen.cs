using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private AudioClip deathSound;
    private AudioClip retrySound;
    private AudioClip leaveSound;

    private AudioSource aSource;

    void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    void PlayDeathSound()
    {
        aSource.clip = deathSound;
        aSource.PlayOneShot(deathSound, 1f);
    }

    public void Retry()
    {

        SceneManager.LoadScene(1);
    }

    public void Leave()
    {

        SceneManager.LoadScene(0);
    }

    void EnableButtons()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
