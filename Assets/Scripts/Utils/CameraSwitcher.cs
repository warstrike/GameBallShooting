using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher Instance;
    public List<GameObject> Cameras=new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }

    public void SetCamera(int id)
    {
        if (id < Cameras.Count)
        {
            for (int i = 0; i < Cameras.Count; i++)
            {
                if (i != id)
                {
                    Cameras[i].SetActive(false);
                }
                else
                {
                    Cameras[i].SetActive(true);
                }
             
            }
        }
        
    }
    public void SetCamera(GameObject gm)
    {
        if (!Cameras.Contains(gm))
        {
            Cameras.Add(gm);
        }

        foreach (var cams in Cameras)
        {
            cams.SetActive(false);
        }
        gm.SetActive(true);
        
    }
}