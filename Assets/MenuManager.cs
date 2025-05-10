using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No more scenes in Build Settings.");
        }
    }
}
