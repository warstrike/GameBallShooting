using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
 private void Start()
 {
  GetComponent<Button>().onClick.AddListener(Click);
 }

 public void Click()
 {
  SceneManager.LoadScene(0);
 }
}
