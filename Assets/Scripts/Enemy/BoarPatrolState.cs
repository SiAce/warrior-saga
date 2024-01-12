using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void LogicUpdate()
    {
        if (!currentEnemy.physicsCheck.isGround || currentEnemy.physicsCheck.touchLeftWall || currentEnemy.physicsCheck.touchRightWall)
        {
            currentEnemy.StartCoroutine(currentEnemy.TurnAndWait());
        }
    }

    public override void PhysicsUpdate()
    {
    }
}

