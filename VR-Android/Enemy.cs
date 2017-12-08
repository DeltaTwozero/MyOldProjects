using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //Creating variables for enemy.
    NavMeshAgent agent;
    float dmg;
    bool isMove;
    public ParticleSystem partSys;

    public static Enemy instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //Getting component for enemy so he can use Unity tool NavMeshAgent; Givin him random damage power; Making him move immidiately.
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        dmg = Random.Range(5, 20);
        isMove = true;
    }
	
    //Enemy moves towards the player.
	void Update ()
    {
        if(isMove)
        agent.destination = Movement.instance.transform.position;
	}

    private void OnCollisionEnter(Collision col)
    {
        //Stopping the enemy. Giving player time to move further away from enemy (enemy movement looping structure).
        if (col.gameObject.tag == "Player")
        {
            Movement.instance.currentHP -= dmg;
            isMove = false;
            StartCoroutine("StartMove");
        }
    }

    //Making enemy move again.
    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(2f);
        isMove = true;
    }
}
