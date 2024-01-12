using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    public override void Move()
    {
        base.Move();
        animator.SetInteger("moveType", 1);
    }

    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
    }
}
