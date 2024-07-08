using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance;
    public AudioClip BoxClip;
    public AudioSource Source;
    private void Awake()
    {
        Instance = this;
    }

    public void PlaySoundBox()
    {
        Source.PlayOneShot(BoxClip);
    }
}
