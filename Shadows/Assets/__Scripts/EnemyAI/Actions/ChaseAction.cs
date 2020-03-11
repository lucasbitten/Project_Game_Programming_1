using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Chase")]
public class ChaseAction : Action
{

    public override void Init(EnemyStateController controller)
    {

    }

    public override void Act(EnemyStateController controller)
    {
        // Chase Player
        Vector2 chaseVector = Vector2.MoveTowards(
            controller.transform.position,
            controller.enemyMovementController.chaseTarget.position,
            0.01f);

        controller.transform.position = new Vector3(
            chaseVector.x, 
            controller.transform.position.y, 
            controller.transform.position.z);

    }

}
