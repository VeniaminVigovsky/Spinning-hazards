using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLogic : MonoBehaviour
{
    Vector3 initialPosition;
    Rigidbody2D rb;
    [SerializeField]
    GameObject animatedBoom;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatedBoom?.SetActive(false);
        initialPosition = transform.position;
    }

    private void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;

        if (collision.gameObject.CompareTag("Player"))
        {
            animatedBoom.transform.position = transform.position;
            StartCoroutine(OnPlayerHit());
            animatedBoom.SetActive(true);

        }
        else
        {
            
            transform.position = initialPosition;
        }
        

    }

    IEnumerator OnPlayerHit()
    {
        rb.isKinematic = true;
        transform.position = new Vector3(0f, 150f, 0);
        yield return new WaitForSeconds(0.8f);
        transform.position = initialPosition;
        rb.isKinematic = false;
        StopCoroutine(OnPlayerHit());
    }
}
