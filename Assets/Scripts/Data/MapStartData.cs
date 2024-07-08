using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapStartData",menuName = "Data/MapStartData")]
public class MapStartData : ScriptableObject
{
   
   public List<RowsInt> Data=new List<RowsInt>();

   [System.Serializable]
   public class RowsInt
   {
      public  List<int> Cells=new List<int>();
      
   }
}
