using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private LevelLocker levelLocker;
    [SerializeField] private bool[] puzzle;
    [SerializeField] private StarsLocker starsLocker;
    private void Start()
    {
        starsLocker.DeactivateStars();
        levelLocker.CheckWhichLevelsAreUnlocked();
    }

    //Passing level selected to next scene via holder class
    public void SelectPuzzleLevel()
    {
        //getting the current selected level
        int level = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        puzzle = levelLocker.GetPuzzleLevels();
        if (puzzle[level])
        {
            LevelHolder.currentLevel = level;
            SceneManager.LoadScene(1);
        }
    }
}
