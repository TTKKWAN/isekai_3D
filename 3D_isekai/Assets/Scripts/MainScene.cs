using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public void Mainmenu()
    {
        SceneManager.LoadScene("1. MainMenu"); 
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("3. Scene1"); 
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("2. HowToPlay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
