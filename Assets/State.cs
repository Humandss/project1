using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Enemy       enemy;
    protected Transform   player;

    public State(Enemy enemy, Transform player) 
    { 
        this.enemy = enemy; 
        this.player = player; 
    }
    //
    protected State DequeueAndActivateState()
    {
        while(enemy.HasEvents)
        {
            //Å¥°¡ ºô´ë±îÁö ÇÏ³ª¾¿ Å¥¿¡¼­ ²¨³»¿È
            EnemyEventType type = enemy.DequeueEvent();
            State next = OnEvent(type);
            if (next != null) {return next;}

        }

        return null;
    }

    protected Vector3 getRandomLocation(float radius)
    {
        return enemy.transform.position
            + new Vector3(Random.Range(radius * -1f, radius * 1f), 0f, Random.Range(radius * -1f, radius * 1f));
    }    

    public abstract State HandleInput();

    public abstract State OnEvent(EnemyEventType type);

    public abstract void DoAction();

    public virtual void Enter() {}

    public virtual void Exit() {}
}

