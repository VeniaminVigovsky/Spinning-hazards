using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState :  IState, IMoveState
{

    PlayerInput playerInput;
    public Animator animator { get; set ; }
    public FiniteStateMachine stateMachine { get; set; }
    public Rigidbody2D rb { get; set; }
    public Vector2 movementVelocity { get; set; }
    public float movementSpeed { get; set; }

    float airBorneMovementMultiplier = 1.2f;
    private float minYVelocity = -15f;

    float fallMultiplier = 0.4f;

    public FallState(PlayerInput playerInput, Animator animator, FiniteStateMachine stateMachine)
    {

        this.playerInput = playerInput;
        this.animator = animator;
        rb = playerInput.transform.GetComponent<Rigidbody2D>();
        
        this.stateMachine = stateMachine;
        movementSpeed = playerInput.speed;
    }

    public void EnterState()
    {
        
        movementVelocity = playerInput.movementVector;
        playerInput.isJumping = false;
        SetVelocity(movementVelocity);
        
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
        SetVelocity(movementVelocity);
        ClampVelocityY();
        CutVelocityY();
    }

    public void SetVelocity(Vector2 movementVelocity)
    {
        movementVelocity.Normalize();
        rb.velocity = new Vector2(movementVelocity.x * movementSpeed * airBorneMovementMultiplier * Time.deltaTime, rb.velocity.y);
    }

    public void ClampVelocityY()
    {
        if (rb.velocity.y <= minYVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, minYVelocity);
        }
    }


    public void CutVelocityY()
    {
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fallMultiplier);
        }
    }

}
