using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BrickControll : MonoBehaviour
{
    Color _color;
    SpriteRenderer _renderer;

    int _brickHP;

    //Assigning random hp to brick.
	void Start ()
    {
        _brickHP = Random.Range(1,4);
        _renderer = GetComponent<SpriteRenderer>();

        //Setting brick color according to hp.
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

    //Taking damage
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            _brickHP--;
        }
    }

    //checking if dead.
    void OnCollisionExit2D(Collision2D collision)
    {
        if (_brickHP == 0) Destroy(this.gameObject);
    }
}
