using System;
using System.Collections;
using System.Collections.Generic;
using TapticPlugin;
using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    [Range(0.1f,100f)]
    public float MaxSpeed;

    [Range(0.1f, 100f)]
    public float AcelerationSpeed = 5f;
    public float SizeObject = 0.25f;
    public float StartSpeed = 1f; 
    private Vector2 MovingVector;
    private float _curentSpeed;
    private float _deadPoint;
    private bool _alive = false;
    private float _maxAliveTime = 30f;
    private float _startTime = 0;
    public UnityEvent OnHited;
    public void Shoot(Vector2 shootVector,float deadpoint)
    {
        _deadPoint = deadpoint;
        MovingVector = shootVector * StartSpeed;
        _curentSpeed = StartSpeed;
        _alive = true;
        _startTime = Time.time;
    }

    
    
    private void FixedUpdate()
    {
        if(!_alive) return;
        CeckHits();
        Move();
        CeckDead();
    }

    public void CeckHits()
    { 
        float distanceToMove = MovingVector.magnitude * Time.fixedDeltaTime;

      float raycastDistance = distanceToMove + SizeObject;

      var hits = HitedObject(raycastDistance, transform.position);
      if (hits)
      {
          Hited(hits);
          return;
      }
      Vector3 rotvec=(Quaternion.AngleAxis(-90, Vector3.forward) * MovingVector.normalized)*SizeObject;
      
      hits = HitedObject(raycastDistance-SizeObject, transform.position+rotvec);
      if (hits)
      {
          //Debug.LogError("OtskokRight");
          Hited(hits);
          return;
      }
      rotvec=(Quaternion.AngleAxis(90, Vector3.forward) * MovingVector.normalized)*SizeObject;
      hits = HitedObject(raycastDistance-SizeObject, transform.position+rotvec);
      if (hits)
      {
       //   Debug.LogError("Otskokleft");
          Hited(hits);
          return;
      }
    
    }

    private RaycastHit2D HitedObject(float raycastDistanc,Vector3 origin)
    {
        var raycastHit= Physics2D.Raycast(origin, MovingVector.normalized, raycastDistanc);
        if (raycastHit)
        {
            return raycastHit;
        }

        return new RaycastHit2D();
    } 

    private void Hited(RaycastHit2D raycastHit)
    {
        MovingVector = Vector2.Reflect(MovingVector, raycastHit.normal);
        if (raycastHit.collider)
        {
            var cmp = raycastHit.collider.GetComponent<IHitable>();
          if(cmp!=null)  cmp.OnHited(transform.position);
          OnHited?.Invoke();
          TapticManager.Impact(ImpactFeedback.Light);
        }
        
    }


    private void Move()
    {
        transform.Translate(MovingVector*Time.fixedDeltaTime,Space.World);
        _curentSpeed = Mathf.Lerp(_curentSpeed, MaxSpeed, AcelerationSpeed * Time.fixedDeltaTime);
        MovingVector = MovingVector.normalized * _curentSpeed;
      
    }

    private void CeckDead()
    {
        if(_startTime+_maxAliveTime<Time.time)  Dead();
        Vector2 curentPos = transform.position;
        if (curentPos.y < _deadPoint && MovingVector.y < 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        if (ShootController.Instance)
        {
            ShootController.Instance.BallWasFinished(this);
        }else Debug.LogError("MisingShootInstace");

        _alive = false;
        gameObject.SetActive(false);
       
    }

  
    /*
    private void OnDrawGizmosSelected()
    {
       
        ;
        Vector2 point = transform.position;
        point+= (MovingVector.normalized * Vector2.right)*SizeObject;
        point = transform.position;
       Vector3 rotvec=(Quaternion.AngleAxis(-90, Vector3.forward) * MovingVector.normalized)*SizeObject;
       point +=new Vector2(rotvec.x,rotvec.y); ;
        Gizmos.DrawSphere(point,0.2f);
        
        point = transform.position;
         rotvec=(Quaternion.AngleAxis(90, Vector3.forward) * MovingVector.normalized)*SizeObject;
        point +=new Vector2(rotvec.x,rotvec.y); ;
        Gizmos.DrawSphere(point,0.2f);
        Gizmos.color=Color.green;
        Gizmos.DrawLine(transform.position,transform.position+new Vector3(MovingVector.normalized.x*2f,MovingVector.normalized.y*2f) );
    }
    */
}
