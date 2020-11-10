using System.Collections;
using UnityEngine;

public class GameFinishedViewModel : MonoBehaviour
{
   [SerializeField] private GameObject gameFinishPanel;
   [SerializeField] private Animator star1Anim, star2Anim, star3Anim, gameFinishPanelAnim;

   public void ShowGameFinishedPanel(int stars)
   {
      StartCoroutine(showPanel(stars));
   }
   
   IEnumerator showPanel(int stars)
   {
      gameFinishPanel.SetActive(true);
      gameFinishPanelAnim.Play("FadeIn");
      yield return new WaitForSeconds(1f);

      switch (stars)
      {
         case 1:
            star1Anim.Play("FadeIn");
            break;
         case 2:
            star1Anim.Play("FadeIn");
            yield return new WaitForSeconds(.1f);
            star2Anim.Play("FadeIn");
            break;
         case 3:
            star1Anim.Play("FadeIn");
            yield return new WaitForSeconds(.1f);
            star2Anim.Play("FadeIn");
            yield return new WaitForSeconds(.1f);
            star3Anim.Play("FadeIn");
            break;
      }
   }

   private IEnumerator HidePanel()
   {
      gameFinishPanelAnim.Play("FadeOut");
      star3Anim.Play("FadeOut");
      star2Anim.Play("FadeOut");
      star1Anim.Play("FadeOut");
      yield return new WaitForSeconds(1f);
      
      gameFinishPanel.SetActive(false);
   }

}
