using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
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

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        dmg = Random.Range(5, 20);
        isMove = true;
    }
	
	void Update ()
    {
        if(isMove)
        agent.destination = Movement.instance.transform.position;
	}

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Movement.instance.currentHP -= dmg;
            isMove = false;
            StartCoroutine("StartMove");
        }
    }

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(2f);
        isMove = true;
    }
}
