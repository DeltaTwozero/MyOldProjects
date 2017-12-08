using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	[SerializeField]
	Rigidbody _rigidbody;

	[SerializeField,Range(0,10)]
	float _lifetime = 3f;

	WaitForSeconds _waitForSeconds;

    public float Damage
    {
        get;
        set;
    }

	void Start()
	{
		_waitForSeconds = new WaitForSeconds (_lifetime);
	}

	public void Initialize(Vector3 direction, float damage)
	{
		Damage = damage;
		if (_rigidbody)
			_rigidbody.AddForce (direction, ForceMode.Impulse);
		else
			print ("ball.rigidbody is missing");

		StartCoroutine (DelayAndDisable ());
	}

	public void Disable()
	{
		if (_rigidbody)
			_rigidbody.velocity = Vector3.zero;
		gameObject.transform.PutClone ();
	}

    private void OnCollisionEnter(Collision collision) 
    {
        HealthComponent temp = collision.gameObject.GetComponent<HealthComponent>();

        if(temp) 
        {
            temp.TakeDamage(Damage);
        }
			
		StopAllCoroutines ();
		Disable ();
    }

	IEnumerator DelayAndDisable()
	{
		yield return _waitForSeconds;

		Disable ();
	}
}
