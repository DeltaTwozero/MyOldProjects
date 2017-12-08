using UnityEngine;
using System.Collections;

public class BallControll : MonoBehaviour
{
    //creating variables for ball.
    [SerializeField]
    Sprite[] _sprites;

    [SerializeField]
    SpriteRenderer _renderer;

    bool _ballGO;
    Vector2 _ballPos;
    Vector2 _ballForce;
    Rigidbody2D rb;

    //Setting up variables.
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        _ballForce = new Vector2(200f, 300f);
        _ballGO = false;
        _ballPos = transform.position;

        if (_sprites == null || _sprites.Length == 0 || _renderer == null) return;
        _renderer.GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, 26)];
    }

    //Launching a ball.
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_ballGO == false)
            {
                transform.parent = null;
                _ballGO = true;
                rb.isKinematic = false;
                rb.AddForce(_ballForce);
            }
        }
	}
}
