using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public bool AutoLoad=false;
    public LevelLoadConfig ConfigLevel;
    public int SetLevelId;
    public static LevelLoader Instance;
    private void Awake()
    {
        Instance = this;
        if (AutoLoad)
        {
            Load();
        }   
    }
    [EasyButtons.Button]
    public void SetId()
    {
        GamePrefs.SetCurrentLevel(SetLevelId);
    }
    [EasyButtons.Button]
    public void Load()
    {

        int neededScene = ConfigLevel.LevelData[GetNeededLevel()].SceneId;
        var sc=  SceneManager.GetActiveScene().buildIndex;
        if (sc == neededScene)
        {
            IsGoodScene();
        }
        else
        {
            SceneManager.LoadScene(neededScene);
        }
        
    }

    public void IsGoodScene()
    {
        LoadObject();
      //  AppMetricaEventSender.Instance.SendOnLevelStart();
    }

    public void LoadObject()
    {
     
        if (ConfigLevel.LevelData[GetNeededLevel()].Level)
        {
           
            Instantiate(ConfigLevel.LevelData[GetNeededLevel()].Level);
        }
       
    }

    public int GetNeededLevel()
    {
        return GamePrefs.GetCurrentLevel()%  ConfigLevel.LevelData.Count;
    }
}