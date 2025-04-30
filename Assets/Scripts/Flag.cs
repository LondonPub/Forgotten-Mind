using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      if(other.gameObject.CompareTag("Mirror"))
      {
        SceneManager.LoadScene("Level2");
      }
      if(other.gameObject.CompareTag("Win"))
      {
        SceneManager.LoadScene("Winscreen");
      }
   }
}
