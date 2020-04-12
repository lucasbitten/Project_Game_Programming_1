using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Soldier/Decisions/ChaseToAttack")]

public class ChaseToAttackDecision : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            controller.enemyMovementController.eyes.position,
            controller.enemyMovementController.eyes.TransformDirection(Vector3.right),
            1f,
            controller.enemyMovementController.playerLayer
            );

        Debug.DrawRay(controller.enemyMovementController.eyes.position,
            controller.enemyMovementController.eyes.TransformDirection(Vector3.right), Color.yellow
        );

         // Check if enemy can see the player
        if (hit && hit.collider.CompareTag("Player"))
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
