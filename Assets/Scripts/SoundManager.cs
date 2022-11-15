using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip coinGrab;
    public AudioClip click;
    public AudioClip jump;

    public AudioSource effectSource;
    private bool isMuted;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleButton()
    {
        isMuted = !isMuted;
        audioSource.mute = isMuted;
    }

    public bool GetMuted()
    {
        return isMuted;
    }

    public void PlayCoinGrabSound()
    {
        if (isMuted) return;
        effectSource.PlayOneShot(coinGrab, 1f);
    }

    public void PlayJumpSound()
    {
        if (isMuted) return;
        effectSource.PlayOneShot(jump, 1f);
    }
    public void PlayClickSound()
    {
        if (isMuted) return;
        effectSource.PlayOneShot(click, 1f);
    }
}
