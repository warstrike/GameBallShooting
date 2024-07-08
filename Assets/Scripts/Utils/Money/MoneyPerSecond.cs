using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPerSecond : MonoBehaviour
{
  
      public TextMeshProUGUI MoneyText;
     // public GiveMoneLogic GiveMoneyLogics;
      public float UpdateTimeMin=0.5f;
      public float UpdateTimeMax = 0.05f;

      private float OldUpdatedTime=0;
      
      private void FixedUpdate()
      {
          if (OldUpdatedTime + NeedToUpdateTime() < Time.time) UpdateUI();
      }

      public float NeedToUpdateTime()
      {
        //  return Mathf.Lerp(UpdateTimeMin, UpdateTimeMax, ClickController.Instance.CurentValueMultipler);
          
          return Mathf.Lerp(UpdateTimeMin, UpdateTimeMax, 1f);
      }

    
      public void UpdateUI()
    {
      //  MoneyText.text = MoneyTransformationHelper.GetMoneyText(GiveMoneyLogics.GetMoneyPerSecond())+"/sec";
        OldUpdatedTime = Time.time;
    }
    
}
