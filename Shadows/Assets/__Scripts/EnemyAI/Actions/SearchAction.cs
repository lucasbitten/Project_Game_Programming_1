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
        
        reachLastKnowPosition = Vector3.Distance(controller.enemyMovementController.transform.position, lastKnownPlayerPosition) < 1f;
        Debug.Log(reachLastKnowPosition);
        stats.timeToSearch -= Time.deltaTime;

        if (!reachLastKnowPosition)
        {
            controller.enemyMovementController.Move(lastKnownPlayerPosition, controller.stats.searchSpeed);

            //// Go to last known location from player
            //Vector2 searchVector = Vector2.MoveTowards(
            //    controller.transform.position,
            //    lastKnownPlayerPosition,
            //    0.06f);

            //controller.transform.position = new Vector3(
            //    searchVector.x,
            //    controller.transform.position.y,
            //    controller.transform.position.z);

        } else if (reachLastKnowPosition && !controller.attacking)
        {

            controller.attacking = true;
            controller.StartCoroutine(searchAttack(controller));

        }
    }

    IEnumerator searchAttack(EnemyStateController controller)
    {
        yield return new WaitForSeconds(0.8f);
        Debug.Log("Search Attacking one side");
        controller.enemyMovementController.animator.SetTrigger("isAttaking");
        yield return new WaitForSeconds(0.8f);
        controller.enemyMovementController.Flip();
        Debug.Log("Search Attacking other side");
        controller.enemyMovementController.animator.SetTrigger("isAttaking");
        controller.attacking = false;
    }

}
