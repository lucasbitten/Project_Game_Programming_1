using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/SearchToChaseOrPatrolDecision")]
public class SearchToChaseOrPatrolDecision : Decision
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
        if (controller.stats.timeToSearch > 0 && hit && hit.collider.CompareTag("Player"))
        {
            // Store the players position as a new chaseTarget
            controller.enemyMovementController.chaseTarget = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}
