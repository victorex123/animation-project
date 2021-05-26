using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToAnotherScene : MonoBehaviour
{
    public string sceneName;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByName(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }

    public void CloseTheGame()
    {
        Application.Quit();
    }

    public void OpenWindow(Canvas window)
    {
        window.gameObject.SetActive(true);
    }
    public void CloseWindow(Canvas window)
    {
        window.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }
    
        
    
}