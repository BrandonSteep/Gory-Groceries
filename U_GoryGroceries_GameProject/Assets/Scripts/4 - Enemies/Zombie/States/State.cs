using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    public bool playerSeen;

    public GameObject player;
    public NavMeshAgent nav;

    public abstract State RunCurrentState();

    void Start()
    {
        nav = GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }
}
