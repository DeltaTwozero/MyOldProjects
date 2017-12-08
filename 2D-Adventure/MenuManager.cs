using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject menuBG;

    //Deactivating game pause menu.
    void Start()
    {
        menuBG.SetActive(false);
    }

    //Checking for button. If pressed, then pause the game and activate menu.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            menuBG.SetActive(true);
        }
    }

    //Closes application.
    public void ExitGame()
    {
        Application.Quit();
    }

    //Resumes the game at the point it was paused.
    public void ContinueGame()
    {
        Time.timeScale = 1;
        menuBG.SetActive(false);
    }

    //Restarts current level.
    public void RestartGame()
    {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
    }
}
