using UnityEngine;
using System.Collections;
using System;

public class RayCastGun : GunBase 
{
    [SerializeField]
    private GameObject _rayEffect;

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
			return;
		}

        RaycastHit hit;

        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width * .5f, Screen.height * .5f, 0f));

        _rayEffect.SetActive(true);
        StartCoroutine(WaitAndDisableEffect(.5f));

        if(Physics.Raycast(ray,out hit, _camera.farClipPlane))
        {
            var tempHealth = hit.collider.gameObject.GetComponent<HealthComponent>();  
			if (tempHealth) 
			{
				tempHealth.TakeDamage (_damage);
				DecrementAmmoAndLable ();
			}
            var tempRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
            if(!tempRigidbody) return;
            tempRigidbody.AddForceAtPosition(_camera.transform.forward * _force, hit.point, ForceMode.Impulse);
        }
    }

    #endregion

    private IEnumerator WaitAndDisableEffect(float time)
    {
        Debug.LogFormat("<color=olive><size=15><b>{0}</b></size></color>", "start coroutine");
        yield return new WaitForSeconds(time);

        _rayEffect.SetActive(false);
        Debug.LogFormat("<color=olive><size=15><b>{0}</b></size></color>", "end coroutine");
    }

}
