using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour
{
    Color colorStart;
    [SerializeField]
    Color colorRandom;
    [SerializeField]
    SpriteRenderer spriteRend;


	void Start ()
    {
        colorStart = spriteRend.color;
        InvokeRepeating("ChangeColor", 0, 5);
	}

    void Update()
    {
        spriteRend.color = Color.Lerp(colorStart, colorRandom, Time.deltaTime);
    }

    void ChangeColor()
    {
        colorRandom = new Color(Random.value, Random.value, Random.value, 1f);
    }
}
