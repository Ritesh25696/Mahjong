using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePuzzleButtons : MonoBehaviour
{
    /*
     * Responsible for creating the game tiles
     * These tiles will be spawned in specific layout
     */
    [SerializeField] private LevelData[] LevelDatas;
    [SerializeField] private Button puzzleButton;
    [SerializeField] private LayoutPuzzleButtons layoutPuzzleButtons;
   
    private LevelData curLevelData;
    private List<Button> levelButtons = new List<Button>();

    public void CreateButtonForCurrentLevel()
    {
        curLevelData = LevelDatas[LevelHolder.currentLevel];
        CreateButtons();
        AssignButtonsToLayout();
    }

    //Creating buttons/Tiles to be spawned
    private void CreateButtons()
    {
        var totalCards = TotalCardsToSpawnInLevel();
        for (var i = 0; i < totalCards; i++)
        {
            Button btn = Instantiate(puzzleButton);
            btn.gameObject.name = "" + i;
            levelButtons.Add(btn);
        }
    }
    
    //Number of cards to be spawned in current level 
    private int TotalCardsToSpawnInLevel()
    {
        var totalCards = curLevelData.row * curLevelData.col;
        var totalCardsInLevel = 0;
        for (var i = 0; i < totalCards; i++)
        {
            if (curLevelData.levelLayout[i] == 'X')
            {
                totalCardsInLevel++;
            }
        }
        return totalCardsInLevel;
    }

    //Passing the instantiated button to layout class
    void AssignButtonsToLayout()
    {
        layoutPuzzleButtons.curLevelButtons = levelButtons;
        layoutPuzzleButtons.curLevelData = curLevelData;
    }
    
}
