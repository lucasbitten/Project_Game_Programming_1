using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Soldier/Actions/Patrol")]
public class PatrolAction : Action
{

    public float patrolSpeed = 5;
    private Vector2 currentTarget;

    public override void Init(EnemyStateController controller)
    {
        controller.enemyMovementController.sign.SetActive(false);
        currentTarget = controller.enemyMovementController.waypoints[0].position;
        controller.enemyMovementController.searching = false;

    }
    public override void Act(EnemyStateController controller)
    {
        int nextWaypoint = controller.enemyMovementController.nextWaypoint;
        Transform waypoint = controller.enemyMovementController.waypoints[nextWaypoint];
        // Call the Move function and provide target
        controller.enemyMovementController.Move( waypoint.position, patrolSpeed);

        // Determine the distance to the target
        float distanceToTarget = Mathf.Abs(controller.transform.position.x -  waypoint.position.x);

        // Change target when the destination has been reached
        if (distanceToTarget < 0.2f)
        {
            controller.enemyMovementController.nextWaypoint = (controller.enemyMovementController.nextWaypoint + 1) % controller.enemyMovementController.waypoints.Count;
        }


    }


}
