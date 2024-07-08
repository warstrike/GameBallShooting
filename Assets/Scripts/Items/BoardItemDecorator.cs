
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardItemDecorator : MonoBehaviour
{
   public void SetColor(Color newColor)
   {
      var comp = GetComponent<SpriteRenderer>();
      if (comp)
      {
         comp.color = newColor;
      }
   }
}
