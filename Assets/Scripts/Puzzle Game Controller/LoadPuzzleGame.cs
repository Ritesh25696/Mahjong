using UnityEngine;

public class LoadPuzzleGame : MonoBehaviour
{
    [SerializeField] private GameObject[] curPuzzleLevelPositions;
    [SerializeField] private LayoutPuzzleButtons layoutPuzzleButtons;
    [SerializeField] private CreatePuzzleButtons createPuzzleButtons;
    private int puzzleLevel;

    private void Start()
    {
        LoadPuzzle(LevelHolder.currentLevel);
    }
    
    public void LoadPuzzle(int level)
    {
        puzzleLevel = level;
        createPuzzleButtons.CreateButtonForCurrentLevel(); //Creating Buttons/Tiles for level       
        layoutPuzzleButtons.LayoutButtonInCurrentLevel(curPuzzleLevelPositions[puzzleLevel].transform); //Laying out the created tiles 
        LoadPuzzleGamePanel();
    }
    
    private void LoadPuzzleGamePanel()
    {
        curPuzzleLevelPositions[puzzleLevel].SetActive(true);
    }
}
