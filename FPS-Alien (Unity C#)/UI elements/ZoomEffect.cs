using UnityEngine;
using System.Collections;

public class ZoomEffect : MonoBehaviour 
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _maxFOV;

    [SerializeField]
    private float _minFOV;

    [SerializeField, Range(1,50)]
    private float _zoomSpeed = 5f;

    [SerializeField]
    private GameObject _binocularsImage;

    private void Start()
    {
        if(_camera) _camera.fieldOfView = _maxFOV;
        if(_binocularsImage) _binocularsImage.SetActive(false);
    }

    private void Update()
    {
       
        if(Input.GetKey(KeyCode.Mouse1))
        {
            if(!_camera) return;
            if(_binocularsImage) _binocularsImage.SetActive(true);

            _camera.fieldOfView -= Time.deltaTime * _zoomSpeed;
            _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, _minFOV, _maxFOV); 

        }
        else
        {
            _camera.fieldOfView += Time.deltaTime * _zoomSpeed;
            _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, _minFOV, _maxFOV); 
            if(_camera.fieldOfView == _maxFOV && _binocularsImage) _binocularsImage.SetActive(false);
        }
    }
}
