using UnityEngine;
using System.Collections;

public class ArrowSpawner : MonoBehaviour
{
    //Creating variables. I've made some variables serialized in order to change certain settings in Unity (for testing purposes).
    [SerializeField]
    GameObject arrow;
    [SerializeField]
    Transform spawn;
    public float speed;
    public float rate;
    [SerializeField]
    int maxHP;
    [SerializeField]
    int currentHP;

    void Start()
    {
        //I'm assigning current hp to max hp. I made this in order to save time in future if I decide to create healing for turret. Starting infinite "fire" cycle.
        currentHP = maxHP;
        InvokeRepeating("CreateArrow", 0, rate);
    }

    //Method for shooting.
    void CreateArrow()
    {
        GameObject temp = Instantiate(arrow, this.transform.position, Quaternion.identity) as GameObject;
        temp.transform.position = spawn.transform.position;
        temp.AddComponent<Move>();
        temp.GetComponent<Move>().speed = speed;
    }

    //Method for calculating damage and changing current hp.
    void TakeDamage(int dmg)
    {
        currentHP = currentHP -= dmg;
        CheckGameOver();
    }

    //Method for destroying an object.
    void CheckGameOver()
    {
        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //Method for taking damage from a specific source.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            TakeDamage(15);
        }
    }
}

//In this section of the code I'm making my projectile move in the precalculated direction.
public class Move : MonoBehaviour
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

    //Method for clearing projectile in order to lower CPU usage.
    void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    //Method checks for collision with objects.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Player")
        {
            this.speed = 0;
        }

        if (col.gameObject.tag == "Player")
            DestroyMe();

        if (col.gameObject.tag == "Bullet")
        {
            DestroyMe();
        }
    }
}