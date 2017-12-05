using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BrickControll : MonoBehaviour
{
    Color _color;
    SpriteRenderer _renderer;

    int _brickHP;
    //int _brickCount;

	void Start ()
    {
        _brickHP = Random.Range(1,4);
        //_brickCount = 20;
        _renderer = GetComponent<SpriteRenderer>();

        
        if (_brickHP == 1)
        {
            _renderer.color = Color.green;
        }

        if (_brickHP == 2)
        {
            _renderer.color = Color.yellow;
        }

        if (_brickHP == 3)
        {
            _renderer.color = Color.red;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            _brickHP--;
            //_brickCount--;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (_brickHP == 0) Destroy(this.gameObject);

        //if (_brickCount == 0)
        //{
        //    SceneManager.LoadScene(0);
        //}
    }
}
