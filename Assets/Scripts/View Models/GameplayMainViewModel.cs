using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayMainViewModel : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private PuzzleGameManager puzzleGameManager;
    private void Update()
    {
        UpdateScoreOnUI();
    }

    private void UpdateScoreOnUI()
    {
        scoreText.text = "Score: " + puzzleGameManager.GetScore();
    }
    
    public void BackToLevelSelectMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void PlayHintAnimations(Animator anim1, Animator anim2)
    {
        anim1.Play("Hint");
        anim2.Play("Hint");
        puzzleGameManager.setIdleTime(2f);
        StartCoroutine(PlayHintIdleAnimationAfterDelay(anim1, anim2));
    }
    
    private IEnumerator PlayHintIdleAnimationAfterDelay(Animator anim1, Animator anim2)
    {
        yield return new WaitForSeconds(0.8f);
        anim1.Play("Idle");
        anim2.Play("Idle");
    }
    
    public IEnumerator PlayHideAnimation(Animator anim1, Animator anim2)
    {
        yield return new WaitForSeconds(0.8f);
        anim1.Play("FadeOut");
        anim2.Play("FadeOut");
        yield return new WaitForSeconds(0.8f);
        puzzleGameManager.ResetGuesses();
    }
    
    public void PlayShakeAnimation(Animator anim)
    {
        anim.Play("ShakeTile");
    }

    public IEnumerator PlayIdleAnimationAfterDelay(Animator anim1, Animator anim2)
    {
        yield return new WaitForSeconds(0.8f);
        anim1.Play("Idle");
        anim2.Play("Idle");
        puzzleGameManager.ResetGuesses();
    }
    
}
