using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControleCenas : MonoBehaviour
{
   public float tempoTransição = 1f;
   public void Jogar ()
   {
      StartCoroutine(MenuFase1());
   }
   IEnumerator MenuFase1()
   {
      yield return new WaitForSeconds(tempoTransição);
      SceneManager.LoadScene("PrimeiraFase");
      Time.timeScale = 0f;
      StopCoroutine(MenuFase1());
   }
   public void TempoNormal()
   {
      Time.timeScale = 1f;
   }
   public void VoltarMenu ()
   {
    SceneManager.LoadScene("Menu");
   }
   public void Sair ()
   {
    Application.Quit();
   }
}
