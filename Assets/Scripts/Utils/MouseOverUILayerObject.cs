using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class MouseOverUILayerObject
{
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == 5) //5 = UI layer
            {
                return true;
            }
        }

        return false;
    }
    
    public static  bool IsPointerOverUIElement()
    {
        bool BlockedByUI = false;
        foreach (Touch touch in Input.touches)
        {
            int pointerID = touch.fingerId;
            // if (EventSystem.current.IsPointerOverGameObject(pointerID))
            //  {

            //     return;
            //  }
            if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(pointerID))
            {
                BlockedByUI = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (BlockedByUI) BlockedByUI = false;
                // here we don't know if the touch was over an canvas UI
               // return  true;
            }
        }

        if (BlockedByUI) return  true;


        if (Input.GetMouseButton(0))
        {
            if(EventSystem.current.IsPointerOverGameObject()) return true;
            if (MouseOverUILayerObject.IsPointerOverUIObject())
            {
                return  true;
            }
            if (BlockedByUI) return true ;
           
        }
        else
        {
         

        }

        return false;
    }
}