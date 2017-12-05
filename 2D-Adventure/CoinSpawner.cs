using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
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
        GameObject temp = Instantiate(coin, /*this.transform.position*/ new Vector3 (UnityEngine.Random.Range (0f, 5f), UnityEngine.Random.Range (0f, 5f)), Quaternion.identity) as GameObject;
		temp.transform.position = spawn.transform.position;
		temp.AddComponent<MoveCoin>();
		temp.GetComponent<MoveCoin>().speed = speed;
	}
}

public class MoveCoin : MonoBehaviour
{
	Rigidbody2D rb;
	public float speed;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		Invoke("DestroyMe", 5f);
	}

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
		if (col.gameObject.tag == "Ground")
		{
			this.speed = 0;
		}

		if (col.gameObject.tag == "Player")
			DestroyMe();
	}
}
