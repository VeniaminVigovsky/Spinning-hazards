using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBarCounter : MonoBehaviour
{
    public TextMeshProUGUI healthNumber;
    public PlayerHealth playerHealth;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        healthNumber.text = playerHealth.currentHealth.ToString();
    }
}
