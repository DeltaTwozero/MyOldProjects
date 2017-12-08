using UnityEngine;
using System.Collections;

public class Headlight : MonoBehaviour 
{
	[SerializeField, Range(1,100)]
	float _lifeTime = 60;

	[SerializeField]
	AnimationCurve _curve;

	[SerializeField]
	private float _workTime;

	private float _originIntencity;

	private Light _light;

	private Light Light
	{
		get
		{
			if (!_light) 
			{
				_light = GetComponent<Light> ();
				if (_light)
					_originIntencity = _light.intensity;
			}
			return _light;
		}
	}

	public bool isOn 
	{
		get 
		{
			return (Light ? Light.enabled : false);
		}

		set
		{
			if (!Light)
				return;
			Light.enabled = value;
		}
	}

	void Update()
	{
		if (!Light)
			return;
		if (!isOn)
			return;

		_workTime += Time.deltaTime;
		_workTime = Mathf.Clamp(_workTime, 0, _lifeTime);

		Light.intensity = _originIntencity * _curve.Evaluate(_workTime / _lifeTime);
	}
}
