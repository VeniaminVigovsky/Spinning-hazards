using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRigidBody;
    public float speed;
    public bool isGrounded;

    public bool canJump;
    float jumpVelocity;
    float airBorneMovementMultiplier;
    bool onPlatform;
    public static bool hasKey;
    float jumpForce;
    public bool canDoubleJump;
    bool canResetButtons;
    playerSFX playerSFX;
    public bool doubleJump;
    Animator animator;
    bool facingLeft = true;
    [SerializeField] GameObject playerSprite = null;
    PlayerHealth playerHealth;

    float jumpBufferTime = 0.2f;
    public float jumpButtonPressedTime;
    float leftGroundTime = -2f;
    bool waitForGroundLeft;

    bool onForwardConveyer;
    bool onBackwardConveyer;

    float conveyerForce = 160f;
    float maxFallingVelocity = -15f;


    void Start()
    {
        jumpButtonPressedTime = Time.time - jumpBufferTime;
        playerHealth = GetComponent<PlayerHealth>();

        animator = GetComponentInChildren<Animator>();

        playerRigidBody = GetComponent<Rigidbody2D>();

        speed = 220f;

        jumpVelocity = 320f;

        airBorneMovementMultiplier = 42f;

        onPlatform = false;

        hasKey = false;

        jumpForce = jumpVelocity * 2;

        playerSFX = GetComponent<playerSFX>();
    }

       






    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject != null)
        {

            if (collision.gameObject.CompareTag("Key"))
            {

                hasKey = true;
                playerSFX.PlayKeySound();

            }
        }               

        
    }



    
}
