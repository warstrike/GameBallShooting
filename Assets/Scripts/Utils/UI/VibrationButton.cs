using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VibrationButton : MonoBehaviour
{
    public Image img;
    public Sprite OnSprite;
    public Sprite OffSprtie;
    void Start()
    {
        Refresh();
        GetComponent<Button>().onClick.AddListener(Click);
    }

    public void Click()
    {
        GamePrefs.SetVibrate(!GamePrefs.GetVibrate());
        Refresh();
    }

    public void Refresh()
    {
        if (GamePrefs.GetVibrate())
        {
            img.sprite = OnSprite;
        }
        else
        {
            img.sprite = OffSprtie;
        }
    }
}
