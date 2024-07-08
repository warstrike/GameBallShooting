using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleDestroy : MonoBehaviour
{
    public float time = 2f;
    void Start()
    {
        Destroy(gameObject,time);
    }

    // Update is called once per frame
   
}
