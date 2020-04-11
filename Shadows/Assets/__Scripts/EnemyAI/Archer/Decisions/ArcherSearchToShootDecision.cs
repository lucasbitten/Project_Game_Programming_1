using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Archer/Decisions/SearchToShoot")]

public class ArcherSearchToShootDecision : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        if (controller.detectedPlayer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
