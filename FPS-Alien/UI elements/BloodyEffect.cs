using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BloodyEffect : MonoBehaviour 
{
    [SerializeField]
    private Color _startColor;

    [SerializeField]
    private Color _endColor;

    [SerializeField]
    private float _fadeSpeed;

    [SerializeField]
    private Image _image;

    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(ShowEffect()); 
    }

    private IEnumerator ShowEffect()
    {
        if(!_image)
        {
            yield break;
        }

        _image.color = _startColor;

        while (_image.color != _endColor)
        {
            _image.color = Color.Lerp(_image.color, _endColor, Time.deltaTime * _fadeSpeed);
            yield return null;
        }
    }

}
