using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    //This is for zone below player.
    public GameObject _player;
    public GameObject _ball;
    public GameObject _GameOver;
    public GameObject _bricks;

    //Taking damage.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
        PlayerControl._playerHP--;
        }
    }

    //Checking if dead.
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            if (PlayerControl._playerHP == 0)
            {
                Destroy(_player);
                Destroy(_ball);
                print("You lose");
                SceneManager.LoadScene(0);
            }
        }
    }
}
