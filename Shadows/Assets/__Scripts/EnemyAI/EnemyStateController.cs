using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public EnemyStats stats;

    [Header("State Information")]
    //Current State
    public State currentState;
    //Dummy state (to represent the same state)
    public State sameState;

    // [Header("Movement Information")]


    [HideInInspector] public EnemyMovementController enemyMovementController;


    void Start()
    {
        enemyMovementController = GetComponent<EnemyMovementController>();
        currentState.InitState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);   
    }

    //Transitions the state machine to a new state
    public void TransitionToState(State nextState)
    {
        if(nextState != sameState)
        {
            // Transition to a new state!
            currentState = nextState;

            // Initialize new state
            currentState.InitState(this);
        }
    }

}
