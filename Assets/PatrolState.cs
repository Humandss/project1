using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PatrolState : State
{
    float step = 1.0f;


    Vector3[] wayPoints = new Vector3[4] {
        new Vector3(3, 0, 3),
        new Vector3(3, 0, -3),
        new Vector3(-3, 0, -3),
        new Vector3(-3, 0, 3)
    };

    int currentWayPointIndex = 0;
    int n_movements = 0;


    int getClosestWaypointIndex(Vector3 position)
    {
        int closestIndex = 0;
        float closestDistance = Vector3.Distance(position, wayPoints[0]);

        for (int i = 1; i < wayPoints.Length; i++)
        {
            float distance = Vector3.Distance(position, wayPoints[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }


    public PatrolState(Enemy enemy, Transform player) : base(enemy, player)
    {
    }

    

    // implement
    public override State HandleInput()
    {
        if (enemy.IsPlayerInApproachRange())
        {
            return new ApproachState(enemy, player);
        }
        if (n_movements == wayPoints.Length)
        {
            n_movements = 0;
            return new IdleState(enemy, player);
        }
        return null;
    }
    
    // implement
    public override void DoAction()
    {
        Debug.Log("PatrolState: DoAction");
        // if destination is reached, set next destination
        if (enemy.IsInTargetLocation(wayPoints[currentWayPointIndex]) )
        {
            n_movements++;
            currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
        }
        enemy.MoveTowards(wayPoints[currentWayPointIndex], step);
    }

    public override void Enter()
    {
        currentWayPointIndex = getClosestWaypointIndex(enemy.transform.position);
        n_movements = 0;
        enemy.ChangeColor(Color.yellow);
    }

    public override void Exit()
    {
    }

}
