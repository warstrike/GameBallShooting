using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBase : MonoBehaviour
{
   public List<BoardRow> Rows=new List<BoardRow>();
   public int CellPerRows = 5;
   public int RowsCount = 7;
   public static BoardBase Instance;
   private void Awake()
   {
      Instance = this;
      InitRows();
   }

   public void InitRows()
   {
      for (int i = 0; i < RowsCount; i++)
      {
         var row = new BoardRow(CellPerRows,i);
         Rows.Add(row);
      }
      
   }

   
   
   
   [System.Serializable]
   public class BoardRow
   {
      public List<BoardCell> Cell=new List<BoardCell>();
      public BoardRow(int CountCell,int posy)
      {
         Cell=new List<BoardCell>();
         for (int i = 0; i < CountCell; i++)
         {
            
            Cell.Add(new BoardCell(new Vector2Int(i,posy)));
         }
      }
     
   }

   public BoardCell GetCellAt(Vector2Int position)
   {
      if (position.y >= RowsCount ||position.y<0) return null;
      if(position.x >= CellPerRows ||position.x<0)return null;
      return Rows[position.y].Cell[position.x];
   }

   public void MoveOllDawn()
   {
      foreach (var boardRow in Rows)
      {
         foreach (var boardCell in boardRow.Cell)
         {
            boardCell.MoveDown();
         }
      }
   }
}
