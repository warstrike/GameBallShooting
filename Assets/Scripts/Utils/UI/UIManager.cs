using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  public GameObject StartUI;
  public GameObject MainUI;
  public GameObject WinUI;
  public GameObject LoseUI;
  public GameObject ChoseUI;
  public float DelayToWin;
  public float DelayToLose;
  public static UIManager Instance;
  

  private void Awake()
  {
    Instance = this;
  }

  public void Win()
  {
    Invoke("SetWin",DelayToWin);
  }

  private void SetWin()
  {
   if(MainUI) MainUI.SetActive(false);
   if(WinUI) WinUI.SetActive(true);
  }

  public void Lose()
  {
    Invoke("SetLose",DelayToLose);
  }
  private void SetLose()
  {
    if(MainUI) MainUI.SetActive(false);
    if(LoseUI) LoseUI.SetActive(true);
  }

  public void PlayGame()
  {
    if (StartUI)
    {
      StartUI.SetActive(false);
    }

    if (ChoseUI)
    {
      ChoseUI.SetActive(false);
    }
    if(MainUI) MainUI.SetActive(true);
    
    
  }
}
