using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyEvents : MonoBehaviour
{
    public KeyCode key;
    private bool IsPresed = false;
    public UnityEvent OnKeyPresed;
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetKey(key))
        {
            if (!IsPresed)
            {
                IsPresed = true;
                OnKeyPresed?.Invoke();
            }
        }
        else
        {
            IsPresed = false;
        }
    }
}
