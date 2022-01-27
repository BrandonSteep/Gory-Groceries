using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;

    public override State RunCurrentState()
    {
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
}
