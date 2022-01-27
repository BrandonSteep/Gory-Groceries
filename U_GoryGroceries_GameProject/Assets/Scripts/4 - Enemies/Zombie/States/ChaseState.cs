using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseState : State
{
    public AttackState attackState;
    public bool inAttackRange;

    public IdleState idleState;
    public PatrolState patrolState;

    public override State RunCurrentState()
    {
        
        SetDestination();

        if (!playerSeen)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist < 50f)
            {
                return this;
            }
            else
            {
                playerSeen = false;
                int patrolChance = Random.Range(0, 9);

                if (patrolChance >= 3)
                {
                    return idleState;
                }
                else
                {
                    return patrolState;
                }
            }
        }
        else
        {
            return this;
        }

        void SetDestination()
        {
            nav.SetDestination(player.transform.position);
            if (nav.isPathStale || (!nav.hasPath && !nav.pathPending) || nav.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathComplete)
            {
                UnityEngine.AI.NavMeshHit hit;
                UnityEngine.AI.NavMesh.SamplePosition(new Vector3(player.transform.position.x, 0, player.transform.position.z), out hit, 1f, UnityEngine.AI.NavMesh.AllAreas);
                nav.SetDestination(hit.position);
            }
        }

        //if (inAttackRange)
        //{
        //    inAttackRange = false;
        //    return attackState;
        //}
        //else
        //{
        //    return this;
        //}
    }
}
