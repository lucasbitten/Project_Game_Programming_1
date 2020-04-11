using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Action : ScriptableObject
{
    public abstract void Init(EnemyStateController controller);

    public abstract void Act(EnemyStateController controller);
    
}
