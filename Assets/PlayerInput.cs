using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerHealth playerHealth;

    SpinningHazards inputActions;

    public FiniteStateMachine stateMachine { get; private set; }

    Animator animator;

    Rigidbody2D rb;

    public MoveOnGroundState moveOnGroundState { get; set; }

    public MoveOnPlatformState moveOnPlatformState { get; set; }

    public PlatformJumpState platformJumpState { get; set; }

    public HitState hitState { get; set; }

    public DeathState deathState { get; set; }

    public FallState fallState { get; set; }    

    public JumpState jumpState { get; set; }

    float jumpBufferLength = 0.2f;
    float jumpBufferTime = -100f;

    float coyoteTimeLength = 0.25f;

    float coyoteTime = -100f;

    public float speed { get; set; }

    public bool onForwardPlatform { get; set; }
    public bool onBackwardPlatform { get; set; }

    bool canDoubleJump;

    public int jumpCount = 0;
    public bool isJumping = false;

    [SerializeField] Transform groundCheckPosition1;
    [SerializeField] Transform groundCheckPosition2;
    [SerializeField] Transform groundCheckPosition3;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask doubleJumpLayer;

    public Vector2 movementVector { get; private set; }
    public Vector2 oldVector { get; private set; }

    

    bool facingLeft = true;
    private float jumpForce = 500f;
    public bool isJumpingOnPlatform;

    public bool canMove;
    private bool moveAfterHit;

    private void Awake()
    {
        inputActions = new SpinningHazards();
        canMove = true;
        
    }

    private void OnEnable()
    {

        inputActions.Player.Move.performed += MoveInput;        

        inputActions.Player.Jump.performed += JumpInput;

        inputActions.Player.Jump.canceled += CancelJumpInput;
        inputActions.Enable();



    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= MoveInput;
        
        inputActions.Player.Jump.performed -= JumpInput;
        inputActions.Player.Jump.canceled -= CancelJumpInput;
        inputActions.Disable();
    }



    void Start()
    {
        speed = 250f;
        animator = GetComponentInChildren<Animator>();
        stateMachine = new FiniteStateMachine();
        jumpState = new JumpState(this, animator, stateMachine);
        moveOnGroundState = new MoveOnGroundState(this, animator, stateMachine);
        fallState = new FallState(this, animator, stateMachine);
        hitState = new HitState(this, animator, stateMachine);
        deathState = new DeathState(this, animator, stateMachine);
        platformJumpState = new PlatformJumpState(this, animator, stateMachine);

        stateMachine.Initialize(moveOnGroundState);
        rb = GetComponent<Rigidbody2D>();

    }
    


    public void MoveInput(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().normalized.x == 0)
        {
            moveAfterHit = false;
        }
        else moveAfterHit = true;
        movementVector = context.ReadValue<Vector2>();
        
        if (stateMachine.currentState != hitState && stateMachine.currentState != deathState)
            stateMachine.ChangeState(moveOnGroundState);
                  
    }



    void JumpInput(InputAction.CallbackContext context)
    {

        jumpBufferTime = Time.time;

        jumpCount++;        
        ProcessDoubleJump();
    }

    void CancelJumpInput(InputAction.CallbackContext ctxt)
    {

        if (!isJumpingOnPlatform && (stateMachine.currentState != hitState && stateMachine.currentState != deathState))
            stateMachine.ChangeState(fallState);
        
    }


    void ProcessDoubleJump()
    {

        if (canDoubleJump && jumpCount > 1)
        {
            if (stateMachine.GetCurrentState() != jumpState && stateMachine.currentState != hitState)
                stateMachine.ChangeState(jumpState);
        }
    }

    private void Update()
    {       


        if ( GroundCheck())
        {
            isJumpingOnPlatform = false;

            //if (stateMachine.GetCurrentState() != moveOnGroundState)
            //    stateMachine.ChangeState(moveOnGroundState);

            if (Time.time < jumpBufferTime + jumpBufferLength)
            {
                if (stateMachine.GetCurrentState() != jumpState && (stateMachine.currentState != hitState && stateMachine.currentState != deathState) && canMove)
                    stateMachine.ChangeState(jumpState);
            }

        }



        stateMachine.currentState.LogicUpdate();
        Flip();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
        if (GroundCheck() && (stateMachine.currentState != hitState && stateMachine.currentState != deathState))
        {
            stateMachine.ChangeState(moveOnGroundState);
        }
    }
    public bool GroundCheck()
    {

        RaycastHit2D groundCheck1 = Physics2D.Linecast(transform.position, groundCheckPosition1.position, groundLayer);

        RaycastHit2D groundCheck2 = Physics2D.Linecast(transform.position, groundCheckPosition2.position, groundLayer);
        RaycastHit2D groundCheck3 = Physics2D.Linecast(transform.position, groundCheckPosition3.position, groundLayer);


        if (groundCheck1 || groundCheck2 || groundCheck3)
        {
            
            coyoteTime = Time.time;
        }


        if ((groundCheck1 || groundCheck2 || groundCheck3) && Time.time < coyoteTime + coyoteTimeLength)
        {

            return true;
        }

        else 
        {
            
            return false; 
        }



    }

    public bool DoubleCheck()
    {
        RaycastHit2D doubleCheck1 = Physics2D.Linecast(groundCheckPosition1.position, transform.position, doubleJumpLayer);

        RaycastHit2D doubleCheck2 = Physics2D.Linecast(groundCheckPosition2.position, transform.position, doubleJumpLayer);
        RaycastHit2D doubleCheck3 = Physics2D.Linecast(groundCheckPosition3.position, transform.position,  doubleJumpLayer);

        if (doubleCheck1 || doubleCheck2 || doubleCheck3)
        {
            coyoteTime = Time.time;
        }



        if ((doubleCheck1 || doubleCheck2 || doubleCheck3) && Time.time < coyoteTime + coyoteTimeLength)
        {
            return true;
        }
        else return false;
    }

    void Flip()
    {
        if ((movementVector.x > 0 && !facingLeft) || (movementVector.x < 0 && facingLeft))
        {
            transform.Rotate(0f, 180f, 0f);
            facingLeft = !facingLeft;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("doubleJump"))
        {
            canDoubleJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("doubleJump"))
        {
            canDoubleJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BackwardConveyer"))
        {
            onBackwardPlatform = true;
        }

        if (collision.gameObject.CompareTag("ForwardConveyer"))
        {
            onForwardPlatform = true;
        }
        if (collision.gameObject.CompareTag("JumpPlatform"))
        {
            stateMachine.ChangeState(platformJumpState);
            isJumpingOnPlatform = true;
            jumpCount++;
            
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BackwardConveyer"))
        {
            onBackwardPlatform = false;
        }

        if (collision.gameObject.CompareTag("ForwardConveyer"))
        {
            onForwardPlatform = false;
        }
    }

    public void DisableInput()
    {        
        inputActions.Disable();
    }

    public void EnableInput()
    {
        inputActions.Enable();
    }


    public void SetZeroMovementVector()
    {        
        movementVector = Vector2.zero;
    }

    public void SetMovementVector()
    {
        if (moveAfterHit)
        {
            this.movementVector = facingLeft ? new Vector2(1f, 0) : new Vector2(-1f,0f);
        }
    }

}
