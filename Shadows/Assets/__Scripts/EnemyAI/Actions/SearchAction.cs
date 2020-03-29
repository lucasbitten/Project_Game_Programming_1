using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Search")]
public class SearchAction : Action
{
    private Vector3 lastKnownPlayerPosition;
    public float TimeToKeepSearching = 5.0f;
    private EnemyStats stats;
    bool reachLastKnowPosition;

    public override void Init(EnemyStateController controller)
    {
        controller.attacking = false;
        controller.enemyMovementController.sign.GetComponent<SpriteRenderer>().sprite = controller.enemyMovementController.questionMark;
        controller.enemyMovementController.sign.SetActive(true);
        // Get last position from player after the touches the ground
        lastKnownPlayerPosition = controller.enemyMovementController.chaseTarget.position;
        stats = controller.stats;
        stats.timeToSearch = TimeToKeepSearching;
        // Set time for keep searching before move to patrol
    }

    public override void Act(EnemyStateController controller)
    {
        
        reachLastKnowPosition = Vector3.Distance(controller.enemyMovementController.transform.position, lastKnownPlayerPosition) < 0.5f;
        stats.timeToSearch -= Time.deltaTime;

        if (!reachLastKnowPosition)
        {
            controller.enemyMovementController.Move(lastKnownPlayerPosition, controller.stats.searchSpeed);


        } else if (reachLastKnowPosition && !controller.attacking)
        {
            controller.enemyMovementController.rBody.velocity = Vector2.zero;
            controller.attacking = true;
            controller.StartCoroutine(searchAttack(controller));

        }
    }

    IEnumerator searchAttack(EnemyStateController controller)
    {
        controller.enemyMovementController.animator.SetTrigger("isAttaking");
        yield return new WaitForSeconds(2f);
        controller.enemyMovementController.Flip();
        controller.StartCoroutine(searchAttack(controller));
    }

}
