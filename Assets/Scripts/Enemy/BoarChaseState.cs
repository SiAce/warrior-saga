

using UnityEngine;

public class BoarChaseState : BaseState
{
    private float inSightTimer;

    public override void OnEnter(Enemy enemy)
    {
        base.enemy = enemy;
        enemy.animator.SetInteger("moveType", 2);
        enemy.currentSpeed = enemy.chaseSpeed;
        inSightTimer = 3; 
    }

    public override void OnExit()
    {
    }

    public override void LogicUpdate()
    {
        inSightTimer -= Time.deltaTime;

        if (enemy.sightDetection.InSight(enemy.FaceDir))
        {
            inSightTimer = 3;
        };

        if (inSightTimer < 0)
        {
            enemy.ChangeState(enemy.patrolState);
        }

        if (!enemy.physicsCheck.isGround || enemy.physicsCheck.touchLeftWall || enemy.physicsCheck.touchRightWall)
        {
            enemy.StartCoroutine(enemy.TurnAndWait(enemy.chaseWaitTime, 2));
        }

        
    }

    public override void PhysicsUpdate()
    {
    }
}