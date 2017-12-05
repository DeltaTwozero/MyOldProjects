using UnityEngine;
using System.Collections;

public class PhysicGun : GunBase 
{
    [SerializeField]
	private Ball _bulletPrefab;

    [SerializeField]
    private Transform _spawnTransform;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }


    #region implemented abstract members of GunBase
    public override void Fire()
    {
		if (AmmoQuantity <= 0) 
		{
			if (UIController.Instance)
				UIController.Instance.ShowFadeText ("Out of ammo");
			return;
		}

        //var temp = Instantiate(_bulletPrefab, _spawnTransform.position, _spawnTransform.rotation) as Rigidbody;
		var temp = _bulletPrefab.GetClone();

		if (!temp)
			return;
		temp.gameObject.SetActive (true);
		temp.gameObject.transform.position = _spawnTransform.position;
		temp.gameObject.transform.rotation = _spawnTransform.rotation;

        //Ball tempBall = temp.GetComponent<Ball>();
		if (temp) 
		{
			temp.Initialize (_camera.transform.forward * _force, _damage);
			DecrementAmmoAndLable ();
		}
        //temp.AddForce(_camera.transform.forward * _force, ForceMode.Impulse);
    }
    #endregion
}
