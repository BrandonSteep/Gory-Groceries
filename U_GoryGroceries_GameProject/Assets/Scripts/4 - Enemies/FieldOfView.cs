using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject player;

    public State idleState;
    public State patrolState;
    public State chaseState;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool playerSeen;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    playerSeen = true;
                    idleState.playerSeen = true;
                    patrolState.playerSeen = true;
                }
                else
                {
                    playerSeen = false;
                    chaseState.playerSeen = false;
                }
            }
            else
            {
                playerSeen = false;
                chaseState.playerSeen = false;
            }
        }
        else if (playerSeen)
        {
            playerSeen = false;
            chaseState.playerSeen = false;
        }
    }

}
