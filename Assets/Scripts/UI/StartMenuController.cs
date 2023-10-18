using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void gameStart()
    {
        StaticClass.IsRestart = false;
        StaticClass.IsStarted = true;
        SceneManager.LoadSceneAsync("MainScene");
    }

}
