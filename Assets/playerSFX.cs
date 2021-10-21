using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSFX : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip currentClip;
    [SerializeField] AudioClip hitAudioClip = null;
    [SerializeField] AudioClip jumpAudioClip = null;
    [SerializeField] AudioClip keyAudioClip = null;

    float lastJumpSoundPitch = 0;
    private float lastTimeClipStarted = -100f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = 1;



    }

    public void PlayJumpSound(float pitch)
    {
                



        if (pitch == lastJumpSoundPitch && lastTimeClipStarted + jumpAudioClip.length * 0.4f > Time.time)
        {            
            return;
        }
        else if (pitch != lastJumpSoundPitch && lastTimeClipStarted + jumpAudioClip.length * 0.4f <= Time.time)
        {
                        
            audioSource.Stop();
            lastJumpSoundPitch = pitch;
            audioSource.pitch = pitch;            
            audioSource.PlayOneShot(jumpAudioClip);
            lastTimeClipStarted = Time.time;
        }




    }

    public void PlayHitSound()
    {
        audioSource.Stop();
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(hitAudioClip);

    }

    public void PlayKeySound()
    {
        audioSource.Stop();
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(keyAudioClip);

    }


    public void ResetPitch()
    {
        lastJumpSoundPitch = 0;
    }
}
