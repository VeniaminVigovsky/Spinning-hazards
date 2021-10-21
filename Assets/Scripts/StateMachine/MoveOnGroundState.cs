using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnGroundState : IState, IMoveState
{
    private PlayerInput playerInput;

    public FiniteStateMachine stateMachine { get; set; }

    public Animator animator { get; set; }
    public Rigidbody2D rb { get; set; }

    private playerSFX playerSFX;

    public Vector2 movementVelocity { get; set; }
    public float movementSpeed { get; set; }

    

    float movingPlatformSpeed = 190f;

    public MoveOnGroundState(PlayerInput playerInput, Animator animator, FiniteStateMachine stateMachine)
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
        
        movementVelocity = playerInput.movementVector;       

    }

    public void ExitState()
    {
        
    }

    public void LogicUpdate()
    {

        movementSpeed = playerInput.speed;
        movementVelocity = playerInput.movementVector;

    }

    public void PhysicsUpdate()
    {
        if (playerInput.onBackwardPlatform)
        {
            SetVelocityBackwardPlatform(movementVelocity);
        }

        else if (playerInput.onForwardPlatform)
        {
            SetVelocityForwardPlatform(movementVelocity);
        }

        else if (!playerInput.onForwardPlatform && !playerInput.onBackwardPlatform)
        {
            SetVelocity(movementVelocity);
        }

        

    }

    public void SetVelocity(Vector2 movementVelocity)
    {
        movementVelocity.Normalize();
        rb.velocity = new Vector2(movementVelocity.x * movementSpeed * Time.deltaTime, rb.velocity.y);
    }

    public void SetVelocityForwardPlatform(Vector2 movementVelocity)
    {
        movementVelocity.Normalize();
        rb.velocity = new Vector2(movementVelocity.x * movementSpeed * Time.deltaTime + movingPlatformSpeed * Time.deltaTime, rb.velocity.y);
    }

    public void SetVelocityBackwardPlatform(Vector2 movementVelocity)
    {
        movementVelocity.Normalize();
        rb.velocity = new Vector2(movementVelocity.x * movementSpeed * Time.deltaTime + -movingPlatformSpeed * Time.deltaTime, rb.velocity.y);
    }
}
