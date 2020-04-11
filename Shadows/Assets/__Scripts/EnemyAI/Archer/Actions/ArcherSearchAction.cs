using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Archer/Actions/Search")]

public class ArcherSearchAction : Action
{
    public override void Init(EnemyStateController controller)
    {
        controller.enemyMovementController.animator.SetBool("Looking", true);
    }

    public override void Act(EnemyStateController controller)
    {
        return;
    }
}
