using UnityEngine;

public class LevelLocker : MonoBehaviour
{
    [SerializeField] private PuzzleGameSaver puzzleGameSaver;
    [SerializeField] private GameObject[] levelStarsHolder, levelPadlocks;
    [SerializeField] private StarsLocker starsLocker;
    private bool[] gameLevels;

    private void Awake()
    {
        DeactivatePadlockAndStarsHolder();
    }

    private void Start()
    {
        GetLevels();
    }

    //Setting unlocked level and active with stars awarded in previous levels
    public void CheckWhichLevelsAreUnlocked()
    {
        DeactivatePadlockAndStarsHolder();
        GetLevels();

        for (int i = 0; i < gameLevels.Length; i++)
        {
            if (gameLevels[i])
            {
                levelStarsHolder[i].SetActive(true);
                starsLocker.ActivateStars(i);
            }
            else
            {
                levelPadlocks[i].SetActive(true);
            }
        }
    }
    
    private void DeactivatePadlockAndStarsHolder()
    {
        for (int i = 0; i < levelStarsHolder.Length; i++)
        {
            levelPadlocks[i].SetActive(false);
            levelStarsHolder[i].SetActive(false);
        }
    }

    private void GetLevels()
    {
        gameLevels = puzzleGameSaver.levelUnlocked;
    }

    public bool[] GetPuzzleLevels()
    {
        return gameLevels;
    }
}
