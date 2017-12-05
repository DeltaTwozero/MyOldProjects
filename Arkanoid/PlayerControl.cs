using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public static int _playerHP;

    float _playerSpeed = 1f;
    public float _bounds;

    float _playerX;
    float _playerY;

    void Start()
    {
        _playerHP = 3;
        _playerX = this.transform.position.x;
        _playerY = this.transform.position.y;
    }

	void Update ()
    {
            Vector2 _mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            this.transform.position = new Vector2(_mousePos.x, _playerY);

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

    void OnGUI()
    {
           GUI.Label(new Rect(10, 10, 100, 50), "Lives: " + _playerHP);
    }
}
