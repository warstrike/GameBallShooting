using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DificultyData",menuName = "Data/LevelDificulty")]
public class LevelDificultyData : ScriptableObject
{
    public int AddLineStart = 1;
    public AddNewBlockCount AddblockCountRule;
    public BlockNumberRules RuleBlockNumber;
    [System.Serializable]
    public class AddNewBlockCount
    {

        public List<int> PriorityPerBlock=new List<int>();

        public int GetBlockCounts()
        {
            int sum=0;
            
            for (int i = 0; i < PriorityPerBlock.Count; i++)
            {
                sum += PriorityPerBlock[i];
            }

           List<float> change=new List<float>();
           
           for (int i = 0; i < PriorityPerBlock.Count; i++)
           {
               change.Add(((float)PriorityPerBlock[i]/sum));
           }

           float randNumber = Random.Range(0, 1f);
           float sumOld = 0;
           int result = 1;
           for (int i = 0; i < change.Count; i++)
           {
               sumOld += change[i];
               if (randNumber < sumOld)
               {
                   result= i+1;
                   break;
               }
           }

           result = Mathf.Clamp(result, 1, 4);
           

           return result;
        }



    }
    [System.Serializable]
    public class BlockNumberRules
    {
        public int MaxNumber = 5;
        public float CoeficMovment;
        public AnimationCurve CurveToMaxNumber;
        [Range(0f,0.5f)]
        public float RandomRange = 0.2f;
        public int GetBlockNumber(int curentMovmentCount)
        {
            float curentCoefic = curentMovmentCount*CoeficMovment;
            curentCoefic = Mathf.Clamp(curentCoefic, 0, 1f+RandomRange/2f);
            curentCoefic += Random.Range(-RandomRange, RandomRange);
           
            curentCoefic=Random.Range(0, CurveToMaxNumber.Evaluate(curentCoefic));
            curentCoefic = Mathf.Clamp(curentCoefic, 0, 1f);
            return Mathf.RoundToInt( Mathf.Lerp(1f, MaxNumber,curentCoefic ));

        }
        

    }
}
