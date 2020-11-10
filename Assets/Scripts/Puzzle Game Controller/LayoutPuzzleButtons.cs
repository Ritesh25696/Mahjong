using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutPuzzleButtons : MonoBehaviour
{
    [SerializeField] private Button emptyPuzzleButton;
    [SerializeField] private SetupPuzzleGame setupPuzzleGame;
    [SerializeField] private PuzzleGameManager puzzleGameManager;
    private int curPuzzleLevel;
    private Transform curLevelPosition;
    [NonSerialized] public List<Button> curLevelButtons;
    [NonSerialized] public LevelData curLevelData;
    
    public void LayoutButtonInCurrentLevel(Transform position)
    {
        curLevelPosition = position;
        var totalButtonCountInLevel = curLevelData.row * curLevelData.col;
        LayoutButtons(totalButtonCountInLevel , curLevelData.levelLayout);
        setupPuzzleGame.setLevelAndButtons(curPuzzleLevel, curLevelButtons);
        puzzleGameManager.setUpButtons(curLevelButtons);
    }
    
    /*
     * Spawning game tiles
     * params layout : level structure in string format.
     */
    private void LayoutButtons(int totalButtonCountInLevel, string layout)
    {
        var curButtonIndex = 0;
        for (int i = 0; i < totalButtonCountInLevel; i++)
        {
            if (layout[i] == 'X')
            {
                SpawnGameButton(curButtonIndex);
                curButtonIndex++;
            }
            else
            {
                SpawnEmptyGameButton();
            }
        }
    }
    
    private void SpawnGameButton(int curButtonIndex)
    {
            curLevelButtons[curButtonIndex].transform.SetParent(curLevelPosition, false);
    }

    //Empty tile will be spawned at places with '0' in level layout
    private void SpawnEmptyGameButton()
    {
        Instantiate(emptyPuzzleButton, curLevelPosition);
    }
}
