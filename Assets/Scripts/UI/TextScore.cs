using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScore : MonoBehaviour
{
   public TextMeshProUGUI TextHightScore;
   public TextMeshProUGUI TextCurentScore;

   public void Start()
   {
      if (ScoreController.Instance)
      {
         ScoreController.Instance.OnScoreChanged.AddListener(Refresh);
      }

      Refresh();
   }

   public void Refresh()
   {
      if (ScoreController.Instance)
      {
         int curentScore = ScoreController.Instance.GetCurentScore();
         TextCurentScore.text = "Curent Score: " + curentScore.ToString();
      }

      TextHightScore.text = "Hight Score: " + GamePrefs.GetBestScore();
   }
}
