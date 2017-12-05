using UnityEngine;
using System.Collections;

public class Bonusrotation : MonoBehaviour 
{
	[SerializeField]
	Vector3 _direction;

	[SerializeField,Range(0,2000)]
	float _speed = 200f;

	void Update()
	{
		transform.Rotate (_direction * Time.deltaTime * _speed);
	}
}
