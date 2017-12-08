using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    //I've made 2 methods for moving platform. I prefer PingPong, but both of them works. I've decided to save them.
	#region PingPong Method

    Vector3 startPos;
    Vector3 goalPos;
    [SerializeField]
    float xpos;
    [SerializeField]
    float ypos;

    [SerializeField]
    bool isHorizontal;

    [SerializeField,Range(0, 1)]
    float speed = 1f;

    void Start()
    {
        //Direction check.
        startPos = this.transform.position;
        if (isHorizontal)
        {
            goalPos = startPos + new Vector3(xpos, 0, 0);
        }
        else
            goalPos = startPos + new Vector3(0, ypos, 0);
    }

    //Platform movement.
    void Update()
    {
        float pingpong = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPos, goalPos, pingpong);
    }

    #endregion

//    #region Lerp Method
//
//    public Transform startPos, goalPos;
//    public bool isRight;
//
//    void Start()
//    {
//        this.transform.position = startPos.position;
//    }
//
//    void Update()
//    {
//        if (Mathf.Round(transform.position.x) == (int)goalPos.position.x)
//        {
//            isRight = false;
//        }
//
//        if (Mathf.Round(transform.position.x) == (int)startPos.position.x)
//        {
//            isRight = true;
//        }
//
//        if (isRight == true)
//        {
//            this.transform.position = Vector3.Lerp(transform.position, goalPos.position, 0.4f);
//        }
//        else
//        {
//            this.transform.position = Vector3.Lerp(transform.position, startPos.position, 0.4f);
//        }
//    }
//    #endregion
}