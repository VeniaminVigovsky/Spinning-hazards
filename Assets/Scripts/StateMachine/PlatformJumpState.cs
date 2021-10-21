using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformJumpState : IState, IMoveState
{
    public FiniteStateMachine stateMachine { get; set; }
    public Animator animator { get; set; }

    PlayerInput playerInput;

    playerSFX playerSFX;

    public Vector2 movementVelocity { get; set; }

    private float jumpForce = 500f;

    float airBorneMovementMultiplier = 1.2f;


    public Rigidbody2D rb { get; set; }
    public float movementSpeed { get; set; }

    public PlatformJumpState(PlayerInput playerInput, Animator animator, FiniteStateMachine stateMachine)
    {
        this.playerInput = playerInput;
        this.animator = animator;
        rb = playerInput.transform.GetComponent<Rigidbody2D>();
        playerSFX = playerInput.transform.GetComponent<playerSFX>();
        this.stateMachine = stateMachine;
        movementSpeed = playerInput.speed;

    }



    public void EnterState()
    {
        playerInput.isJumpingOnPlatform = true;        
        movementVelocity = playerInput.movementVector;
        SetJumpVelocity();
        SetVelocity(movementVelocity);
        animator.SetTrigger("player_jump");
        playerSFX.PlayJumpSound(0.8f);
    }

    public void ExitState()
    {
        playerSFX.ResetPitch();
        playerInput.isJumpingOnPlatform = false;
    }

    public void LogicUpdate()
    {
        movementSpeed = playerInput.speed;
        movementVelocity = playerInput.movementVector;
    }

    public void PhysicsUpdate()
    {
        SetVelocity(movementVelocity);


    }

    public void SetVelocity(Vector2 movementVelocity)
    {
        movementVelocity.Normalize();
        rb.velocity = new Vector2(movementVelocity.x * movementSpeed * airBorneMovementMultiplier * Time.deltaTime, rb.velocity.y);
    }

    public void SetJumpVelocity()
    {
        rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }
}
