using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip introClip = null;
    [SerializeField] AudioClip loopClip = null;


    // Start is called before the first frame update
    void Start()
    {
        double introDuration = (double)introClip.samples / introClip.frequency;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(introClip);
        audioSource.clip = loopClip;
        audioSource.PlayScheduled(AudioSettings.dspTime + introDuration);
    }




}
