using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardItemFabric : MonoBehaviour
{
   public static BoardItemFabric Instance;
   public FabricData Data;
   private void Awake()
   {
      Instance = this;
   }

   public BoardItem GetItemAtNumber(int number)
   {
      var data = Data.GetCompByNumber(number);
      var gm = Instantiate(data.Prefab, transform);
      var itemCmp = gm.GetComponent<BoardItem>();
      itemCmp.SetColor(data.ElementColor);
      return itemCmp;
   }
}
