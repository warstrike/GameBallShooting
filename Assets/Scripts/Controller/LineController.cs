using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
   public ShootController Controller;
   public LineRenderer Line;
   private bool _work = false;
   public List<float> raycastsTop=new List<float>();
   public int RicoshetCount = 1;
   private void Start()
   {
      if (!Line) Line = GetComponent<LineRenderer>();
      Controller.OnPresed.AddListener(Presed);
      Controller.OnCanceled.AddListener(Canceled);
      Controller.OnShoted.AddListener(Canceled);
   }

   public void Presed()
   {
      _work = true;
   }

   public void Canceled()
   {
      _work = false;
      Line.positionCount = 0;
   }

   private void FixedUpdate()
   {
      if(!Controller) return;
      if (_work)
      {
         if (Controller.PresedMouse())
         {
            if (Controller.GetShootDirection().y > 0)
            {
               RefreshPoints();
            }
            else
            {
               Line.positionCount = 0;
               ;
            }
         }
      }
   }

   public void RefreshPoints()
   {
      Vector2 posStart = Controller.transform.position;
      List<Vector3> positions=new List<Vector3>();
      positions.Add(posStart);
      Vector2 dir = Controller.GetShootDirection();
      Vector3 oldSavedPoint = posStart;
      for (int i = 0; i < RicoshetCount; i++)
      {
         
         var oldPoint =oldSavedPoint + new Vector3(dir.x, dir.y, 0)*0.5f;
         oldPoint.z = 0;
         var raycastHit = Physics2D.Raycast(oldPoint, dir, 50f);
        
         if (raycastHit)
         {
            
            positions.Add(raycastHit.point);
            oldSavedPoint = raycastHit.point - dir * 0.5f;
            dir=Vector2.Reflect(dir, raycastHit.normal);
         }
      }

      Line.positionCount = positions.Count;
      Line.SetPositions(positions.ToArray());


   }

   public RaycastHit2D GetHitPoint(Vector3 old,Vector3 dir)
   {
      if(!Controller.BallObject) return new RaycastHit2D();
      float sizeBall = Controller.BallObject.SizeObject;
      List<RaycastHit2D> raycasts=new List<RaycastHit2D>();
      RaycastHit2D tmpRay = Physics2D.Raycast(old, dir, 50f);
      if(tmpRay)raycasts.Add(tmpRay);
      
      var rotvec=(Quaternion.AngleAxis(-90, Vector3.forward) * dir);
      if ((old + rotvec).x < 6.25f)
      {
         tmpRay = Physics2D.Raycast(old + rotvec, dir, 50f);
         if (tmpRay) raycasts.Add(tmpRay);
      }

      rotvec=(Quaternion.AngleAxis(90, Vector3.forward) * dir);
      if ((old + rotvec).x < 6.25f)
      {

         tmpRay = Physics2D.Raycast(old + rotvec, dir, 50f);
         if (tmpRay) raycasts.Add(tmpRay);
      }

      raycasts.Sort((emp1,emp2)=>emp1.distance.CompareTo(emp2.distance));
       raycastsTop.Clear();
       for (int i = 0; i < raycasts.Count; i++)
       {
          raycastsTop.Add(raycasts[i].distance);
       }
      if(raycasts.Count>0)
       return raycasts[0];
      
      return new RaycastHit2D();


   }
}
