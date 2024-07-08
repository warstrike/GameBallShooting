using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardItemUI : MonoBehaviour
{
   public BoardItem item;
   public TextMeshPro text;
   public void Awake()
   {
      item.OnSeted.AddListener(SetNumber);
   }

   public void SetNumber(int number)
   {
      text.text = number.ToString();
   }
}
