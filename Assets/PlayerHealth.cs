using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Vector2 respawnPosition { get; private set; }
    public Vector2 startingPosition { get; private set; }
    int maxHealth;
    public int currentHealth;
    Vector2 offset;
    PlayerInput player;
    public bool invincible;
    float timeBeingInvincible;
    float speed;
    playerSFX playerSFX;
    Animator animator;
    public SpriteRenderer _spriteRenderer;
    public Material defaultMaterial { get; private set; }
    public Material flashMaterial = null;
    WaitForSeconds timeBetweenFlashes = new WaitForSeconds(0.1f);
    Rigidbody2D playerRb2D;

    bool zeroVelocity = false;

    public static bool hasKey;
    float gravScale;


    private void Start()
    {

        playerRb2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = _spriteRenderer.material;
        maxHealth = 5;
        currentHealth = maxHealth;
        offset = new Vector2(0f, 1f);
        player = GetComponent<PlayerInput>();
        startingPosition = transform.position;
        invincible = false;
        timeBeingInvincible = 0.5f;

        gravScale = playerRb2D.gravityScale;
        hasKey = false;
        playerSFX = GetComponent<playerSFX>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.CompareTag("Hazard") & !invincible)
            {
                currentHealth--;
                Score.hitCount++;
                player.onBackwardPlatform = false;
                player.onForwardPlatform = false;
                StopAllCoroutines();
                playerSFX.PlayHitSound();
                if (currentHealth > 0)
                {
                    if (player.stateMachine.currentState != player.hitState)
                    {
                        player.stateMachine.ChangeState(player.hitState);

                    }
                        
                }
                else
                {
                    if (player.stateMachine.currentState != player.deathState)
                    {
                        player.stateMachine.ChangeState(player.deathState);
                        currentHealth = maxHealth;
                        hasKey = false;
                        platformButtonBehav.allButtonreset = true;
                        platformSpinHandler.spin = false;
                    }
                        

                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {

            if (collision.gameObject.CompareTag("Hazard") & !invincible)
            {               
                currentHealth--;
                Score.hitCount++;
                player.onBackwardPlatform = false;
                player.onForwardPlatform = false;
                StopAllCoroutines();
                playerSFX.PlayHitSound();
                if (currentHealth > 0)
                {
                    player.stateMachine.ChangeState(player.hitState);
                }
                else
                {
                    player.stateMachine.ChangeState(player.deathState);
                    currentHealth = maxHealth;
                    hasKey = false;                    
                    platformButtonBehav.allButtonreset = true;
                    platformSpinHandler.spin = false;
                }
            }



            if (collision.gameObject.CompareTag("Respawn") && player.GroundCheck())
            {
                respawnPosition = new Vector2(collision.transform.position.x, collision.transform.position.y) + offset;
            }

        }

        
    }


    public void StartFlash(float startTime, float flashStateLength)
    {
        StopCoroutine(Flash(startTime, flashStateLength));
        StartCoroutine(Flash(startTime, flashStateLength));
    }

    public IEnumerator Flash(float startTime, float flashStateLength)
    {
        invincible = true;
        while (Time.time < startTime + flashStateLength)
        {
            
            _spriteRenderer.material = flashMaterial;
            yield return timeBetweenFlashes;
            _spriteRenderer.material = defaultMaterial;
            yield return timeBetweenFlashes;
            _spriteRenderer.material = flashMaterial;
            yield return timeBetweenFlashes;
            _spriteRenderer.material = defaultMaterial;
            yield return timeBetweenFlashes;
        }
        invincible = false;

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
