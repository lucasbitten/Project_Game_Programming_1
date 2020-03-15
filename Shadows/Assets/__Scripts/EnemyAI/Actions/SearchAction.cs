using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Search")]
public class SearchAction : Action
{
    private Vector3 lastKnownPlayerPosition;
    public float TimeToKeepSearching = 5.0f;
    private EnemyStats stats;
    public override void Init(EnemyStateController controller)
    {
        // Get last position from player after the touches the ground
        lastKnownPlayerPosition = controller.enemyMovementController.chaseTarget.position;
        stats = controller.stats;
        stats.timeToSearch = TimeToKeepSearching;
        // Set time for keep searching before move to patrol
    }

    public override void Act(EnemyStateController controller)
    {
        // Go to last known location from player
        Vector2 searchVector = Vector2.MoveTowards(
            controller.transform.position,
            lastKnownPlayerPosition,
            0.1f);

        controller.transform.position = new Vector3(
            searchVector.x, 
            controller.transform.position.y,
            controller.transform.position.z);

        var reachedDestination = controller.enemyMovementController.transform.position == lastKnownPlayerPosition;
        
        stats.timeToSearch--;
        
        if (reachedDestination)
        {
            controller.transform.position = Vector3.left; 
            Debug.Log("Search Attacking Left");
            controller.enemyMovementController.animator.SetTrigger("isAttaking");
            //if (Time.time >= controller.nextAttack)
            //{
            //    controller.nextAttack = Time.time + 1f / stats.attackRate;
            //}
            controller.transform.position = Vector3.right;
            Debug.Log("Search Attacking Left");
            controller.enemyMovementController.animator.SetTrigger("isAttaking");
        }
        // TODO: attack each side to see if the player is there
        // TODO: Chase if player is in range or go back to patrol if player is long gone.

    }
}
