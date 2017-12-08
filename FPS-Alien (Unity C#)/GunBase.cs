using UnityEngine;
using System.Collections;

public abstract class GunBase : MonoBehaviour 
{
    [SerializeField]
    protected Camera _camera;

    [SerializeField]
    protected float _force = 10;

    [SerializeField]
    protected float _damage = 10;

	[SerializeField]
	string _name;

	[SerializeField]
	int _ammoQuantity;

	public string Name
	{
		get
		{
			return _name;
		}
	}

	public int AmmoQuantity
	{
		get
		{
			return _ammoQuantity;
		}

		set
		{
			_ammoQuantity = value;
		}
	}

	protected void DecrementAmmoAndLable()
	{
		_ammoQuantity = _ammoQuantity <= 0 ? 0 : --_ammoQuantity;

//		if (_ammoQuantity = 0) 
//		{
//			Input.GetKeyDown(KeyCode.R) _ammoQuantity = _ammoQuantity;
//		}

		if (UIController.Instance)
			UIController.Instance.SetBulletQuantity (_ammoQuantity);
	}

    public abstract void Fire();
}
