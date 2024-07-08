using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToPointCurveRotationAndPadding : AnimationElementPart
{
    public Transform MovingTransform;
    
    public Transform ResultObject;
    public float Speed;
    public AnimationCurve Curva;
    public UnityEvent OnAnimationComplited;
    [Header("Rotation")]
    public AnimationCurve RotationXCurve;
    public AnimationCurve RotationYCurve;
    public AnimationCurve RotationZCurve;
    [Header("Position")]
    public AnimationCurve PositionXCurvePad;
    public AnimationCurve PositionYCurvePad;
    public AnimationCurve PositionZCurvePad;
    public bool ChangeRotation;
    public bool ChangePosition;

    public float TestValue= 0;
   
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
    [EasyButtons.Button]
    public void StartTestV()
    {
        StartCoroutine(StartAnimationPaused());
    }
    
    IEnumerator StartAnimationPaused()
    {
        Debug.Log("StartedCurveg"+gameObject.name);
        float curentvalue = 0;
        Quaternion StartRot = MovingTransform.rotation;
        Vector3 StartPos = MovingTransform.position;
        while (curentvalue<50f)
        {
           // curentvalue += Time.deltaTime*Speed;
           curentvalue = TestValue;
            if (ChangeRotation)
            {
                Vector3 ResultAngle=new Vector3(RotationXCurve.Evaluate(curentvalue),RotationYCurve.Evaluate(curentvalue),RotationZCurve.Evaluate(curentvalue));
                MovingTransform.rotation=Quaternion.Lerp(StartRot,Quaternion.Euler(ResultAngle), 2);
            }
            if (ChangePosition)
            {
                Vector3 resultPadding=new Vector3(PositionXCurvePad.Evaluate(curentvalue),PositionYCurvePad.Evaluate(curentvalue),PositionZCurvePad.Evaluate(curentvalue));
                MovingTransform.position=Vector3.Lerp(StartPos,ResultObject.position,Curva.Evaluate(curentvalue))+resultPadding;
//               
            }
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("FinishedCurve"+gameObject.name);
        yield return new WaitForEndOfFrame();
        OnAnimationComplited?.Invoke();
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
                Vector3 ResultAngle=new Vector3(RotationXCurve.Evaluate(curentvalue),RotationYCurve.Evaluate(curentvalue),RotationZCurve.Evaluate(curentvalue));
                MovingTransform.rotation=Quaternion.Lerp(StartRot,Quaternion.Euler(ResultAngle), 2);
            }
            if (ChangePosition)
            {
                Vector3 resultPadding=new Vector3(PositionXCurvePad.Evaluate(curentvalue),PositionYCurvePad.Evaluate(curentvalue),PositionZCurvePad.Evaluate(curentvalue));
                MovingTransform.position=Vector3.Lerp(StartPos,ResultObject.position,Curva.Evaluate(curentvalue))+resultPadding;
//               
            }
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("FinishedCurve"+gameObject.name);
        yield return new WaitForEndOfFrame();
        OnAnimationComplited?.Invoke();
    }
}
