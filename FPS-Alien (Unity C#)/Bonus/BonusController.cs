using UnityEngine;
using System.Collections;

public class BonusController : MonoBehaviour 
{
	[SerializeField]
	PickupBonus[] _bonusPrefabs;

	[SerializeField]
	Transform[] _spawn;

	[SerializeField]
	float _minDelay = 1f;

	[SerializeField]
	float _maxDelay = 5f;

	void Start () 
	{
		AddNewBonus ();
	}

	void AddNewBonus()
	{		
		float time = Random.Range (_minDelay, _maxDelay);
		StopAllCoroutines();
		StartCoroutine (WaitAndAddBonus (time));
	}

	IEnumerator WaitAndAddBonus(float time)
	{
		yield return new WaitForSeconds (time);

		var temp = _bonusPrefabs.GetRandomItem ();

		var go = temp.GetClone ();

		//go.transform.position = _spawn.GetRandomItem ().position;

		var tempPos = _spawn.GetRandomItem ().position;
		tempPos.y = go.transform.position.y;

		//var tempBonus = go.GetComponent<PickupBonus> ();
		go.transform.position = tempPos;

		if (go.Type == BonusType.Ammo)
			go.AmmoType = (AmmoType)Random.Range (0, 3);

		go.OnBonusGet += TempBonus_OnBonusGet;
	}

	void TempBonus_OnBonusGet (PickupBonus obj)
	{
		obj.OnBonusGet -= TempBonus_OnBonusGet;
		AddNewBonus();
	}
}
