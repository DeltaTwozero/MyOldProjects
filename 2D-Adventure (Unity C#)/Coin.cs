using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    //Creating variables for coin.
    [SerializeField]
    Transform coin;
    [SerializeField]
    Transform player;

    Rigidbody2D rb;

    //isMagnet is a Power Up that player can activate.
    public bool isMagnet;
    float speed;
    float distance;

    //Even though I've made prefab for coin I still prefer getting component for object. Just to be safe.
    void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    //A safe switch or if a player teleports to another level Power Up should not affect new coins.
	void Start ()
    {
        isMagnet = false;
	}

	void Update ()
    {
        //activating Power Up.
        if (Input.GetKeyDown(KeyCode.R))
        {
            isMagnet = true;
            this.rb.isKinematic = false;
        }

        //Coin moves towards player.
        distance = Vector3.Distance(coin.position, player.position);
        if (distance <= 5 && isMagnet)
        {
            transform.position = Vector3.MoveTowards(coin.position, player.position, speed * Time.deltaTime);
        }
        else
            this.rb.isKinematic = true;
	}
}
