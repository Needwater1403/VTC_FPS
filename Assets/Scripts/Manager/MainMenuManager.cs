using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
