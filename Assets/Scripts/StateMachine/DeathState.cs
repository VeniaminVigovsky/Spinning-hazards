using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : HitState, IState
{
    PlayerInput player;
    PlayerHealth playerHealth;

    public DeathState(PlayerInput playerInput, Animator animator, FiniteStateMachine stateMachine) : base(playerInput, animator, stateMachine)
    {
        player = playerInput;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void Respawn()
    {
        player.transform.position = playerHealth.startingPosition;
        stateMachine.ChangeState(player.fallState);
    }
}

