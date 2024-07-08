using System.Collections;
using System.Collections.Generic;
//using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyAddCanvAnimation : MonoBehaviour
{
    public bool LookToCamera=true;
    public float UpSpeed = 5f;
    public float VewTime = 3f;
    public bool Fade = true;
    public float FadeTime = 1.5f;
    public TextMeshProUGUI texts;
    void Start()
    {
        Destroy(gameObject,VewTime);
     /// if(Fade)  texts.DOFade(0, FadeTime);
    }

    public void SetMoney(string monewy)
    {
        texts.text = monewy+"$";
    }

   
    void Update()
    {
        if (LookToCamera)
        {
            transform.LookAt(Camera.main.transform);
        }
        transform.Translate(Vector3.up*Time.deltaTime*UpSpeed,Space.World);
    }
}
