using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeText : MonoBehaviour 
{
	[SerializeField]
	Color _startColor;

	[SerializeField]
	Color _endColor;

	[SerializeField]
	float _fadeSpeed;

	[SerializeField]
	private Text _ammoDepleted;

	public string Text
	{
		set
		{
			_ammoDepleted.text = value;
		}
	}

	public void Show(string text)
	{
		this._ammoDepleted.text = text;
		StopAllCoroutines();
		StartCoroutine(ShowEffect()); 
	}

	private IEnumerator ShowEffect()
	{
		if(!_ammoDepleted)
		{
			yield break;
		}

		_ammoDepleted.color = _startColor;

		while (_ammoDepleted.color != _endColor)
		{
			_ammoDepleted.color = Color.Lerp(_ammoDepleted.color, _endColor, Time.deltaTime * _fadeSpeed);
			yield return null;
		}
	}
}
