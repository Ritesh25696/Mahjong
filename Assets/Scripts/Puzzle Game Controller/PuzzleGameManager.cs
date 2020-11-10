using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour
{
    [SerializeField] private GameplayMainViewModel gameplayMainViewModel;
    private bool firstGuess, secondGuess;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;
    private GameObject firstGuessPuzzlePiece, secondGuessPuzzlePiece;
    private Animator anim1, anim2;
    
    private int countCorrectGuess, guessToWinLevel, score;
    private float timeToGiveHint = 5f;
    private float idleTime = 0f;
    
    List<Button> curLevelPuzzleButtons = new List<Button>();
    List<Sprite> curLevelSprites = new List<Sprite>();

    //Can be placed inside the level file (scriptable object)
    [SerializeField] private int threeStarScore = 150;
    [SerializeField] private int twoStarScore = 100;

    [SerializeField] private PuzzleGameSaver puzzleGameSaver;
    [SerializeField] private GameFinishedViewModel gameFinishedViewModel;

    private void Start()
    {
        puzzleGameSaver = (PuzzleGameSaver)FindObjectOfType(typeof(PuzzleGameSaver));
    }

    private void Update()
    {
        //Hints to player after 5 sec of inactivity
        idleTime += Time.deltaTime;
        if (idleTime > timeToGiveHint)
        {
            GiveHintsToPlayer();
        }
    }

    public void setIdleTime(float idleTime)
    {
        this.idleTime = idleTime;
    }

    public int GetScore()
    {
        return score;
    }

    //Setting up listeners on tiles
    public void setUpButtons(List<Button> levelButtons)
    {
        curLevelPuzzleButtons = levelButtons;
        guessToWinLevel = levelButtons.Count / 2;
        AddListeners();
    }
    
    public void SetUpSprites(List<Sprite> levelSprites)
    {
        curLevelSprites = levelSprites;
    }
    
    //Adding on click listener to game tiles 
    private void AddListeners()
    {
        foreach (var btn in curLevelPuzzleButtons)
        {
            btn.onClick.RemoveListener(() => { });
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }
    
    //Called on click of tiles 
    private void PickAPuzzle()
    {
        idleTime = 0;
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessPuzzlePiece = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            firstGuessIndex = int.Parse(firstGuessPuzzlePiece.name);
            firstGuessPuzzle = curLevelSprites[firstGuessIndex].name;
            GetPuzzlePieceAnimator(firstGuessIndex, out anim1);
            gameplayMainViewModel.PlayShakeAnimation(anim1);
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessPuzzlePiece = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            secondGuessIndex = int.Parse(secondGuessPuzzlePiece.name);
            secondGuessPuzzle = curLevelSprites[secondGuessIndex].name;
            if(!FirstAndSecondGuessAreSame())
            {
                GetPuzzlePieceAnimator(secondGuessIndex, out anim2);
                gameplayMainViewModel.PlayShakeAnimation(anim2);
                CheckIfPuzzleMatch();
            }
        }
    }

    //If first and second clicks are same unselecting the piece
    private bool FirstAndSecondGuessAreSame()
    {
        if (firstGuessPuzzlePiece.transform == secondGuessPuzzlePiece.transform)
        {
            firstGuess = false;
            secondGuess = false;
            anim1.Play("Idle");
            return true;
        }

        return false;
    }
    
    private void GetPuzzlePieceAnimator(int pieceIdx, out Animator anim)
    {
        anim = curLevelPuzzleButtons[pieceIdx].GetComponent<Animator>();
    }

    public void ResetGuesses()
    {
        firstGuess = false;
        secondGuess = false;
    }

    private void CheckIfPuzzleMatch()
    {
        if (firstGuessPuzzle == secondGuessPuzzle)//Matched
        {
            StartCoroutine(gameplayMainViewModel.PlayHideAnimation(anim1, anim2));
            CheckIfGameIsFinished();
            score += 15;
        }
        else//Not Matched
        {
            score -= 10;
            score = score < 0 ? 0 : score;
            StartCoroutine(gameplayMainViewModel.PlayIdleAnimationAfterDelay(anim1, anim2));
        }
    }

    private void CheckIfGameIsFinished()
    {
        countCorrectGuess++;
        if (countCorrectGuess == guessToWinLevel)
        {
            CheckHowManyStars();
        }
    }

    //When level is completed checking how many stars to be awarded
    private void CheckHowManyStars()
    {
        var starsToBeAwarded = 1;
        if (score >= threeStarScore)
        {
            starsToBeAwarded = 3;
        }
        else if (score >= twoStarScore)
        {
            starsToBeAwarded = 2;
        }
        gameFinishedViewModel.ShowGameFinishedPanel(starsToBeAwarded);
        puzzleGameSaver.Save(LevelHolder.currentLevel, starsToBeAwarded);
    }
    
    //Give Hints to User after 5 sec of inactivity
    private void GiveHintsToPlayer()
    {
        var firstHindButtonIdx = -1;
        var secondHintButtonIdx = -1;
        for (int i = 0; i < curLevelPuzzleButtons.Count; i++)
        {
            if (curLevelPuzzleButtons[i].interactable)
            {
                firstHindButtonIdx = i;
                break;
            }
        }

        for (int i = 0; i < curLevelPuzzleButtons.Count; i++)
        {
            if (curLevelPuzzleButtons[i].interactable && curLevelSprites[firstHindButtonIdx].name == curLevelSprites[i].name && i != firstHindButtonIdx)
            {
                secondHintButtonIdx = i;
                break;
            }
        }

        if (firstHindButtonIdx >= 0 && secondHintButtonIdx >= 0)
        {
            Animator hintAnim1, hintAnim2;
            GetPuzzlePieceAnimator(firstHindButtonIdx, out hintAnim1);
            GetPuzzlePieceAnimator(secondHintButtonIdx, out hintAnim2);
            gameplayMainViewModel.PlayHintAnimations(hintAnim1, hintAnim2);
        }
    }
}
