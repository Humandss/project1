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

    protected Vector3 getRandomLocation(float radius)
    {
        return enemy.transform.position
            + new Vector3(Random.Range(radius * -1f, radius * 1f), 0f, Random.Range(radius * -1f, radius * 1f));
    }    

    public abstract State HandleInput();

    public abstract void DoAction();

    public virtual void Enter() {}

    public virtual void Exit() {}
}

