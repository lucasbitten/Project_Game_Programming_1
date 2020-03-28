using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/SearchToPatrolDecision")]

public class SearchToPatrolDecision : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        RaycastHit2D hit = Physics2D.Raycast(
                   controller.enemyMovementController.eyes.position,
                   controller.enemyMovementController.eyes.TransformDirection(Vector3.right),
                   5.0f,
                   controller.enemyMovementController.playerLayer
               );

        Debug.DrawRay(controller.enemyMovementController.eyes.position,
            controller.enemyMovementController.eyes.TransformDirection(Vector3.right) * 5, Color.blue
        );

        // Check if enemy can see the player
        if (controller.stats.timeToSearch < 0 && !controller.attacking)
        {
            Debug.Log("Search to Patrol true");
            return true;
        }
        else
        {
            return false;
        }
    }


}
