using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCycler : MonoBehaviour
{

    public Color[] colors;

    void Start()
    {
        StartCoroutine("ColorChanger");
    }

    IEnumerator ColorChanger()
    {
        while(true)
        {
            int randColor = Random.Range(0, 15);

            Camera.main.backgroundColor = colors[randColor];

            yield return new WaitForSeconds(10f);
        }
    }
}