using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashWarningLoader : MonoBehaviour
{
    public void LoadCrashWarning()
    {
        SceneManager.LoadScene("Crash Warning");
    }
}