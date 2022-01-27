using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public State currentState;
    private NavMeshAgent nav;

    [SerializeField] private float speedMin = 7f;
    [SerializeField] private float speedMax = 2f;
    public float speed;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        speed = Random.Range(speedMin, speedMax);
        nav.speed = speed;
    }
    
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}
