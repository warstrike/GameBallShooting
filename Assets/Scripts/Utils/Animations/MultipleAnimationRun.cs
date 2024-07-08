using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleAnimationRun : MonoBehaviour
{
   public  List<AnimationElementPart> MultipleList=new List<AnimationElementPart>();
   public UnityEvent OnStart;
    void Start()
    {
        
    }

    [EasyButtons.Button]
    public void GetOll()
    {
        MultipleList.Clear();
        MultipleList.AddRange(gameObject.GetComponentsInChildren<AnimationElementPart>());
    }
    [EasyButtons.Button]
    public void StartAnimations()
    {
        OnStart?.Invoke();
        foreach (var lista in MultipleList)
        {
            lista.Play();
        }
    }
}
