using UnityEngine;
using System.Collections;

public class CoinW : MonoBehaviour
{
    //Same idea as "Coin script" but for some reason that script was not working well with a chest. I made this one instead.
    [SerializeField]
    Transform player;

    public bool isMagnet;
   
    public float speed;
    public float distance;

    void Start ()
    {
        player = Hero.instance.transform;
        isMagnet = false;
    }

    void Update ()
    {
        distance = Vector3.Distance(transform.position, player.position);

        if (Hero.instance.isMagActive == true)
        {
            isMagnet = true;
        }
            
        if (distance <= 5f && isMagnet)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
        }
    }
}
