using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData",menuName = "Level/Data")]
public class LevelLoadConfig : ScriptableObject
{
    public  List<LevelInformation> LevelData=new List<LevelInformation>();
    [System.Serializable]
    public class LevelInformation
    {
        public GameObject Level;
        public int SceneId;
    }
}