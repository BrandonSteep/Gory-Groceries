using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public State chaseState;
    public bool attackFinished;

    public override State RunCurrentState()
    {
        if (attackFinished)
        {
            attackFinished = false;
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
