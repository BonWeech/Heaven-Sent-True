using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{ 
    public string levelName;

    public void SceneSwitch()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
