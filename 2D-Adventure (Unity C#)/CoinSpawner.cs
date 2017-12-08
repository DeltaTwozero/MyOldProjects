using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    //Creating variables for coin spawn.
	[SerializeField]
	GameObject coin;
	[SerializeField]
	Transform spawn;
	public float speed;
	public float rate;

	void Start ()
	{
		InvokeRepeating("CreateCoin", 0, rate);
	}
		
	void CreateCoin()
	{
        GameObject temp = Instantiate(coin, new Vector3 (UnityEngine.Random.Range (0f, 5f), UnityEngine.Random.Range (0f, 5f)), Quaternion.identity) as GameObject;
		temp.transform.position = spawn.transform.position;
		temp.AddComponent<MoveCoin>();
		temp.GetComponent<MoveCoin>().speed = speed;
	}
}

//In this section I'm "ejecting" coins from chest.
public class MoveCoin : MonoBehaviour
{
	Rigidbody2D rb;
	public float speed;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		Invoke("DestroyMe", 5f);
	}

    //FixedUpdate provided smoother "ejecting".
	void FixedUpdate()
	{
        rb.velocity = new Vector2(-speed, rb.velocity.y);
	}

	void DestroyMe()
	{
		Destroy(this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
        //Coin stop on colission with ground.
		if (col.gameObject.tag == "Ground")
		{
			this.speed = 0;
		}

        //Coin is destroyed (picked up) on colission with player.
		if (col.gameObject.tag == "Player")
			DestroyMe();
	}
}
