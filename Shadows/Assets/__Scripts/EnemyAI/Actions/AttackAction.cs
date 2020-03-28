using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Attack")]

public class AttackAction : Action
{
    private Transform attackPoint;
    private EnemyStats stats;
    private LayerMask playerLayer;

    public override void Init(EnemyStateController controller)
    {
        controller.enemyMovementController.sign.GetComponent<SpriteRenderer>().sprite = controller.enemyMovementController.exclamation;
        controller.enemyMovementController.sign.SetActive(true);
        attackPoint = controller.enemyMovementController.attackPoint;
        stats = controller.stats;
        playerLayer = controller.enemyMovementController.playerLayer;
    }

    public override void Act(EnemyStateController controller)
    {
        Debug.Log("Attacking");
        controller.enemyMovementController.animator.SetTrigger("isAttaking");

        if (Time.time >= controller.nextAttack)
        {
            controller.nextAttack = Time.time + 1f / stats.attackRate;
        }


    }


}


