using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour 
{

    [SerializeField]
    private Renderer[] _spawnPoints;

    [SerializeField]
    private Enemy[] _enemyPrefabs;  

    [SerializeField]
    private int _enemyQuantity;

    [SerializeField]
    private Transform _playerTransform;

//    private List<Enemy> _enemys = new List<Enemy>();
    private List<Transform> _usedTransforms = new List<Transform>();
    private int counter;

    private void Update()
    {
        //Debug.LogFormat("<color=olive><size=15><b>{0}</b></size></color>", _spawnPoints[0].isVisible);
        EnemyHandler();
    }

//    private void OnDestroy()
//    {
//        foreach (var item in _enemys)
//        {
//            if(!item) continue;
//            item.OnEnemyKilled -= OnEnemyKilled;
//        }
//    }

    private void EnemyHandler()
    {
//        int counter = 0;
//
//        foreach (var item in _enemys)
//        {
//            if(!item) continue;
//            if(item.gameObject.activeSelf) counter++;
//        }

        if(counter >= _enemyQuantity) return;

        _usedTransforms.Clear();

        for (int i = counter; i < _enemyQuantity; i++)
        {
            AddEnemy();
        }
    }

    private void AddEnemy()
    {
        Transform enemyTransform = GetPosition();

        if(!enemyTransform) return;

        if(UIController.Instance) UIController.Instance.AddMessage("Enemy added"); 

//        foreach (var item in _enemys)
//        {
//            if(!item) continue;
//            if(!item.gameObject.activeSelf) 
//            {
//                item.Initialize(_playerTransform, enemyTransform.position, enemyTransform.rotation);
//                counter++;
//                return;
//            }
//        }

        //var temp = Instantiate(_enemyPrefabs.GetRandomItem()) as Enemy;
		var prefab = _enemyPrefabs.GetRandomItem();
		var temp = prefab.GetClone<Enemy> ();

        //_enemys.Add(temp);
		//var tempEnemy = temp.GetComponent<Enemy>();

		temp.OnEnemyKilled += OnEnemyKilled;
        temp.Initialize(_playerTransform, enemyTransform.position, enemyTransform.rotation);
        counter++;
    }

    private Transform GetPosition()
    {
        foreach (var item in _spawnPoints)
        {
            if(item == null) continue;
            if(!item.isVisible && !_usedTransforms.Contains(item.transform)) 
            {
                _usedTransforms.Add(item.transform);
                return item.transform;
            }
        }

        return null;
    }

	private void OnEnemyKilled (Enemy enemy)
    {
		enemy.OnEnemyKilled -= OnEnemyKilled;
        counter--;
        _enemyQuantity++;
    }

}
