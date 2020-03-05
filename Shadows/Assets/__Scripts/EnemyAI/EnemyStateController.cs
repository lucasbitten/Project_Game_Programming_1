using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public EnemyStats stats;

    [Header("State Information")]
    // Current State
    public States currentStates;
    // Dummy State
    public States sameStates;

    //[Header("Move Information")]



    [HideInInspector] public EnemyMovementController enemyMovementController;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovementController = GetComponent<EnemyMovementController>();
        currentStates.InitState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentStates.UpdateState(this);
    }

    // Transition the state machine to a new state
    public void TransitionToState(States nextState)
    {
        if (nextState != sameStates)
        {
            // Transition to a new state
            currentStates = nextState;

            // Initialize new state
            currentStates.InitState(this);
        }
    }
}
