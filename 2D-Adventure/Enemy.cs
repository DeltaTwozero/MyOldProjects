using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    //Crating variables for enemy.
    Animator animator;

    Vector3 startPos;
    Vector3 goalPos;
    [SerializeField]
    float MovePoint;

    [SerializeField,Range(0, 1)]
    float speed = 1f;
    public bool isLeft;
    public int currentHP;
    public int maxHP;

    //I'm getting animator in order to use animator tool provided by Unity.
    void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    //Assigning all the variables in Start.
	void Start ()
    {
        currentHP = maxHP;
        startPos = this.transform.position;
        Vector3 offset = new Vector3(MovePoint, 0, 0);
        goalPos = startPos + offset;
        var startTime = Time.time;
        var journeyLength = Vector3.Distance(startPos, goalPos);
        StartCoroutine(Move(startPos, goalPos, journeyLength, startTime));        
    }

    //After reaching destination enemy turns around.
    private void OnDestinationReached(Vector3 startPosition, Vector3 destPosition, float distance, float startTime)
    {
        isLeft = !isLeft;
        this.GetComponent<SpriteRenderer>().flipX = isLeft;
        StartCoroutine(Move(startPosition, destPosition, distance, startTime));
    }

    //Movement logic.
    private IEnumerator Move(Vector3 startPosition, Vector3 destposition, float distance, float startTime)
    {     
        bool isActive = true;
        while(isActive)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / distance;
            var result = Vector3.Lerp(transform.position, destposition, fracJourney);
            transform.position = result;

            yield return null;

            if (Vector3.Distance(destposition, transform.position) <= 0.1f)
            {
                startTime = Time.time;
                OnDestinationReached(destposition, startPosition, distance, startTime);
                isActive = false;
            }
        }        
    }
	
    //Changing animations.
	void Update ()
    {
       float pingpong = Mathf.PingPong(Time.time * speed, 1f);

       if (Mathf.Abs(transform.position.x) > 0)
       {
           animator.SetBool("isWalk", true);
       }
       else
       {
           animator.SetBool("isWalk", false);
       }

       //Checking if dead.
       if (currentHP <= 0)
       {
           Destroy(this.gameObject);
       }
	}

    //Checking for collisions with different objects.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            TakeDamage(15);
            Destroy(GameObject.FindWithTag("Bullet"));
        }

        if (col.gameObject.tag == "Bomb")
        {
            TakeDamage(50);
            Destroy(this.gameObject);
            Destroy(GameObject.FindWithTag("Bomb"));
        }
    }

    //Taking damage and destroying an object that delivered damage.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            TakeDamage(15);
            Destroy(GameObject.FindWithTag("Bullet"));
        }
    }

    void TakeDamage(int dmg)
    {
        currentHP = currentHP - dmg;
    }
}
