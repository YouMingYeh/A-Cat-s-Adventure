using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public GameObject gameMenu;
    private void Start()
    {
        gameMenu.SetActive(true);
    }
    public void gameStart()
    {
        
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
        }

    }

}
