using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToPointCurve : AnimationElementPart
{
    public Transform MovingTransform;
    
    public Transform ResultObject;
    public float Speed;
    public AnimationCurve Curva;
    public UnityEvent OnAnimationComplited;
    public bool ChangeRotation;
    public bool ChangePosition;
  
   
    [EasyButtons.Button]
    public void PlayAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(StartAnimation());
    }

    public override void Play()
    {
        PlayAnimation();
    }

    IEnumerator StartAnimation()
    {
        Debug.Log("StartedCurveg"+gameObject.name);
        float curentvalue = 0;
        Quaternion StartRot = MovingTransform.rotation;
        Vector3 StartPos = MovingTransform.position;
        while (curentvalue<1f)
        {
            curentvalue += Time.deltaTime*Speed;
            if (ChangeRotation)
            {
                MovingTransform.rotation=Quaternion.Lerp(StartRot,ResultObject.rotation,Curva.Evaluate(curentvalue));
            }
            if (ChangePosition)
            {
                MovingTransform.position=Vector3.Lerp(StartPos,ResultObject.position,Curva.Evaluate(curentvalue));
//                Debug.LogError("Curva"+Curva.Evaluate(curentvalue)+" Main"+curentvalue);
            }
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("FinishedCurve"+gameObject.name);
        yield return new WaitForEndOfFrame();
        OnAnimationComplited?.Invoke();
    }
}
