using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxData",menuName = "Data/FabricData")]
public class FabricData : ScriptableObject
{
   public List<ElementData> Data=new List<ElementData>();
   
   [System.Serializable]
   public class ElementData
   {
      public GameObject Prefab;
      public Color ElementColor;
      
   }

   public ElementData GetCompByNumber(int number)
   {
      number -= 1;
      if (number < Data.Count)
      {
         return Data[number];
      }
      
      return null;
   }

   
}
