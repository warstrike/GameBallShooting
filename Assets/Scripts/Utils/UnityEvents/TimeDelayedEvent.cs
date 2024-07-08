using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeDelayedEvent : MonoBehaviour
{
    public float Time;
    public UnityEvent OnDelayFinish;
    public bool StartOnPlay = false;
    void Start()
    {
     if(StartOnPlay)   StartEvent();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(Time);
        StartWithoutDelay();
    }

    public void StartEvent()
    {
        StartCoroutine(Delay());
    }

    public void StartWithoutDelay()
    {
        OnDelayFinish?.Invoke();
    }
}
