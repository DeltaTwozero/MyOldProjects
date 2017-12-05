using UnityEngine;
using System.Collections;
using System;

public class PickupBonus : MonoBehaviour 
{
	[SerializeField]
	BonusType _type;

	[SerializeField]
	float _quantity;

	[SerializeField]
	AmmoType _ammoType;

	public event Action<PickupBonus> OnBonusGet;

	void OnBonusGetHandler()
	{
		if (OnBonusGet != null)
			OnBonusGet (this);
	}

	public AmmoType AmmoType
	{
		get
		{
			return _ammoType;;
		}

		set
		{
			_ammoType = value;
		}
	}

	public BonusType Type
	{
		get
		{
			return _type;
		}

		set
		{
			_type = value;
		}
	}

	public float Quantity
	{
		get
		{
			return _quantity;
		}

		set
		{
			_quantity = value;
		}
	}

	void OnDisable()
	{
		OnBonusGetHandler ();
	}
}
	

public enum BonusType
{
	Ammo,
	Health,
	Cargo
}

public enum AmmoType
{
	Phys,
	Ray,
	Granata
}