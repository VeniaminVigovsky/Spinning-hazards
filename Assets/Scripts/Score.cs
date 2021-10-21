using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score 
{

    
    public static int hitCount = 0;

    static int score;

    public static int CalculateScore()
    {
        
        score = (25 - hitCount) * 4;
        if (score <= 0)
        {
            score = 0;
        }

        return score;
    }
}
