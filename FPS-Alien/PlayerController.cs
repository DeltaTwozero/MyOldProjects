using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityRandom = UnityEngine.Random;

public class PlayerController : MonoBehaviour 
{
    [SerializeField]
    private GunBase[] _guns;

	[SerializeField]
	Headlight _light;

    [SerializeField]
    private HealthComponent _healthComponent;

    [SerializeField]
    private FirstPersonController _firstPersonController;

    private int _currentGunIndex;
	private int _hpQuantity;
	private int _cargoQuantity;

    private void Start()
    {
        SetActiveGun(_currentGunIndex);

        if(_healthComponent) _healthComponent.OnTakeDamage += OnTakeDamage;
    }

    private void OnDestroy()
    {
        if(_healthComponent) _healthComponent.OnTakeDamage -= OnTakeDamage;
    }

    private void OnTakeDamage (float arg1, float arg2)
    {
		if (UIController.Instance) 
		{
			UIController.Instance.SetLifeValue (arg2);

			if (arg1 > 0) 
			{
				UIController.Instance.ShowBloodyScreen ();
				if (UIController.Instance)
					UIController.Instance.AddMessage (string.Format ("Player gets {0} damage, current health {1}", arg1, arg2));
			}
			//"Player gets " + arg1 + " damage"

			if (arg2 <= 0) 
			{
				UIController.Instance.SetActiveGameOverScreen (true);
				if (_firstPersonController)
					_firstPersonController.UnlockCursor ();
			}
		}
      }

    private void LateUpdate()
    {
        ScrollWheelHandler();
		UserInputHandler ();
    }

	void UserInputHandler()
	{
		if (Input.GetKeyDown (KeyCode.H) && _hpQuantity > 0) 
			{
				_hpQuantity--;
				_healthComponent.SetMaxHP ();
				OnTakeDamage (0, _healthComponent.CurrentLifeValue);
				if (UIController.Instance)
				UIController.Instance.SetHP (_hpQuantity);
			}

		if (Input.GetKeyDown (KeyCode.C) && _cargoQuantity > 0) 
		{
			_cargoQuantity--;
			UseBonus ((BonusType)UnityRandom.Range (0, 2), (AmmoType)UnityRandom.Range (0, 3), 15);

			if (UIController.Instance)
				UIController.Instance.SetCargo (_cargoQuantity);
		}

		if (_light && Input.GetKeyDown (KeyCode.F)) 
		{
			_light.isOn = !_light.isOn;
		}
	}

	public void UseBonus(BonusType type, AmmoType ammo, int quantity)
	{
		if (type == BonusType.Ammo) 
		{
			_guns [(int)ammo].AmmoQuantity += quantity;
			SetActiveGun (_currentGunIndex);
		} 
		else
		{
			_hpQuantity++;
			if (UIController.Instance)
				UIController.Instance.SetHP (_hpQuantity);
		}
	}

	void UseBonus(PickupBonus temp)
	{
		SetBonusData (temp);

		if (temp.Type == BonusType.Ammo) 
		{
			_guns [(int)temp.AmmoType].AmmoQuantity += (int)temp.Quantity;
			SetActiveGun (_currentGunIndex);
		} 
		else
		{
			if (temp.Type == BonusType.Health)
			{
				_hpQuantity++;
				if (UIController.Instance)
					UIController.Instance.SetHP (_hpQuantity);
				//_healthComponent.CurrentLifeValue += temp.Quantity;
			}
			else
			{
				_cargoQuantity++;
				if (UIController.Instance)
					UIController.Instance.SetCargo (_cargoQuantity);
				print ("cargo recieved");
			}
		}
		temp.gameObject.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		print ("rabotaet");

		var temp = other.GetComponent<PickupBonus> ();

		if (!temp)
			return;

		UseBonus (temp);
	}

	void SetBonusData(PickupBonus bonus)
	{
		if (!bonus)
			return;
		if (UIController.Instance)
			UIController.Instance.AddMessage (string.Format ("Bonus {0}{1}{2}", bonus.Type, (bonus.Type == BonusType.Ammo ? bonus.AmmoType.ToString () : string.Empty), bonus.Quantity));
	}

    private void ScrollWheelHandler()
    {
        var temp = Input.GetAxis("Mouse ScrollWheel");

        if(temp == 0) return;

        _currentGunIndex = temp > 0 ? ++_currentGunIndex : --_currentGunIndex;
        _currentGunIndex = _currentGunIndex < 0 ? _guns.Length - 1 : _currentGunIndex;
        _currentGunIndex = _currentGunIndex >= _guns.Length ? 0 : _currentGunIndex;

        SetActiveGun(_currentGunIndex);
    }

    private void SetActiveGun(int index)
    {
        if(_guns == null || _guns.Length < 2) return;

        foreach (var item in _guns)
        {
            if(!item) continue;
            item.gameObject.SetActive(false);
        }

        if(index < 0 || index >= _guns.Length || !_guns[index]) return;

        _guns[index].gameObject.SetActive(true);

		if(UIController.Instance) 
		{
			UIController.Instance.SetGunName (_guns [index].Name);
			UIController.Instance.SetBulletQuantity (_guns [index].AmmoQuantity);
		}
    }
}
