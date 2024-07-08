using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TapticPlugin;
using UnityEngine;
using UnityEngine.Events;

public class BoardItem : MonoBehaviour,IHitable
{
    private int _number;
    private BoardCell _curentCell;
    private Vector2Int _posOnBoard;
    public UnityEvent<int> OnSeted;
    public UnityEvent OnStartDestroy;
    private bool _blocked = false;
    private bool _IsAlive = true;
    private Vector3 _localScale;
    public BoardItemDecorator decor;
    
    public void Set(int number,BoardCell newcell)
    {
        _number = number;
        SetNewBoardCell(newcell);
        _localScale = transform.localScale;
        OnSeted?.Invoke(_number);
    }
    public void SetNewBoardCell(BoardCell newCell)
    {
        if (_curentCell != null)
        {
            _curentCell.SetFree(this);
        }

        _curentCell = newCell;
        _curentCell.SetNewItem(this);
        _posOnBoard = _curentCell.Pos;
    }
    public void OnHited(Vector3 ballPosition)
    {
       // Debug.LogError("Hited");
        if(!_IsAlive) return;
        if(_blocked) return;
       // Debug.LogError("Hited2");
        var moveDir = GetDirectionHited(ballPosition); 
        //Debug.LogError("Dir"+moveDir);
       var moved= TrayToMove(moveDir);
       if (!moved)
       {
           PlayCanTMove();
       }
    }

    private void PlayCanTMove()
    {
        var secv = DOTween.Sequence();
        secv.Append(transform.DOScale(_localScale / 2f, 0.1f));
        secv.Append(transform.DOScale(_localScale, 0.1f));
    }

    public Vector2Int GetDirectionHited(Vector3 ballPosition)
    {
        Vector2Int result=new Vector2Int();
        Vector3 Dir = (transform.position - ballPosition).normalized;

        if (Mathf.Abs(Dir.y) > Mathf.Abs(Dir.x))
        {
            result=Vector2Int.up;
            Dir.x = Dir.y;
        }
        else
        {
            result=Vector2Int.right;
          
        }

        if (Dir.x < 0) result *= -1;
        return result;
    }
    [EasyButtons.Button]
    public void MoveDown()
    {
      //  if(_blocked) return;
       var newCell= BoardController.Instance.GetCellAt(_posOnBoard+Vector2Int.down);
       _blocked = true;
       if (newCell != null)
       {
           SetNewBoardCell(newCell);
           
       }
       else
       {
           _IsAlive = false;
           if(GameManager.Instance)
           GameManager.Instance.Lose();
       }

       MoveAtPoint(BoardController.Instance.GetWorldPosAt(_posOnBoard ));
    }

    public void MoveAtPoint(Vector3 point)
    {
        _blocked = true;
        transform.DOMove(point,0.3f).OnComplete(() => { AnimationFinished(); });
        
        
    }

    public void AnimationFinished()
    {
        _blocked = false;
        if (_IsAlive) CeckNear();
    }

    public void CeckNear()
    {
          List<Vector2Int> dirs=new List<Vector2Int>();
          dirs.Add(Vector2Int.up);
          dirs.Add(Vector2Int.down);
          dirs.Add(Vector2Int.left);
          dirs.Add(Vector2Int.right);
          List<BoardItem> PosibleItems=new List<BoardItem>();
          foreach (var dir in dirs)
          {
              if (BoardController.Instance.GetCellNumberAt(_posOnBoard + dir)  ==_number)
              {
                  PosibleItems.Add(BoardController.Instance.GetItemAt(_posOnBoard + dir));
              } 
          }
          foreach (var posibleItem in PosibleItems)
          {
              if (posibleItem._IsAlive && !posibleItem._blocked)
              {
                  Anigilation(posibleItem);
                  break;
              }
          }
          
    }

    public void Anigilation(BoardItem anotherItem)
    {
        _blocked = true;
        _IsAlive = false;
        if(anotherItem._IsAlive)
        anotherItem.Anigilation(this);
        Vector2 movePoint = transform.position - (transform.position - anotherItem.transform.position).normalized*
            Vector3.Distance(transform.position,anotherItem.transform.position)/2f;
        transform.DOScale(_localScale / 3f, 0.1f);
        transform.DOMove(movePoint,0.2f).OnComplete(() => { Dead();});

    }
    

    public bool TrayToMove(Vector2Int dir)
    {
       var newcell= BoardController.Instance.TrayMoveAt(_posOnBoard+dir);

       if (newcell!=null)
       {
          
           SetNewBoardCell(newcell);
           MoveAtPoint(BoardController.Instance.GetWorldPosAt(_posOnBoard ));
           return true;
       }
        return false;
    }

    public void Dead()
    {
        _IsAlive = false;
        if (_curentCell!=null)
        {
            _curentCell.SetFree(this);
        }
        OnStartDestroy?.Invoke();
        DestroyAnimation();
    }

    public void DestroyAnimation()
    {
        if (AudioPlayer.Instance)
        {AudioPlayer.Instance.PlaySoundBox();
        }
        var secv = DOTween.Sequence();
        secv.Append(transform.DOScale(0, 0.2f));
        secv.Insert(0,transform.DOShakeRotation(0.2f, 50f));
        secv.OnComplete(() => { DestroyObject(); });
        
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }


    public void SetColor(Color dataElementColor)
    {
        if(decor)decor.SetColor(dataElementColor);
    }

    public int GetNumber() => _number;

}
