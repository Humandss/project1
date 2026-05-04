using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachState : State
{
    float step = 1.0f;

    public ApproachState(Enemy enemy, Transform player) : base(enemy, player) { }

    // implement
    public override State HandleInput()
    {
        /*
        if (enemy.IsPlayerInAttackRange())
        {
            return new AttackState(enemy, player);
        }
        if (enemy.IsPlayerOutOfApproachRange())
        {
            return new GoHomeState(enemy, player);
        }
        */
        State state = DequeueAndActivateState();
        if(state != null) return state;
        return null;
    }

    public override State OnEvent(EnemyEventType type) => type switch
    {
        EnemyEventType.EnterAttackRange => new AttackState(enemy, player),
        EnemyEventType.ExitApproachRange => new GoHomeState(enemy, player),
        _ => null
    };
    // implement
    public override void DoAction()
    {
        enemy.MoveTowardsPlayer(step);
        Debug.Log("ApproachState: DoAction");
    }

    public override void Enter()
    {
        enemy.ChangeColor(Color.orange);
    }

    public override void Exit()
    { 
    }

}
