using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class X5Button : MonoBehaviour
{
    public Color DisableColor=Color.white;
    public Color EnableColor=Color.green;
    public Button button;
    public void Start()
    {
        Time.timeScale = 1f;
        if (!button) button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        if (Time.timeScale > 1.5f)
        {
            Time.timeScale = 1f;
            SetColor(DisableColor);
        }
        else
        {
            Time.timeScale = 5f;
            SetColor(EnableColor);
        }
    }

    private void SetColor(Color newcol)
    {
        button.GetComponent<Image>().color = newcol;
    }
}
