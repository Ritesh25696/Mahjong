using System;

[Serializable]
public class GameData
{
    //These will be serialized and store in a file 
    private bool[] levelUnlocked;//Which levels are unlocked
    private int[] levelStars;//No of stars in each level
    private bool isGameStartedForFirstTime;//Will be used to initialize all variables 

    public bool[] GetLevelUnlocked()
    {
        return levelUnlocked;
    }
    public int[] GetLevelStars()
    {
        return levelStars;
    }

    public bool GetIsGameStartedForFirstTime()
    {
        return isGameStartedForFirstTime;
    }
    
    public void SetLevelUnlocked(bool[] levelUnlocked)
    {
         this.levelUnlocked = levelUnlocked;
    }
    public void SetLevelStars(int[] levelStars)
    {
         this.levelStars = levelStars;
    }

    public void SetIsGameStartedForFirstTime(bool isGameStartedForFirstTime)
    {
         this.isGameStartedForFirstTime = isGameStartedForFirstTime;
    }

}
