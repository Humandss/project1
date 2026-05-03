using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeState : State
{
    Vector3 targetLocation;
    float step = 1.0f;

    public GoHomeState(Enemy enemy, Transform player) : base(enemy, player) { }

    // implement
    public override State HandleInput()
    {
        if (enemy.IsInSpawnLocation())
            return new IdleState(enemy, player);
        if (enemy.IsPlayerInApproachRange())
            return new ApproachState(enemy, player);
        if (enemy.Hp <= 10f)
            return new RunAwayState(enemy, player);
        return null;
    }
    
    // implement
    public override void DoAction()
    {
        enemy.MoveTowards(targetLocation, step);
        Debug.Log("GoHomeState: DoAction");
    }

    public override void Enter()
    {
        targetLocation = enemy.GetSpawnLocation();
        enemy.ChangeColor(Color.green);
    }

    public override void Exit()
    { 
    }

}
