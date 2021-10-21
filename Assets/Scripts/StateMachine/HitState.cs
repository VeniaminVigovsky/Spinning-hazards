using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitState : IState
{
    PlayerInput player;

    PlayerHealth playerHealth;
    public Animator animator { get; set; }
    public FiniteStateMachine stateMachine { get; set; }
    public Rigidbody2D rb { get; set; }

    float startTime;
    private float flashStateLength = 0.4f;
    private float timeBeforeRespawn = 0.5f;

    float gravScale;
    



    public HitState(PlayerInput playerInput, Animator animator, FiniteStateMachine stateMachine)
    {
        player = playerInput;
        playerHealth = player.GetComponent<PlayerHealth>();
        this.animator = animator;
        this.stateMachine = stateMachine;
        rb = player.GetComponent<Rigidbody2D>();        
        
    }



    public virtual void EnterState()
    {
        player.canMove = false; 
        player.SetZeroMovementVector();
        animator.SetTrigger("player_hurt");
        startTime = Time.time;
        playerHealth.StartFlash(startTime, flashStateLength);
        rb.velocity = new Vector2(0f, 0f);
        gravScale = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.isKinematic = true;
        
        
    }

    public virtual void ExitState()
    {
        rb.isKinematic = false;
        rb.gravityScale = gravScale;
        player.canMove = true;
        player.SetMovementVector();       
        
    }

    public virtual void LogicUpdate()
    {
        if (Time.time > startTime + timeBeforeRespawn)
        {
            Respawn();
        }
    }

    public void PhysicsUpdate()
    {
        rb.velocity = new Vector2(0f, 0f);
    }

    public virtual void Respawn()
    {
        rb.isKinematic = false;
        rb.gravityScale = gravScale;
        player.canMove = true;
        player.transform.position = playerHealth.respawnPosition;      

        
        stateMachine.ChangeState(player.moveOnGroundState);

        
    }
}
