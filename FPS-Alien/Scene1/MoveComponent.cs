using UnityEngine;
using System.Collections;

public class MoveComponent : MonoBehaviour 
{
	[SerializeField,Range(0,20)]
	float _speed = 5f;

	public Vector3 MousePoint
	{
		get
		{
			var temp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			temp.z = transform.position.z;
			return temp;
		}
	}

	void Start()
	{
		StartCoroutine (MoveCoroutine ());
	}

	IEnumerator MoveCoroutine()
	{
		while(true)
		{
			transform.position = Vector3.Lerp(transform.position, MousePoint, Time.deltaTime * _speed);
			yield return null;
		}
	}
}
