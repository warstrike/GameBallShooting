using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleEventsOnKey : MonoBehaviour
{
    public KeyCode key;
    private bool Presed = false;
    public List<UnityEvent> Events=new List<UnityEvent>();
    private int curentClicked = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key))
        {
            if (!Presed)
            {
                OnClicked();
            }
            Presed = true;
        }
        else
        {
            Presed = false;
        }   
    }

    public void OnClicked()
    {
        if (curentClicked < Events.Count)
        {
            Events[curentClicked]?.Invoke();
        }
        curentClicked++;
    }
}
