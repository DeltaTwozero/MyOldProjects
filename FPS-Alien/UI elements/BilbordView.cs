using UnityEngine;
using System.Collections;

public class BilbordView : MonoBehaviour 
{
	void Update () 
    {
	    transform.forward = Camera.main.transform.forward;
	}
}
