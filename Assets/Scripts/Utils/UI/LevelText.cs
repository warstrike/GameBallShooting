using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    public Text Leveltxt;
    void Start()
    {
        if (!Leveltxt)
        {
            Leveltxt = GetComponent<Text>();
            
        }

        if (Leveltxt)
        {
            Leveltxt.text = "level" + (GamePrefs.GetCurrentLevel() + 1);
        }
    }

    
}
