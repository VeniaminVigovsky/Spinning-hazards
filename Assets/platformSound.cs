using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformSound : MonoBehaviour
{
    AudioSource[] audioSources;
    [SerializeField] AudioClip click_1, click_2, click_3;
    void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();

    }


    public void Click()
    {

        foreach(AudioSource audioSource in audioSources)
        {

            audioSource.PlayOneShot(click_1);

        }

    }

    public void Clack()
    {

        foreach (AudioSource audioSource in audioSources)
        {

            audioSource.PlayOneShot(click_2);

        }

    }
    public void Clock()
    {

        foreach (AudioSource audioSource in audioSources)
        {

            audioSource.PlayOneShot(click_3);

        }

    }
}
