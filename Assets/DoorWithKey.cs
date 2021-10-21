using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorWithKey : MonoBehaviour
{
    public GameObject nextLevelOpener;
    GameObject text;
    

    private void Awake()
    {

        text = GameObject.Find("needkey");
    }
    private void Start()
    {
        if (text != null)
        {

            text.SetActive(false);
        }
        else return;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerHealth.hasKey)
            {
                nextLevelOpener.SetActive(true);
                Time.timeScale = 0f;
                MenuHandler.nextLevelSelection = true;
                

            }

           else
            {

                text.SetActive(true);

            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           

            
           

                text.SetActive(false);

            

        }
    }

}
