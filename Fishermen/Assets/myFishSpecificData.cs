using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFishSpecificData : MonoBehaviour
{
    //stock des données spécifiques d'un poisson
    public FishData FishData;
    public float currentSize;

    public int currentAge;

    void Awake()
    {
        float randomSize = Random.Range(FishData.maxSize, FishData.minSize);

        if (currentSize == 0) currentSize = Mathf.Round(((randomSize + FishData.commonSize) / 2) * 10.0f) * 0.1f;
    }
}
