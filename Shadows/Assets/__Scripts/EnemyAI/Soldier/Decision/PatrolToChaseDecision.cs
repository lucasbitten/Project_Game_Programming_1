﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Soldier/Decisions/PatrolToChase")]
public class PatrolToChaseDecision : Decision
{
    public override bool Decide(EnemyStateController controller)
    {

        if (!controller.player.visible)
            return false;

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
            AudioManager.instance.Play("detected");

            return true;
        } 
        else
        {
            return false;
        }

    }

}
