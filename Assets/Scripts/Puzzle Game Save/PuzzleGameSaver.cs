using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PuzzleGameSaver : MonoBehaviour
{
    private static PuzzleGameSaver instance;
    private GameData gameData;
    [SerializeField] private int numberOfLevels = 5;
    public bool[] levelUnlocked;
    public int[] levelStars;
    private bool isGameStartedForFirstTime;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        
        InitializeGame();
    }

    void InitializeGame()
    {
        LoadGameData();
        if (gameData != null)
        {
            isGameStartedForFirstTime = gameData.GetIsGameStartedForFirstTime();
        }
        else
        {
            isGameStartedForFirstTime = true;
        }

        if (isGameStartedForFirstTime)
        {
            //Initializing all the variables
            isGameStartedForFirstTime = false;
            levelUnlocked = new bool[numberOfLevels];
            levelStars = new int[numberOfLevels];
            levelUnlocked[0] = true;
            for (int i = 1; i < numberOfLevels; i++)
            {
                levelStars[i] = 0;
                levelUnlocked[i] = false;
            }
            gameData = new GameData();
            gameData.SetLevelStars(levelStars);
            gameData.SetLevelUnlocked(levelUnlocked);
            gameData.SetIsGameStartedForFirstTime(isGameStartedForFirstTime);
            
            SaveGameData();
            LoadGameData();
        }
    }
    
    //Serializing the gamedata and storing it in a file 
    private void SaveGameData()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameData.dat");
            if (gameData != null)
            {
                gameData.SetLevelUnlocked(levelUnlocked);
                gameData.SetLevelStars(levelStars);
                gameData.SetIsGameStartedForFirstTime(isGameStartedForFirstTime);

                bf.Serialize(file, gameData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    //Deserializing the file and saving it back to gamedata
    private void LoadGameData()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            gameData = (GameData) bf.Deserialize(file);
            if (gameData != null)
            {
                levelUnlocked = gameData.GetLevelUnlocked();
                levelStars = gameData.GetLevelStars();
                
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    //Save the user data in file
    public void Save(int level, int stars)
    {
        //In case user is playing level again only storing the highest star count 
        if(levelStars[level] < stars)
        {
            var unlockedNextLevel = -1;
            unlockedNextLevel = level + 1;
            levelStars[level] = stars;
            if (unlockedNextLevel < levelUnlocked.Length)
            {
                levelUnlocked[unlockedNextLevel] = true;
            }
            SaveGameData();
        }
    }

}
