using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour 
{

	public event Action<Enemy> OnEnemyKilled;

    private void OnEnemyKilledHandler()
    {
        if(OnEnemyKilled != null)
			OnEnemyKilled(this);
    }

    [SerializeField]
    private HealthComponent _heathComponent;

    [SerializeField]
    private Slider _lifeSlider;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;

    [SerializeField]
    private float _attackDelay = 1;

    [SerializeField]
    private float _damage = 10;

    [SerializeField]
    private float _health = 100;

    [SerializeField]
    private AnimationComponent _animationComponent;

    private float _timeToNextAttack;
    private HealthComponent _playerHealth;
    //private bool _isDead;
	Enemystates _enemyStates;


    public Transform Player
    {
        set
        {
            _player = value;
        }
    }

    public void Initialize(Transform player, Vector3 pos, Quaternion rot)
    {
        //_isDead = false;
		_enemyStates = Enemystates.normal;
        if(_animationComponent) _animationComponent.Run();
        gameObject.SetActive(true);
        transform.position = pos;
        transform.rotation = rot;

        Player = player;
        if(_lifeSlider)
        {
            _lifeSlider.maxValue = _lifeSlider.value = _health;
            _lifeSlider.minValue = 0;
        }

        if(_heathComponent)
        {
            _heathComponent.CurrentLifeValue = _health;
        }

    }

    private void Start()
    {
        if(_heathComponent) _heathComponent.OnTakeDamage += OnTakeDamage; 
        if(_player) _playerHealth = _player.GetComponent<HealthComponent>();
        if(_animationComponent) _animationComponent.OnAttack += OnAttack;
    }

    private void OnDestroy()
    {
        if(_heathComponent) _heathComponent.OnTakeDamage -= OnTakeDamage; 
        if(_animationComponent) _animationComponent.OnAttack -= OnAttack;

    }

    private void OnTakeDamage (float arg1, float arg2)
    {
		if(_enemyStates == Enemystates.dead) return;

        if(_lifeSlider) {_lifeSlider.value = arg2;}

        if(arg2 <= 0) 
        {   
            //_isDead = true;
			_enemyStates = Enemystates.dead;
            if(_animationComponent) _animationComponent.Die();
            if(UIController.Instance) UIController.Instance.AddMessage("Enemy killed"); 
            _navMeshAgent.destination = transform.position;
            Invoke("Die", 1f);
        }
        else
        {
            if(_animationComponent) _animationComponent.Hit();
            _navMeshAgent.destination = transform.position;
            //_isDead = true;
			_enemyStates = Enemystates.damaged;
            Invoke("HitEnd", .5f);
        }
    }

    private void HitEnd()
    {
        //_isDead = false;
		_enemyStates = Enemystates.normal;
    }

    private void Die()
    {
        OnEnemyKilledHandler();
        gameObject.SetActive(false);
    }

    private void OnAttack()
    {
        if(_playerHealth) _playerHealth.TakeDamage(_damage);
    }

    private void Update()
    {
		if(_enemyStates != Enemystates.normal) return;

        if(Vector3.Distance(_player.position, transform.position) <= 3 && Time.time > _timeToNextAttack) 
        {
            if(_animationComponent) _animationComponent.Attack();
            _timeToNextAttack = Time.time + _attackDelay;
            return;
        }

        if(_navMeshAgent && _player && Vector3.Distance(_player.position, transform.position) > 3f) 
        {
            _navMeshAgent.destination = _player.position;
            if(_animationComponent) _animationComponent.Run();
        }
    }

//    [SerializeField]
//    private InputController _inputController;
//
//    [SerializeField]
//    private float _speed = 2;
//
//    [SerializeField]
//    private NavMeshAgent _agent;
//
//    private Vector3 _targetPoint;
//
//    private void Start()
//    {
//        if(_inputController) _inputController.OnMouseDown += OnMouseDown;
//        _targetPoint = transform.position;
//    }
//
//    private void OnDestroy()
//    {
//        if(_inputController) _inputController.OnMouseDown -= OnMouseDown;
//    }
//
//    private void OnMouseDown(Vector3 obj)
//    {
//        _targetPoint = new Vector3(obj.x, transform.position.y, obj.z);
//        if(_agent) _agent.destination = _targetPoint;
////        StopAllCoroutines();
////        StartCoroutine(MoveToPoint(_targetPoint, Random.Range(.5f, 3f)));
//    }
//
//    private IEnumerator MoveToPoint(Vector3 pos, float time)
//    {
//        Debug.LogFormat("<color=olive><size=15><b>{0} {1}</b></size></color>", "start coroutine", time);
//
//       // yield return new WaitForSeconds(time);
//
//        Debug.LogFormat("<color=olive><size=15><b>{0}</b></size></color>", "start move");
//
//        while(Vector3.Distance(transform.position, pos) > 1)
//        {
//            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, Time.deltaTime * _speed);
//            yield return null;
//        }
//
//        Debug.LogFormat("<color=olive><size=15><b>{0}</b></size></color>", "end coroutine");
//    }

//    private void Update()
//    {
//        //transform.position = Vector3.Lerp(transform.position, _targetPoint, Time.deltaTime * _speed);
//        if(_agent) _agent.destination = _targetPoint;
//    }
}

public enum Enemystates
{
	normal,
	damaged,
	dead
}