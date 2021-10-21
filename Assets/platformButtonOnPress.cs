using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformButtonOnPress : MonoBehaviour
{
    public bool pressed;
    Vector3 initialPosition;
    Rigidbody2D buttonRb;

    
    void Start()
    {
        pressed = false;
        initialPosition = transform.position;
        buttonRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (platformButtonBehav.allButtonreset == true)
        {

            pressed = false;

            transform.position = initialPosition;
            buttonRb.isKinematic = true;
            

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            platformButtonBehav.allButtonreset = false;
            buttonRb.isKinematic = false;
            if (pressed == false)
            {
                platformSpinHandler.spin = !platformSpinHandler.spin;
                pressed = true;

            }
            else return;




        }
    }

}
