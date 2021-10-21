using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (PlayerHealth.hasKey)
        {

            spriteRenderer.enabled = false;
            boxCollider.enabled = false;

        }

        else
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;

        }
    }
}
