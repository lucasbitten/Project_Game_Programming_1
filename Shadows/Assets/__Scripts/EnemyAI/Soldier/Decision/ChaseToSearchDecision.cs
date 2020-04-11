using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Soldier/Decisions/ChaseToSearch")]
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
        if (!hit && !controller.player.visible)
        {
            // Store the players position as a new target
            Debug.Log("Chase to Search true");
            controller.enemyMovementController.chaseTarget = controller.player.transform;
            return true;
        } 
        else
        {
            return false;
        }

    }

}
