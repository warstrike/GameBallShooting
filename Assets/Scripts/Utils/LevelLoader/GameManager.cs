using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
  
   private bool wasSeted = false;
   private void Awake()
   {
      Instance = this;
   }
   

   private void Start()
   {
     // StartCoroutine(waitForTap());
   }

   IEnumerator waitForTap()
   {
      while (!isStart())
      {
         yield return new WaitForEndOfFrame();
      }

      PlayGame();
   }

   public bool isStart()
   {
      if (Input.GetMouseButton(0))
      {
         if (Screen.height- Input.mousePosition.y >300 )
         {
       
            return true;
         }
      }
      return false;
   }

   public void PlayGame()
   {

     
      UIManager.Instance.PlayGame();
    
   }

   public void Win()
   {
     if(wasSeted) return;
     wasSeted = true;
      GamePrefs.SetCurrentLevel(GamePrefs.GetCurrentLevel()+1);
      UIManager.Instance.Win();
    
   }

   public void Lose()
   {
      if(wasSeted) return;
      wasSeted = true;
      UIManager.Instance.Lose();
   
   }

   public bool Seted() => wasSeted;
}
