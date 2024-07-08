using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToPointAnimation : MonoBehaviour
{
    public bool CanMove = true;
    public Transform StartPosition;
    public Transform ResultPosition;
    public float Speed=10f;
    public AnimationCurve Curva;
    public float CurentValue;

    public UnityEvent OnComplete;
   

  
    void Update()
    {
        if (CanMove)
        {
            CurentValue += Speed * Time.deltaTime;
            SetPosition();
            if (CurentValue >= 1f)
            {
                Complete();
            }
        }
    }

    public void Complete()
    {
        OnComplete?.Invoke();
        CanMove = false;
    }

    public void SetPosition()
    {
        transform.position = Vector3.Lerp(StartPosition.position, ResultPosition.position, Curva.Evaluate(CurentValue));
        transform.rotation=Quaternion.Lerp(StartPosition.rotation, ResultPosition.rotation, Curva.Evaluate(CurentValue));
        
        
    }

    public void GoOut()
    {
        Transform tmptransform = StartPosition;
        StartPosition = ResultPosition;
        ResultPosition = tmptransform;
        CurentValue = 0;
        GoMove();
    }
    [EasyButtons.Button]
    public void GoMove()
    {
        CanMove = true;

    }
}
