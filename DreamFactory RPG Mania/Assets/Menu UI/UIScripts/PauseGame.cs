using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseGame : MonoBehaviour
{
    private Player _playerInput;
    public GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.escapeKey.wasPressedThisFrame && GameManager.Instance.CanPause == true)
        {
            PauseStateSwitch(GameManager.Instance.isPaused);
        }
    }
    public void PauseStateSwitch( bool curr)
    {
        if (curr == false)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    public void OnPause(InputAction.CallbackContext ctx)
    {
       
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);

        GameManager.Instance.isPaused = true;
    }
    public  void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        GameManager.Instance.isPaused = false;
    }

    
}
