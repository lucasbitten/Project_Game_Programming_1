using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Attack")]

public class AttackAction : Action
{

    public override void Init(EnemyStateController controller)
    {

    }

    public override void Act(EnemyStateController controller)
    {
        Debug.Log("Attacking");
    }


}


