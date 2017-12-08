using UnityEngine;
using System.Collections;

public class AnimationComponent : MonoBehaviour 
{
    public event System.Action OnAttack;

    private void OnAttackHandler()
    {
        if(OnAttack != null) OnAttack(); 
    }

    [SerializeField]
    private Animation _animation;

    [SerializeField]
    private string _runAnimationKey = "run";

    [SerializeField]
    private string[] _attackAnimationKeys;

    [SerializeField]
    private string[] _dieAnimationKeys;

    [SerializeField]
    private string[] _hitAnimationKeys;

    public void Play(string animationKey)
    {
        if(!_animation) return;
        _animation.Play(animationKey);
    }



    public void Attack()
    {
        Play(_attackAnimationKeys.GetRandomItem());
    }

    public void Run()
    {
        Play(_runAnimationKey);
    }

    public void Die()
    {
        Play(_dieAnimationKeys.GetRandomItem());
    }

    public void Hit()
    {
        Play(_hitAnimationKeys.GetRandomItem());
    }

    public void AnimationAttack()
    {
        //Debug.LogFormat("<color=olive><size=15><b>{0}</b></size></color>", "attack");
        OnAttackHandler();
    }

//    private void OnGUI()
//    {
//        if(GUI.Button(new Rect(10,10,100,50), "Run"))
//        {
//            Run();
//        }
//
//        if(GUI.Button(new Rect(10,60,100,50), "Attack"))
//        {
//            Attack();
//        }
//
//        if(GUI.Button(new Rect(10,110,100,50), "Die"))
//        {
//            Die();
//        }
//
//        if(GUI.Button(new Rect(10,160,100,50), "Hit"))
//        {
//            Hit();
//        }
//    }

}

public static class ExtentionsArray
{
    public static T GetRandomItem<T>(this T[] array)
    {
        //if(array == null || array.Length == 0) return null;

        return array[Random.Range(0, array.Length)];
    }

	public static void setdefpos(this GameObject go)
	{
		//if(array == null || array.Length == 0) return null;

		go.transform.position = Vector3.zero;
	}

	public static void setParent(this GameObject go, GameObject newParent)
	{
		//if(array == null || array.Length == 0) return null;

		go.transform.parent = newParent.transform;
	}

	public static T AddOrGetComponent<T>(this GameObject obj) where T : Component
	{
		if (obj == null)
			return null;
		var temp = obj.GetComponent<T> ();
		if (temp)
			return temp;

		return obj.AddComponent<T> ();
	}
}
