﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformSpinHandler : MonoBehaviour
{
    public static bool spin = false;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
    }

    public void Spin()
    {

        animator.SetBool("spin", spin);

    }
    

}
