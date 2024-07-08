using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTransformationHelper 
{
    public static string GetMoneyText(float Amount)
    {
        string Result="";
        string Leter = "";
        int Number = 1;
        if (Amount >=1000000000)
        {
            Leter = "B";
            Number = 1000000000;
            
        }
        else
        {
            if (Amount >= 1000000)
            {
                Leter = "M";
                Number = 1000000;
            }
            else
            {
                if (Amount > 1000)
                {
                    Leter = "K";
                    Number = 1000;
                }
            }
            
        }

        if (Amount > 1000)
        {
            int devider1 = Number / 10;
            int devider2 = Number / 100;
            int rest1 = 0;
            if (devider1 != 0)
            {
                rest1 =(int) ((int) Amount % Number)/ (devider1);
            }

            int rest2 = 0;
            if (devider2 != 0)
            {
                rest2 =(int) ((int) Amount % Number)/(devider2)%10;
            }

         
            if (rest1 > 0||rest2!=0)
            {
                Result = Mathf.FloorToInt(Amount/Number).ToString()+"."+rest1;
                if (rest2 != 0)
                {
                    Result += rest2;
                }

                Result += Leter;
            }
            else
            {
                Result = Mathf.FloorToInt(Amount/Number).ToString();
                Result += Leter;
            }
        }
        else
        {
            if (Mathf.FloorToInt(Amount )== 1000)
            {
                Result = "1K";
            }
            else
            {
                Result = Mathf.RoundToInt(Amount).ToString();
            }
           
        }
        
        
        
        return Result;
    }
    
}
