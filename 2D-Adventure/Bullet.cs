using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    Vector3 dir = Vector2.zero;
    float speed = 5f;

    public Vector3 Dir
    {
        set{dir = value;}
    }

	void Update ()
    {
        if (dir.Equals(Vector3.zero))
            Destroy(gameObject);
        transform.position += dir * speed * Time.deltaTime;
	}
}
