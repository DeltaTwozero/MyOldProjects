using UnityEngine;
using System.Collections;
//using UnityEngine.UI;
using System;

public class HealthComponent : MonoBehaviour 
{

    public event Action<float,float> OnTakeDamage;

    private void OnTakeDamageHandler(float damage, float currentHealth)
    {
        if(OnTakeDamage != null) OnTakeDamage(damage, currentHealth); 
    }

//    [SerializeField]
//    private Slider _lifeSlider;

    [SerializeField]
    private float _maxLifeValue;

    private float _currentLifeValue;

    public float CurrentLifeValue
    {
        set
        {
            _currentLifeValue = value;
        }

		get
		{
			return _currentLifeValue;
		}
    }

    private void Start()
    {
        if(UIController.Instance)
        {
//            UIController.Instance.maxValue = _maxLifeValue;
//            UIController.Instance.value = _maxLifeValue;
            //_lifeSlider.minValue = 0;
        }

        _currentLifeValue = _maxLifeValue;
    }

    public void TakeDamage(float damage)
    {
        _currentLifeValue -= damage;
        OnTakeDamageHandler(damage, _currentLifeValue);
        //if(UIController.Instance) UIController.Instance.SetLifeValue(_currentLifeValue);
    }

	public void SetMaxHP()
	{
		_currentLifeValue = _maxLifeValue;
	}
}
