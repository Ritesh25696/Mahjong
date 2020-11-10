using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupPuzzleGame : MonoBehaviour
{
    private List<Button> curLevelPuzzleButtons;
    private Sprite[] allGameImages;
    [SerializeField] private PuzzleGameManager puzzleGameManager;
    private List<Sprite> curLevelImages = new List<Sprite>();
    private int totalActiveCardsInLevel;

    private void Awake()
    {
        //Fetching all the sprites at given path
        allGameImages = Resources.LoadAll<Sprite>("TestAssets/TestAssets/TileSetFaces");
    }
    
    private int TotalSpritesInCurrentLevel()
    {
        //Total number of tiles/2 as 2 tiles of each type are there
        return curLevelPuzzleButtons.Count/2;
    }

    //Getting sprites which will be used in current level
    private void PrepareGameSprites()
    {
        curLevelImages.Clear();
        curLevelImages = new List<Sprite>();
        var totalSpitesInCurrentLevel = TotalSpritesInCurrentLevel();
        var spriteIdx = 0;
        for (int i = 0; i < totalSpitesInCurrentLevel; i++)
        {
            //Adding twice so that pair always exits
            curLevelImages.Add(allGameImages[spriteIdx]);
            curLevelImages.Add(allGameImages[spriteIdx]);
            spriteIdx++;
            spriteIdx = spriteIdx == allGameImages.Length ? 0 : spriteIdx;
        }
        Shuffle(curLevelImages);
        SetSpritesOnButtons();
        puzzleGameManager.SetUpSprites(curLevelImages);
    }

    private void SetSpritesOnButtons()
    {
        for (var i = 0; i < curLevelImages.Count; i++)
        {
            //curLevelPuzzleButtons[i].image.sprite = curLevelImages[i];
            curLevelPuzzleButtons[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = curLevelImages[i];
        }
    }
    
    //Every time the layout will be having random sprites, not predictable
    private void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void setLevelAndButtons(int level, List<Button> puzzleButtons)
    {
        curLevelPuzzleButtons = puzzleButtons;
        PrepareGameSprites();
    }
    
}
