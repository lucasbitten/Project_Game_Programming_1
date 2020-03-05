using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]

public class States : ScriptableObject
{
    // A set of Actions
    public Action[] actions;
    // A set of Transitions
    public Transition[] transitions;

    // Initialization function for a state
    public void InitState(EnemyStateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Init(controller);
        }
    }

    // Update the different aspect of this state
    public void UpdateState(EnemyStateController controller)
    {
        // Evaluate each action and decision (transition)
        DoActions(controller);
        CheckTransition(controller);
    }

    private void CheckTransition(EnemyStateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);
            controller.TransitionToState(decisionSucceeded ? transitions[i].trueStates : transitions[i].falseStates);
        }
    }

    private void DoActions(EnemyStateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }
}
