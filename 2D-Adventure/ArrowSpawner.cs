using UnityEngine;
using System.Collections;

public class ArrowSpawner : MonoBehaviour
{
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
        currentHP = maxHP;
        InvokeRepeating("CreateArrow", 0, rate);
    }

    void CreateArrow()
    {
        GameObject temp = Instantiate(arrow, this.transform.position, Quaternion.identity) as GameObject;
        temp.transform.position = spawn.transform.position;
        temp.AddComponent<Move>();
        temp.GetComponent<Move>().speed = speed;
    }

    void TakeDamage(int dmg)
    {
        currentHP = currentHP -= dmg;
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            TakeDamage(15);
        }
    }
}

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

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }

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