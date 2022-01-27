using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private AudioSource painSource;

    [SerializeField]
    private AudioClip painSound;
    [SerializeField]
    private AudioClip deathSound;

    void Awake()
    {
        painSource = GetComponent<AudioSource>();
    }


    public void DeathScreen()
    {
        SceneManager.LoadScene(2);
    }

    public void HurtSound()
    {
        painSource.PlayOneShot(painSound, 1f);
    }

    public void DeathSound()
    {
        painSource.PlayOneShot(deathSound, 1f);
    }
}
