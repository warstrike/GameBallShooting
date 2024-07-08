using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurentMoneyUI : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public float UpdateTimeMin=0.5f;
    public float UpdateTimeMax = 0.05f;

    private float OldUpdatedTime=0;
    void Start()
    {
      
    }

    private void FixedUpdate()
    {
        if (OldUpdatedTime + NeedToUpdateTime() < Time.time) UpdateUI();
    }

    public float NeedToUpdateTime()
    {
       // return Mathf.Lerp(UpdateTimeMin, UpdateTimeMax, ClickController.Instance.CurentValueMultipler);
        return Mathf.Lerp(UpdateTimeMin, UpdateTimeMax, 1f);
    }

    public void UpdateUI()
    {
        MoneyText.text = MoneyTransformationHelper.GetMoneyText(GamePrefs.GetMoney());
        OldUpdatedTime = Time.time;
    }

   
}
