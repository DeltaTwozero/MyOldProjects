using UnityEngine;
using System.Collections;
using UnityStandardAssets.Effects;

public class Bomb : MonoBehaviour 
{
    [SerializeField]
    private GameObject _effect;

    [SerializeField]
    private ExplosionPhysicsForce _explosionPhysicsForce;

    public float Damage 
    {
        get;
        set;
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(_explosionPhysicsForce) _explosionPhysicsForce.Damage = Damage;

        if(_effect)
        {
            _effect.transform.parent = null;
            Vector3 tempPos = transform.position;
            tempPos.y += 1f;
            _effect.transform.position = tempPos;
            _effect.SetActive(true);
        }

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
