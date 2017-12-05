using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Start Game"))
        {
            SceneManager.LoadScene(1);
        }

        if (GUI.Button(new Rect(10, 60, 100, 50), "Exit Game"))          
        {
            Application.Quit();
        }
    }
}
