public static class PlayerStatsController
{
    private static int highScore, highestMultiplier, lastScore;

    public static int LastScore {
        get { return lastScore; }
        set { lastScore = value;}
    }
    
    public static int HighScore {
        get { return highScore; }
        set {
            if (value > highScore)
                highScore = value;
        }
    }

    public static int HigestMultiplier {
        get { return highestMultiplier; }
        set { 
            if (value > highestMultiplier)
                highestMultiplier = value;
        }
    }
}
