using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_menu_behav : MonoBehaviour
{
    
    PlayerMovement playerMovement;
    Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            StartCoroutine(PlayerCycle());

        }

        else StopAllCoroutines();
    }


    IEnumerator PlayerCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            playerMovement.canJump = true;
            animator.SetTrigger("player_jump");
            

        }



    }

}
