using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayState : State
{
    Vector3 targetLocation;
    float step = 1.0f;

    public RunAwayState(Enemy enemy, Transform player) : base(enemy, player) { }

    // implement
    public override State HandleInput()
    {
        if (enemy.Hp >= 60f)
            return new ApproachState(enemy, player);
        return null;
    }
    
    // implement
    public override void DoAction()
    {
        Debug.Log("RunAwayState: DoAction, hp = " + enemy.Hp);
        enemy.Hp += Time.deltaTime * 5f;

        if (enemy.IsInTargetLocation(targetLocation)) 
            targetLocation = enemy.GetOppositeDirectionFromPlayer();
        enemy.MoveTowards(targetLocation, step);
    }

    public override void Enter()
    {
        targetLocation = enemy.GetOppositeDirectionFromPlayer();
        enemy.ChangeColor(Color.blue);
    }

    public override void Exit()
    { 
    }

}
