using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScaleAnimation : AnimationElementPart
{
    public Vector3 ResultVector;
    public float Speed;
    public UnityEvent OnComplete;
    void Start()
    {
        
    }


[EasyButtons.Button]
public override void Play()
{
StartCoroutine(Animation());
}

IEnumerator Animation()
{
Vector3 StartValue = transform.localScale;
float curentValue = 0;
    while (curentValue< 1f)
{
    curentValue += Time.deltaTime * Speed;
    transform.localScale = Vector3.Lerp(StartValue, ResultVector, curentValue);
    yield return new WaitForEndOfFrame();
}
OnComplete?.Invoke();
yield return new WaitForEndOfFrame();
}
  
}
