using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneIndex(int scnenIndex)
    {
        SceneManager.LoadScene(scnenIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
