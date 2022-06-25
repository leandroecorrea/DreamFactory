using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{

    //Can be used on UI Buttons

   
    public void Pause()
    {
        Time.timeScale = 0;
        GameManager.Instance.isPaused = true;
    }
    public static void Resume()
    {
        Time.timeScale = 1;
        GameManager.Instance.isPaused = false;
    }
    public void Restart()
    {
        
        // reload level // or can do a respawn if there are check points
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Respawn();
        Resume();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Respawn()
    {

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadEnd()
    {
        SceneManager.LoadScene(2);
    }


}
