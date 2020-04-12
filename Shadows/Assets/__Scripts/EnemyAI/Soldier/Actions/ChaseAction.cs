using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Soldier/Actions/Chase")]
public class ChaseAction : Action
{

    public override void Init(EnemyStateController controller)
    {
        controller.enemyMovementController.sign.GetComponent<SpriteRenderer>().sprite = controller.enemyMovementController.exclamation;
        controller.enemyMovementController.sign.SetActive(true);
        controller.enemyMovementController.searching = false;

    }

    public override void Act(EnemyStateController controller)
    {
        // Chase Player

        if(HealthManager.Instance.currentHealth > 0)
        {
            if (Mathf.Abs(controller.transform.position.x - controller.enemyMovementController.chaseTarget.position.x) > 0.9f)
            {
                if (controller.transform.position.x < controller.enemyMovementController.chaseTarget.position.x)
                {
                    controller.enemyMovementController.Move(controller.enemyMovementController.chaseTarget.position - Vector3.right * 0.9f, controller.stats.chaseSpeed);
                }
                else
                {
                    controller.enemyMovementController.Move(controller.enemyMovementController.chaseTarget.position + Vector3.right * 0.9f, controller.stats.chaseSpeed);

                }
            }
        }
    }

}
