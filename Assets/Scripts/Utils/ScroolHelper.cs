using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ScroolHelper : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
    {
        public GameObject _Scroolbar;
        private float scrol_pos = 0;
    [SerializeField]    private float[] pos;
        public int curentPage = 0;
        private float distance;
        public float SpeedMoveNext = 0.2f;
        public delegate void OnChangeDeleg(int newPages);

        public Coroutine refCorunthine;
        public OnChangeDeleg PageChanged;
        public bool WasClikedButton = false;
        private int GoPage;
        public Transform Content;
        public Vector2 StartPosition=Vector2.zero;
        public Vector2 CurentPosMouse=Vector2.zero;
        public GameObject nextButton;
        public GameObject backButton;
        public int pagesNum = 0;
        private void Start()
        {
            StartCoroutine(prepare());
        }

        IEnumerator prepare()
        {
            yield return new WaitForSeconds(0.02f);
            pos = new float[Content.childCount];
            distance = 1f / (pos.Length - 1f);
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = distance * i;
            
            }

            pagesNum = Content.childCount;
            yield return null;
        }

        public void OnDrag(PointerEventData eventData)
        {
            CurentPosMouse = Input.mousePosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            scrol_pos = _Scroolbar.GetComponent<Scrollbar>().value;
          //  Debug.LogError("DragStarted");
            //StopCoroutine(GoAnotherPages());
            StartPosition = Input.mousePosition;
            
            StopAllCoroutines();
        }

        public void OnEndDrag(PointerEventData eventData)
        { scrol_pos = _Scroolbar.GetComponent<Scrollbar>().value;
            
            
            StopAllCoroutines();
         //   Debug.LogError("Suka Bliati");
            ControllSwipe();
        }

        public void ControllSwipe()
        {
            if (StartPosition != Vector2.zero)
            {
                
            }
         
            if (Vector2.Distance(Input.mousePosition, CurentPosMouse) > 10)
            {
                if ((Input.mousePosition.x - CurentPosMouse.x) > 0)
                {
                    ClickBackword();
                }
                else
                {
                    ClickNext();
                }
            }
            else
            {
                StartCoroutine(GoAnotherPages());
            }
        }
       

        IEnumerator GoAnotherPages()
        {
            while (true)
            {


                for (int i = 0; i < pos.Length; i++)
                {
                    if (scrol_pos < pos[i] + (distance / 2) && scrol_pos > pos[i] - (distance / 2))
                    {
                        _Scroolbar.GetComponent<Scrollbar>().value =
                            Mathf.Lerp(_Scroolbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                        if (curentPage != i)
                        {
                            PageChanged?.Invoke(i);
                            curentPage = i;
                        }

                        if (Math.Abs(_Scroolbar.GetComponent<Scrollbar>().value - pos[i]) < 0.0005f)
                        {
                            _Scroolbar.GetComponent<Scrollbar>().value = pos[i];
                            //    Mathf.Lerp(_Scroolbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                       //   Debug.LogError("sucika");
                            yield break;
                             break;
                             
                             ;
                        }
                    }
                }

                yield return  new WaitForEndOfFrame();
            }
            yield return null;
        }
        [EasyButtons.Button]
        public void ClickNext()
        {
           // nextButton.GetComponent<Button>().interactable = false;
          //  backButton.GetComponent<Button>().interactable = true;
            UpdatePageButtons(curentPage + 1);
            StopAllCoroutines();
          if(curentPage+1<pos.Length)
            StartCoroutine(GoAnotherPage(curentPage + 1));
        }

        public void UpdatePageButtons(int goPages)
        {
            if (goPages < pagesNum - 1)
            {
              if(nextButton)  nextButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                if(nextButton)    nextButton.GetComponent<Button>().interactable = false;
            }

            if (goPages < 1)
            {
                if(backButton)    backButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                if(backButton)   backButton.GetComponent<Button>().interactable = true;
            }
        }
        IEnumerator GoAnotherPage(int newPage)
        {
           // StopAllCoroutines();
            while (true)
            {
                
                
                    _Scroolbar.GetComponent<Scrollbar>().value =
                        Mathf.Lerp(_Scroolbar.GetComponent<Scrollbar>().value, pos[newPage], SpeedMoveNext);
                    

                    if (Math.Abs(_Scroolbar.GetComponent<Scrollbar>().value - pos[newPage]) < 0.001f)
                    {
                        _Scroolbar.GetComponent<Scrollbar>().value = pos[newPage];
                        if (curentPage !=newPage)
                        {
                           
                            curentPage = newPage;
                            PageChanged?.Invoke(newPage);
                        }
                        break;
                    }
                
                yield return  new WaitForEndOfFrame();
            }
            
            yield return null;
        }
        [EasyButtons.Button]
        public void ClickBackword()
        {
            UpdatePageButtons(curentPage - 1);
            StopAllCoroutines();
           
            if(curentPage-1>=0)
            StartCoroutine(GoAnotherPage(curentPage - 1));
        }
    }
}
