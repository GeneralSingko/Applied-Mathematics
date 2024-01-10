using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineCosineGenerator : MonoBehaviour
{
    public float sineValue;
    public float cosineValue;

    private void Start()
    {
        GenerateRandomValues();
    }

    public void GenerateRandomValues()
    {
        sineValue = Random.Range(1f, 10f);
        cosineValue = Random.Range(1f, 10f);
    }
}
