using UnityEngine;
using System.Collections;

public class EnemyEagle : MonoBehaviour
{
    Animator animator;
    [SerializeField, Range(0,100)]
    float speed;
    [SerializeField]
    int currentHP;
    [SerializeField]
    int maxHP;
    [SerializeField]
    Transform eagle;
    [SerializeField]
    Transform player;

    bool isMove;
    float distance;

	void Start ()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        animator.SetBool("isIdle", true);
	}

	void Update ()
    {
        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.x >= player.position.x)
        {
            this.GetComponent<SpriteRenderer> ().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer> ().flipX = false;
        }

        distance = Vector3.Distance(player.position, eagle.position);
        if (distance <= 10)
        {
            animator.SetBool("isIdle", false);
            transform.position = Vector3.MoveTowards(eagle.position, player.position, speed * Time.deltaTime);
        }
        else
            animator.SetBool("isIdle", true);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bomb")
        {
            TakeDamage(50);
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Bullet")
        {
            TakeDamage(15);
            Destroy(col.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKeyDown(KeyCode.V) && col.gameObject.tag == "Player")
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int dmg)
    {
        currentHP = currentHP - dmg;
    }
}
