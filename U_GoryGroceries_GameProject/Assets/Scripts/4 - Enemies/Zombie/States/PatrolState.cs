using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public ChaseState chaseState;

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private float timer;

    void OnEnable()
    {
        timer = wanderTimer;
    }

    public override State RunCurrentState()
    {
        RandomMovement();
        if (playerSeen)
        {
            playerSeen = false;
            return chaseState;
        }
        else
        {
            return this;
        }
    }

    private void RandomMovement()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            nav.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}