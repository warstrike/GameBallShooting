using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionAndTrigerEvent : MonoBehaviour
{

    public GameObject Target;
   // public object objectSc;
   public bool CheckOnliDefined;
    public UnityEvent OnCollision;
    public UnityEvent OnTrigerEvent;
    

    private void OnTriggerEnter(Collider other)
    {
        if (CheckOnliDefined)
        {


            if (other.gameObject == Target)
            {
                OnTrigerEvent?.Invoke();
            }
        }
        else
        {
            OnTrigerEvent?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (CheckOnliDefined)
        {


            if (other.gameObject == Target)
            {
                OnTrigerEvent?.Invoke();
            }
        }
        else
        {
            OnTrigerEvent?.Invoke();
        }
    }
}
