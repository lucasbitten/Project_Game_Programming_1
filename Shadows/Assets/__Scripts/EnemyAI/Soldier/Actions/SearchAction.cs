using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Soldier/Actions/Search")]
public class SearchAction : Action
{
    private Vector3 lastKnownPlayerPosition;
    private EnemyStats stats;
    bool reachLastKnowPosition;
    bool stopAttack = false;

    public override void Init(EnemyStateController controller)
    {
        controller.attacking = false;
        controller.enemyMovementController.sign.GetComponent<SpriteRenderer>().sprite = controller.enemyMovementController.questionMark;
        controller.enemyMovementController.sign.SetActive(true);
        // Get last position from player after the touches the ground
        lastKnownPlayerPosition = controller.enemyMovementController.chaseTarget.position;
        stats = controller.stats;
        controller.timeLookingForPlayer = stats.timeToSearch;
        // Set time for keep searching before move to patrol
    }

    public override void Act(EnemyStateController controller)
    {
        
        //reachLastKnowPosition = Vector3.Distance(controller.enemyMovementController.transform.position, lastKnownPlayerPosition) < 0.5f;
        reachLastKnowPosition = Mathf.Abs(controller.enemyMovementController.transform.position.x - lastKnownPlayerPosition.x) < 1;
        controller.timeLookingForPlayer -= Time.deltaTime;

        if (!reachLastKnowPosition)
        {
            controller.enemyMovementController.Move(lastKnownPlayerPosition, controller.stats.searchSpeed);
            controller.enemyMovementController.searching = false;


        }
        else if (reachLastKnowPosition && !controller.attacking)
        {
            controller.enemyMovementController.rBody.velocity = Vector2.zero;
            controller.attacking = true;
            controller.StopAllCoroutines();
            controller.StartCoroutine(searchAttack(controller));

        }
    }

    IEnumerator searchAttack(EnemyStateController controller)
    {
        controller.enemyMovementController.searching = true;

        yield return new WaitForSeconds(0.5f);
        controller.enemyMovementController.animator.SetTrigger("isAttaking");
        yield return new WaitForSeconds(1f);
        if (stopAttack)
        {

            controller.attacking = false;
            stopAttack = false;
            controller.enemyMovementController.searching = false;
            controller.timeLookingForPlayer = 0;
        }
        else
        {

            stopAttack = true;
            controller.enemyMovementController.Flip();
            controller.StartCoroutine(searchAttack(controller));
            
        }
    }

}
