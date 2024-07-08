using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BoardController : MonoBehaviour
{

    public static BoardController Instance;
    public BoardBase board;
    public Vector2 BoardPadding;
    public Vector2 BoardScale;
    public MapStartData StartData;
    public LevelDificultyData LevelDificulty;
    public UnityEvent OnMapCreated;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateStartMap();
    }

    public void CreateStartMap()
    {
        int posYStart = board.RowsCount-1;
        posYStart -= LevelDificulty.AddLineStart;
        for (int i = 0; i < StartData.Data.Count; i++)
        { 
            if(posYStart-i<=0) break;
            for (int j = 0; j < StartData.Data[i].Cells.Count; j++)
            {
                int number = StartData.Data[i].Cells[j];
                if (number > 0)
                {
                    Vector2Int resultPos=new Vector2Int(j,posYStart-i);
                    var item = BoardItemFabric.Instance.GetItemAtNumber(number);
                    item.transform.position = GetWorldPosAt(resultPos);
                    SetItemAt(resultPos, item,number);
                }
            }
           
        }
        //// set lines
        posYStart += 1;
        for (int i = 0; i < LevelDificulty.AddLineStart; i++)
        {
            List<int> numbers = new List<int>();
            for (int j1 = 0; j1 < board.CellPerRows; j1++)
            {
                numbers.Add(j1);
            }

            int blockCounts = LevelDificulty.AddblockCountRule.GetBlockCounts();
            for (int j = 0; j <blockCounts; j++)
            {
              
                int numberResult =numbers[ Random.Range(0, numbers.Count)];
                numbers.Remove(numberResult);
                Vector2Int resultPos=new Vector2Int(numberResult,posYStart+i);
                int blockNumber = LevelDificulty.RuleBlockNumber.GetBlockNumber(0);
                var item = BoardItemFabric.Instance.GetItemAtNumber(blockNumber);
                item.transform.position = GetWorldPosAt(resultPos);
                SetItemAt(resultPos, item,blockNumber);
            }
          
        }

        OnMapCreated?.Invoke();
    }

    public void SetItemAt(Vector2Int pos,BoardItem item,int number)
    {
        var cells = board.GetCellAt(pos);
        item.Set(number,cells);
    }

    public BoardCell TrayMoveAt(Vector2Int pos)
    {
        var cell = board.GetCellAt(pos);
        if (cell == null) return null;
        if (cell.CanMoveHere())
        {
            return cell;
        }
        return null;
    }
    public int GetCellNumberAt(Vector2Int pos)
    {
        var cell = board.GetCellAt(pos);
        if (cell == null) return -2;
        if (!cell.CanMoveHere())
        {
            if (cell.CurentItem)
            {
                return cell.CurentItem.GetNumber();
            }
        }
        return -2;
    }
    
    public BoardItem GetItemAt(Vector2Int pos)
    {
        var cell = board.GetCellAt(pos);
        if (cell == null) return null;
        if (cell.CurentItem != null)
        {
            return cell.CurentItem;
        }
        return null;

    }
    public BoardCell GetCellAt(Vector2Int pos)
    {
        var cell = board.GetCellAt(pos);
        if (cell == null) return null;
        return cell;

    }

    public Vector2 GetWorldPosAt(Vector2Int pos)
    {
        Vector2 result = BoardPadding;
        result.x += (pos.x * BoardScale.x);
        result.y += (pos.y * BoardScale.y);
        return result;
    }
    [EasyButtons.Button]
    public void MoveDawn()
    {
        board.MoveOllDawn();
        AddRow();
    }

    public void AddRow()
    {
        List<int> numbers = new List<int>();
        for (int j1 = 0; j1 < board.CellPerRows; j1++)
        {
            numbers.Add(j1);
        }

        int blockCounts = LevelDificulty.AddblockCountRule.GetBlockCounts();
        for (int j = 0; j <blockCounts; j++)
        {
              
            int numberResult =numbers[ Random.Range(0, numbers.Count)];
            numbers.Remove(numberResult);
            Vector2Int resultPos=new Vector2Int(numberResult,board.RowsCount-1);
            int blockNumber = LevelDificulty.RuleBlockNumber.GetBlockNumber(ShootController.Instance.GetShotedCount());
            var item = BoardItemFabric.Instance.GetItemAtNumber(blockNumber);
            item.transform.position = GetWorldPosAt(resultPos+Vector2Int.up);
            SetItemAt(resultPos, item,blockNumber);
            item.MoveAtPoint(GetWorldPosAt(resultPos));
        } 
    }

    

    public void BallWasFinished()
    {
        MoveDawn();
        if (!GameManager.Instance.Seted())
        {
            StartCoroutine(EnableShooting());
        }
    }

    IEnumerator EnableShooting()
    {
        yield return new WaitForSeconds(1f);
        ShootController.Instance.SetCanShoot();
    }
    
    
}
