using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AimComponent : MonoBehaviour 
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Text _distanceText;

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ViewportPointToRay(Vector3.one * .5f), out hit, Camera.main.farClipPlane);

        _image.color = hit.collider && hit.collider.gameObject.GetComponent<Enemy>() ? Color.red : Color.white;

        if(_distanceText)
        {
            if(_image.color == Color.red) _distanceText.text = hit.distance.ToString("F");
            else
            {
                _distanceText.text = string.Empty;
            }
        }
    }
}
