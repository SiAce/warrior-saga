using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        base.enemy = enemy;
        enemy.animator.SetInteger("moveType", 1);
        enemy.currentSpeed = enemy.patrolSpeed;
    }

    public override void OnExit()
    {
        enemy.StopAllCoroutines();
        enemy.isHurt = false;
        enemy.isWaiting = false;
    }

    public override void LogicUpdate()
    {
        if (enemy.sightDetection.InSight(enemy.FaceDir))
        {
            enemy.ChangeState(enemy.chaseState);
        };

        if (!enemy.physicsCheck.isGround || enemy.physicsCheck.touchLeftWall || enemy.physicsCheck.touchRightWall)
        {
            enemy.StartCoroutine(enemy.TurnAndWait(enemy.patrolWaitTime));
        }
    }

    public override void PhysicsUpdate()
    {
    }
}

