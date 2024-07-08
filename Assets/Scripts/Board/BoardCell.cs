using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell
{
   public BoardItem CurentItem;
   public Vector2Int Pos;

  public BoardCell(Vector2Int newpos)
  {
     Pos = newpos;
  }
   public void SetNewItem(BoardItem newItem)
   {
      CurentItem = newItem;
   }

   public void MoveDown()
   {
      if (CurentItem)
      {
         CurentItem.MoveDown();
      }
   }
   public void SetFree( )
   {
      CurentItem = null;
   }
   public void SetFree(BoardItem oldItem )
   {
      if(oldItem==CurentItem)
      CurentItem = null;
   }

   public bool CanMoveHere()
   {
      return CurentItem == null;
   }
}
