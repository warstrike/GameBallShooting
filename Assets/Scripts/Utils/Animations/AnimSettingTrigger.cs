using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSettingTrigger : AnimationElementPart
{
    public Animator anim;
    public String TrigerName;
    void Start()
    {
        
    }

    [EasyButtons.Button]
    public void PlayAnimation()
    {
        anim.SetTrigger(TrigerName);
    }

    public override void Play()
    {
        PlayAnimation();
    }
    [EasyButtons.Button]
    public void PlayAnimationHard()
    {
        anim.StopPlayback();
        anim.SetTrigger(TrigerName);
    }
}
