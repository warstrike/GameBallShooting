using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShootController : MonoBehaviour
{

    public static ShootController Instance;
    
    private bool _canShoot = true;
    private float _BallInitPosition;

    private Vector3 _mousePos;
    private bool _presed;
    private int _shotedCount=0;
    public UnityEvent OnPresed;
    public UnityEvent OnShoted;
    public UnityEvent OnCanceled;
    public BallController BallObject;
    public float Sensevity = 10f;
    private Vector2 _ShootDirection;
    
    private void Awake()
    {
        Instance = this;
    }
   
    private void FixedUpdate()
    {
        CheckMouse();
    }
    public int GetShotedCount()=>_shotedCount;
    #region MouseCecking
    
  
    private void CheckMouse()
    {
        if(!_canShoot) return;
        if (MouseOverUILayerObject.IsPointerOverUIElement()) return;
        if (Input.GetMouseButton(0))
        {

            if (!_presed)
            {
                MousePresed();
            }
            else
            {
                _ShootDirection = Vector2.Lerp(_ShootDirection, GetDirection(), Sensevity * Time.fixedDeltaTime);
            }
        }
        else
        {
            if (_presed) Release();
        }
    }

    private void MousePresed()
    {
        _presed = true;
        _mousePos =Camera.main.WorldToScreenPoint(transform.position) ;
        _ShootDirection = GetDirection();
        OnPresed?.Invoke();
    }
    private void MouseCanceled()
    {
        _presed = false;
        OnCanceled?.Invoke();
        
    }
    private void Release()
    {
        _presed = false;
        Shoot(GetShootDirection());
        OnShoted?.Invoke();
    }

    public Vector2 GetStartMousePosition()
    {
        return _mousePos;
    }
    public Vector2 GetDirection()
    {
        return   (Input.mousePosition-_mousePos).normalized;
    }
    public Vector2 GetShootDirection()
    {
        return   _ShootDirection;
    }

    public bool PresedMouse()
    {
        return _presed;
    }
    #endregion

    public void Shoot(Vector2 direction)
    {
        if (direction.y > 0)
        {
            _canShoot = false;

            BallObject.Shoot(direction,transform.position.y);
            _shotedCount++;
        }
    }

    

    public void SetCanShoot()
    {
        _canShoot = true;
        Vector2 oldpos = transform.position;
        transform.position=new Vector2(BallObject.transform.position.x,oldpos.y);
        BallObject.transform.position = transform.position;
        BallObject.gameObject.SetActive(true);
    }


    public void BallWasFinished(BallController ballController)
    {
       BoardController.Instance.BallWasFinished();
    }
}
