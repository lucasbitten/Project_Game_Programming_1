using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Chase")]
public class ChaseAction : Action
{

    public override void Init(EnemyStateController controller)
    {
        controller.enemyMovementController.sign.GetComponent<SpriteRenderer>().sprite = controller.enemyMovementController.exclamation;
        controller.enemyMovementController.sign.SetActive(true);
    }

    public override void Act(EnemyStateController controller)
    {
        // Chase Player

        controller.enemyMovementController.Move(controller.enemyMovementController.chaseTarget.position, controller.stats.chaseSpeed);


        //Vector2 chaseVector = Vector2.MoveTowards(
        //    controller.transform.position,
        //    controller.enemyMovementController.chaseTarget.position,
        //    0.06f);

        //controller.transform.position = new Vector3(
        //    chaseVector.x, 
        //    controller.transform.position.y, 
        //    controller.transform.position.z);

    }

}
