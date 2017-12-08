using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    //Craeting variables for player.
    public static int _playerHP;

    float _playerSpeed = 1f;
    public float _bounds;

    float _playerX;
    float _playerY;

    //Setting up player hp and position.
    void Start()
    {
        _playerHP = 3;
        _playerX = this.transform.position.x;
        _playerY = this.transform.position.y;
    }

    //Player follows mouse, but only in x dimension.
	void Update ()
    {
            Vector2 _mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            this.transform.position = new Vector2(_mousePos.x, _playerY);

        //Check to prevent player going out of game bounds.
        if (this.transform.position.x < -_bounds)
        {
            transform.position = new Vector2(-_bounds, _playerY);
        }
        if (this.transform.position.x > _bounds)
        {
            transform.position = new Vector2(_bounds, _playerY);
        }

    }

    public void PlayerHPcount()
    {
        _playerHP--;
    }

    //displaying player hp.
    void OnGUI()
    {
           GUI.Label(new Rect(10, 10, 100, 50), "Lives: " + _playerHP);
    }
}
