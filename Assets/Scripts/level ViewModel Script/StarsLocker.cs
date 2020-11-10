using System;
using UnityEngine;

public class StarsLocker : MonoBehaviour
{
    [SerializeField] private PuzzleGameSaver puzzleGameSaver;
    [SerializeField] private GameObject[] level1Stars, level2Stars, level3Stars, level4Stars, level5Stars;

    [NonSerialized] public int[] levelStars;

    public void ActivateStars(int level)
    {
        GetStars();
        int stars;
        stars = levelStars[level];
        ActivateLevelStars(level, stars);
        
    }

    /*
     * Setting stars for every level
     * Each level is having 5 stars
     * Enabling only number of stars earned by user
     */
    void ActivateLevelStars(int level, int stars)
    {
        switch (level)
        {
            case 0:
                if (stars != 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level1Stars[i].SetActive(true);
                    }
                } 
                break;
            case 1:
                if (stars != 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level2Stars[i].SetActive(true);
                    }
                } 
                break;
            case 2:
                if (stars != 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level3Stars[i].SetActive(true);
                    }
                } 
                break;
            case 3:
                if (stars != 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level4Stars[i].SetActive(true);
                    }
                } 
                break;
            case 4:
                if (stars != 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level5Stars[i].SetActive(true);
                    }
                } 
                break;
        }
    }

    public void DeactivateStars()
    {
        for (int i = 0; i < level1Stars.Length; i++)
        {
            level1Stars[i].SetActive(false);
            level2Stars[i].SetActive(false);
            level3Stars[i].SetActive(false);
            level4Stars[i].SetActive(false);
            level5Stars[i].SetActive(false);
        }
    }

    private void GetStars()
    {
        levelStars = puzzleGameSaver.levelStars;
    }
}
