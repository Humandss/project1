using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    float attackCooldownTimer = 2f;
    float currentCooldownTimer;

    public AttackState(Enemy enemy, Transform player) : base(enemy, player) { }


    // implement
    public override State HandleInput()
    {
        if (enemy.Hp <= 10f)
        {
            return new RunAwayState(enemy, player);
        }
        if (enemy.IsPlayerOutOfAttackRange())
        {
            return new ApproachState(enemy, player);
        }
        return null;
    }
    
    // implement
    public override void DoAction()
    {
        // do nothing
        if (currentCooldownTimer >= attackCooldownTimer)
        {
            currentCooldownTimer = 0f;
            enemy.AttackPlayer();
        }
        else
        {
            currentCooldownTimer += Time.deltaTime;
        }
        Debug.Log("AttackState: DoAction");
    }

    public override void Enter()
    {
        currentCooldownTimer = 0f;
        enemy.ChangeColor(Color.red);
    }

    public override void Exit()
    {
    }

}
