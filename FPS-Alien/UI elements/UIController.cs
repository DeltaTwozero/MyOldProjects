using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour 
{
    [SerializeField]
    private Slider _lifeSlider;

    [SerializeField]
    private Slider _forceSlider;

    [SerializeField]
    private BloodyEffect _bloodyEffect;

    [SerializeField]
    private GameObject _gameOverScreen;

    [SerializeField]
    private Text _messagesText;

    [SerializeField]
    private int _messegeQuantity = 6;

	[SerializeField]
	Text _gunName;

	[SerializeField]
	Text _ammoCurrent;

	[SerializeField]
	FadeText _fadeText;

	[SerializeField]
	Text _cargoQuantity;

	[SerializeField]
	Text _hpQuantity;

    private Queue<string> _messages = new Queue<string>();

    private static UIController _instance;

    public static UIController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Start()
    {
        if(!_instance)
        {
            _instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

	public void SetHP(int val)
	{
		if (_hpQuantity)
			_hpQuantity.text = val.ToString();
	}

	public void SetCargo(int val)
	{
		if (_cargoQuantity)
			_cargoQuantity.text = val.ToString();
	}

	public void SetGunName(string name)
	{
		if (_gunName)
			_gunName.text = name;
	}

	public void SetBulletQuantity(int value)
	{
		if (_ammoCurrent)
			_ammoCurrent.text = value.ToString ();
	}

	public void ShowFadeText(string value)
	{
		if (_fadeText)
			_fadeText.Show (value);
	}

    public void AddMessage(string message)
    {
        if(!_messagesText) return;

        _messages.Enqueue(message);

        if(_messages.Count > _messegeQuantity) _messages.Dequeue();

        _messagesText.text = string.Empty;

        foreach (var item in _messages)
        {
            _messagesText.text += ">> " + item + "\n";
        }

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().name));
    }

    public void SetActiveGameOverScreen(bool flag)
    {
        if(_gameOverScreen) _gameOverScreen.SetActive(flag);
    }

    public void ShowBloodyScreen()
    {
        if(_bloodyEffect) _bloodyEffect.Show(); 
    }

    public void SetLifeValue(float value)
    {
        if(!_lifeSlider) return;

        _lifeSlider.value = value;
    }

    public void SetForceValue(float value)
    {
        if(!_forceSlider) return;

        _forceSlider.value = value;
    }

}
