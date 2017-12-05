using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    [SerializeField]
    Transform coin;
    [SerializeField]
    Transform player;

    Rigidbody2D rb;

    public bool isMagnet;
    float speed;
    float distance;

    void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
    }

	void Start ()
    {
        isMagnet = false;
	}

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R)/*Hero.instance.*/)
        {
            isMagnet = true;
            this.rb.isKinematic = false;
        }

        distance = Vector3.Distance(coin.position, player.position);
        if (distance <= 5 && isMagnet)
        {
            transform.position = Vector3.MoveTowards(coin.position, player.position, speed * Time.deltaTime);
        }
        else
            this.rb.isKinematic = true;
	}
}
