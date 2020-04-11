using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Archer/Actions/Shoot")]

public class ArcherShootAction : Action
{

    private Transform attackPoint;
    private EnemyStats stats;
    private LayerMask playerLayer;
    private float attackDelay;

    public override void Init(EnemyStateController controller)
    {
        controller.enemyMovementController.animator.SetBool("Looking", false);
        attackPoint = controller.enemyMovementController.attackPoint;
        stats = controller.stats;
        playerLayer = controller.enemyMovementController.playerLayer;
        controller.attacking = true;
    }

    public override void Act(EnemyStateController controller)
    {


        if (controller.player.transform.position.x < controller.transform.position.x)
        {
            controller.archerController.archerSprites.gameObject.transform.localScale = new Vector3(-1, 1, 1);

        }
        else
        {
            controller.archerController.archerSprites.gameObject.transform.localScale = new Vector3(1, 1, 1);

        }
    


        if (controller.nextAttack <= 0)
        {
            controller.enemyMovementController.animator.SetBool("Waiting", false);

            controller.enemyMovementController.animator.SetTrigger("isAttaking");
            controller.nextAttack = controller.stats.attackRate;

        }
    }


}
