using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageManagerButton : MonoBehaviour
{
    [SerializeField]
    Button _nextImg;

    [SerializeField]
    Button _prevImg;

    [SerializeField]
    Sprite[] _imgs;

    [SerializeField]
    Image _show;

    [SerializeField]
    int _currentImg;

    void Start()
    {
        _currentImg = 0;
        _show.sprite = _imgs[_currentImg];
    }

    public void NextImg()
    {
        if (_currentImg + 1 < _imgs.Length)
        {
            print("++ works");
            _currentImg++;
        }
        else
        {
            _currentImg = 0;
        }
        _show.sprite = _imgs[_currentImg];
    }

    public void PrevImg()
    {
        if (_currentImg - 1 > -1)
        {
            print("-- works");
            _currentImg--;
        }
        else
        {
            _currentImg = _imgs.Length - 1;
        }
        _show.sprite = _imgs[_currentImg];
    }
}
