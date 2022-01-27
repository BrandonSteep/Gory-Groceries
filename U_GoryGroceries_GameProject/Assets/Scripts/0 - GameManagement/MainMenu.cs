using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource menuAudio;
    [SerializeField] private AudioClip buttonSound;

    void Start()
    {
        menuAudio = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonSound()
    {
        menuAudio.PlayOneShot(buttonSound, 1f);
    }
}
