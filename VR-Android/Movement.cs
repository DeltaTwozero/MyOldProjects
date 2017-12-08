using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    //Creating variables for player.
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

    //Creating instance of this code in order to access it's components from another code.
    public static Movement instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //Getting NavMeshAgent in order to use Unity tool.
        agent = GetComponent<NavMeshAgent>();
    }

    void Start ()
    {
        hp = 100f;
        currentHP = hp;
        speed = 2;
    }

    //Player moves automatically towards invisible goal.
    void Update()
    {
        agent.destination = goal.transform.position;
        Transform cam = Camera.main.transform;
        ray = new Ray(cam.position, cam.rotation * Vector3.forward);
        RandomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1.0f);

        Raycast();

        Debug.DrawLine(cam.position, cam.rotation * Vector3.forward * 100f, Color.red);
    }

    //Player uses his head as a "weapon" in order to hit the enemy.
    void Raycast()
    {
        if (Physics.Raycast(ray, out hit))
        {
            //If player looks at enemy long enough enemy changes color. It's visually represented by activating particle system (red dots flying).
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

            //If player looks at something else but enemy, then nothing happens. If player was looking at the enemy and turns away, then particle system deactivates.
            if (hit.collider.gameObject.tag != "Enemy")
            {
                if (Enemy.instance != null)
                    Enemy.instance.partSys.enableEmission = false;
                timer = 1;
            }

            //Initially player was moving towards ray end point.
            #region Ray Movement
            //if (hit.collider.gameObject.tag == "Ground")
            //{
            //    goal.transform.position = hit.point;
            //} 
            #endregion
        }
    }

    //Goal coordinates created randomly.
    void CreateGoal()
    {
        goal.transform.position = new Vector3(Random.Range(-9, 9), 1, Random.Range(-9, 9));
    }

    //New goal created.
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Goal")
        {
            CreateGoal();
        }
    }
}