using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    
    [SerializeField] private AudioSource audioSrc;

    [SerializeField] private List<AudioClip> clips;

    public List<AudioClip> Clips
    {
        get => clips;
    }

    private void Awake()
    {
        soundManager = this;
    }

    public void PlaySnapShot(int index)
    {
        audioSrc.PlayOneShot(clips.ElementAt(index));
    }
}
