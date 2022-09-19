using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void StartButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName); //change to the first scene
    }

    public void QuitButton()
    {
        Application.Quit(); //quits the app
    }
}
