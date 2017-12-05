using UnityEngine;
using System.Collections;

public class BomberGun : GunBase 
{
    [SerializeField]
    private Rigidbody _bulletPrefab;

    [SerializeField]
    private Transform _spawnTransform;

    [SerializeField, Range(.1f,10f)]
    private float _sliderSpeed = 1f;

    private float _currentForce;
    private float CurrentForce
    {
        get
        {
            return _currentForce;
        }

        set
        {
            _currentForce = Mathf.Clamp01(value);

            if(UIController.Instance) UIController.Instance.SetForceValue(_currentForce);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            CurrentForce = 0;
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(CurrentForce == 0) _sliderSpeed = Mathf.Abs(_sliderSpeed);
            if(CurrentForce == 1) _sliderSpeed = -Mathf.Abs(_sliderSpeed);
            CurrentForce += Time.deltaTime * _sliderSpeed;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    #region implemented abstract members of GunBase

    public override void Fire()
    {
		if (AmmoQuantity <= 0) 
		{
			return;
		}

        var temp = Instantiate(_bulletPrefab, _spawnTransform.position, _spawnTransform.rotation) as Rigidbody;

        Bomb tempBomb = temp.gameObject.GetComponent<Bomb>();
        if(tempBomb) tempBomb.Damage = _damage;

        temp.AddForce(_camera.transform.forward * _force * CurrentForce, ForceMode.Impulse);
		DecrementAmmoAndLable ();
    }

    #endregion
}
