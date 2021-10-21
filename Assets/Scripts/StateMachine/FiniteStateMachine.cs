using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public IState currentState { get; private set; }

    public void Initialize(IState initialState)
    {
        currentState = initialState;
        currentState?.EnterState();
    }

    public void ChangeState(IState newState)
    {        
        currentState.ExitState();
        newState.EnterState();
        currentState = newState;        
    }

    public IState GetCurrentState()
    {
        return currentState;
    }
}
