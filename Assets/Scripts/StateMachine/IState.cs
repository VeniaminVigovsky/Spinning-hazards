using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    Animator animator { get; set; }

    FiniteStateMachine stateMachine { get; set; }

    Rigidbody2D rb { get; set; }

    void EnterState();

    void ExitState();

    void PhysicsUpdate();

    void LogicUpdate();

}
