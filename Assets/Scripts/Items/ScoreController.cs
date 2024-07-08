using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
  public static ScoreController Instance;
  public UnityEvent OnScoreChanged;
  private int _curentSavedScore;
  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    ShootController.Instance.OnShoted.AddListener(ScoreChanged);
  }
  private void ScoreChanged()
  {
   
    int curentScore = ShootController.Instance.GetShotedCount();
    _curentSavedScore = curentScore;
    if (curentScore > GetHightScore())
    {
      GamePrefs.SetBestScore(curentScore);
    }
    OnScoreChanged?.Invoke();
  }

  public int GetCurentScore() => _curentSavedScore;
  public int GetHightScore()
  {
    return GamePrefs.GetBestScore();
  }

    
}
