using UnityEngine;
using System.Collections;
using System;

public class InputController : MonoBehaviour 
{
    public event Action<Vector3> OnMouseDown;

    private void OnMouseDownHandler(Vector3 pos)
    {
        if(OnMouseDown != null) OnMouseDown(pos);   
    }

    [SerializeField]
    private Collider _locationCollider;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Transform _marker;

    private void Update()
    {
        RaycastHit hit;

        if(_locationCollider.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, _camera.farClipPlane))
        {
            _marker.position = new Vector3(hit.point.x, _marker.position.y, hit.point.z);
            if(Input.GetKeyDown(KeyCode.Mouse0)) OnMouseDownHandler(hit.point);
        }
    }
}
