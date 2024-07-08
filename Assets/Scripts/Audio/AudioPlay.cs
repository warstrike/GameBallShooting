using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioSource Source;
    public float Time = 0.12f;
    public AudioClip Clip;
    [EasyButtons.Button]
    public void Play()
    {
        Source.PlayScheduled(Time);
       
    }
}
