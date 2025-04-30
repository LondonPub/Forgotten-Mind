using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorsNShi : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      if(other.gameObject.CompareTag("Mirror"))
      {
        SceneManager.LoadScene("Level 2");
      }
      if(other.gameObject.CompareTag("Mirror2"))
      {
        SceneManager.LoadScene("Level 3");
      }
      if(other.gameObject.CompareTag("Mirror3"))
      {
        SceneManager.LoadScene("Level 4");
      }
      if(other.gameObject.CompareTag("Win"))
      {
        SceneManager.LoadScene("Winscreen");
      }
      if(other.gameObject.CompareTag("Devroom"))
      {
        SceneManager.LoadScene("Devroom");
      }
      if(other.gameObject.CompareTag("Bingus"))
      {
        SceneManager.LoadScene("Bingusroom");
      }
   }
}
