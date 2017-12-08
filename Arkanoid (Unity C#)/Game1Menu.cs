using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game1Menu : MonoBehaviour
{
    //Simple GUI menu interface. Start game or quit.
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Main menu"))
        {
            SceneManager.LoadScene(0);
        }

        if (GUI.Button(new Rect(10, 60, 100, 50), "Exit Game"))
        {
            Application.Quit();
        }
    }
}
