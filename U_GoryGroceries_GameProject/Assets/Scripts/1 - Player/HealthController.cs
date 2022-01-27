using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private Status playerStatus;

    [SerializeField] private Image[] hearts;

    void Start()
    {
        playerStatus = GameObject.FindWithTag("Player").GetComponent<Status>();
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerStatus.currentHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
