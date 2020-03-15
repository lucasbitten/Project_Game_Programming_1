using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/ChaseToSearch")]
public class ChaseToSearchDecision : Decision
{
    public override bool Decide(EnemyStateController controller)
    {

        // Check if an object is in front of this object
        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(
            controller.enemyMovementController.eyes.position,
            controller.enemyMovementController.eyes.TransformDirection(Vector3.right),
            5.0f,
            controller.enemyMovementController.playerLayer
            );

        Debug.DrawRay(controller.enemyMovementController.eyes.position,
            controller.enemyMovementController.eyes.TransformDirection(Vector3.right) * 5, Color.red
        );

         // Check if enemy can see the player
        if (hit && hit.collider.CompareTag("Player"))
        {
            // Store the players position as a new chaseTarget
            controller.enemyMovementController.chaseTarget = hit.transform;
            return false;
        } 
        else
        {
            return true;
        }

    }

}
