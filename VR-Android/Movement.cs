using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public float repeatRate, hp, currentHP;
    [SerializeField]
    GameObject capsule;
    [SerializeField]
    Transform goal;
    [SerializeField]
    float timer,speed;
    Color RandomColor;

    Ray ray;
    RaycastHit hit;

    NavMeshAgent agent;

    public static Movement instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        agent = GetComponent<NavMeshAgent>();
    }

    void Start ()
    {
        hp = 100f;
        currentHP = hp;
        speed = 2;
        //Create random goal in seconds
        //Invoke("CreateGoal", 1f);
        //InvokeRepeating("CreateGoal", 1, repeatRate);
    }

    void Update()
    {
        agent.destination = goal.transform.position;
        Transform cam = Camera.main.transform;
        ray = new Ray(cam.position, cam.rotation * Vector3.forward);
        RandomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);

        Raycast();

        //transform.position += cam.forward * 0.7f * Time.deltaTime;

        Debug.DrawLine(cam.position, cam.rotation * Vector3.forward * 100f, Color.red);
    }

    void Raycast()
    {
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                if (Enemy.instance != null)
                    Enemy.instance.partSys.enableEmission = true;
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    hit.transform.gameObject.GetComponent<Renderer>().material.color = RandomColor;
                    timer = 1f;
                }
            }

            if (hit.collider.gameObject.tag != "Enemy")
            {
                if (Enemy.instance != null)
                    Enemy.instance.partSys.enableEmission = false;
                timer = 1;
            }

            #region Ray Movement
            //if (hit.collider.gameObject.tag == "Ground")
            //{
            //    goal.transform.position = hit.point;
            //} 
            #endregion
        }
    }

    void CreateGoal()
    {
        goal.transform.position = new Vector3(Random.Range(-9, 9), 1, Random.Range(-9, 9));
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Goal")
        {
            CreateGoal();
        }
    }
}