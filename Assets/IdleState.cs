using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
   
    float resetTimer;

    float currentTimer;

    public IdleState(Enemy enemy, Transform player) : base(enemy, player)
    {
        resetTimer = 3f;
        currentTimer = 0f;
    }
   

    // implement
    public override State HandleInput()
    {
        //기존 코드
        //if (enemy.IsPlayerInApproachRange())
        // {
        // return new ApproachState(enemy, player);
        // }

        State state = DequeueAndActivateState();
        if (state != null) return state;
 
        if (currentTimer >= resetTimer)
        {
            currentTimer = 0f;
            if (Random.Range(0, 1) == 0)
            {
                return new PatrolState(enemy, player);
            }
            // if 0, stay idle
        }        
        return null;
    }

    public override State OnEvent(EnemyEventType type) => type switch
    {
        EnemyEventType.EnterApproachRange => new ApproachState(enemy, player),
        _ => null
    };
    // implement
    public override void DoAction()
    {
        Debug.Log("IdleState: DoAction");
        currentTimer += Time.deltaTime;
    }

    public override void Enter()
    {
        currentTimer = 0f;
        enemy.ChangeColor(Color.white);
    }

    public override void Exit()
    {
        
    }

}
