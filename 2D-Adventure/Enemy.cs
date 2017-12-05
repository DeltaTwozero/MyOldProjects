using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
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

    void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

	void Start ()
    {
//        startPos = this.transform.position;
//        goalPos = startPos + new Vector3(5, 0, 0);
        currentHP = maxHP;
        startPos = this.transform.position;
        Vector3 offset = new Vector3(MovePoint, 0, 0);
        goalPos = startPos + offset;
        //Debug.Log(speed);
        var startTime = Time.time;
        var journeyLength = Vector3.Distance(startPos, goalPos);
        StartCoroutine(Move(startPos, goalPos, journeyLength, startTime));        
    }

    private void OnDestinationReached(Vector3 startPosition, Vector3 destPosition, float distance, float startTime)
    {
        isLeft = !isLeft;
        this.GetComponent<SpriteRenderer>().flipX = isLeft;
        StartCoroutine(Move(startPosition, destPosition, distance, startTime));
    }

    private IEnumerator Move(Vector3 startPosition, Vector3 destposition, float distance, float startTime)
    {
        //float pingpong = Mathf.PingPong(Time.time * speed, 1f);     
        bool isActive = true;
        while(isActive)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / distance;
            var result = Vector3.Lerp(transform.position, destposition, fracJourney);
            transform.position = result;

            //if (MovePoint > 0)
            //{
            //    Debug.LogFormat("{0}, {1}, {2}, {3}, {4},", startPosition, destposition, result, MovePoint, Vector3.Distance(destposition, transform.position));
            //}

            yield return null;

            if (Vector3.Distance(destposition, transform.position) <= 0.1f)
            {
                startTime = Time.time;
                OnDestinationReached(destposition, startPosition, distance, startTime);
                isActive = false;
            }
        }        
    }
	
	void Update ()
    {
       float pingpong = Mathf.PingPong(Time.time * speed, 1f);

       /*if (transform.position == startPos)
       {
           isRight = false;
       }

        if (transform.position == goalPos)
        {
            isRight = true;
        }

        if (isRight == true)
       {
            //this.transform.position = Vector3.Lerp(transform.position, goalPos, speed);
            transform.position = Vector3.Lerp(startPos, goalPos, pingpong);
            this.GetComponent<SpriteRenderer>().flipX = false;
       }

       if (isRight == false)
       {
            //this.transform.position = Vector3.Lerp(transform.position, startPos, speed);
            transform.position = Vector3.Lerp(goalPos, startPos, pingpong);
            this.GetComponent<SpriteRenderer>().flipX = true;
       }*/

       if (Mathf.Abs(transform.position.x) > 0)
       {
           animator.SetBool("isWalk", true);
       }
       else
       {
           animator.SetBool("isWalk", false);
       }

       if (currentHP <= 0)
       {
           Destroy(this.gameObject);
       }

//        if (Mathf.Round(pingpong) == 0)
//        {
//            this.GetComponent<SpriteRenderer> ().flipX = false;
//        }
//        else if (Mathf.Round(pingpong) == 1f)
//        {
//            this.GetComponent<SpriteRenderer> ().flipX = true;
//        }
//
//        Debug.Log(pingpong + " " +(Mathf.Round(pingpong)));
	}

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
