using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFishSpecificData : MonoBehaviour
{
    //stock des données spécifiques d'un poisson
    public FishData FishData;
    public float currentSize = 0;

    public int currentAge = 0;

    void Awake()
    {
        int randomFish = Random.Range(0, FishingGame.instance.FishesCanBeCaughtInThisArea.Count);
        FishData = FishingGame.instance.FishesCanBeCaughtInThisArea[randomFish];

        float randomSize = Random.Range(FishData.minSize, FishData.maxSize);
        if (currentSize == 0) currentSize = Mathf.Round(randomSize * 10.0f) * 0.1f;

        int randomAge = Random.Range(0, FishData.ageMax);
        if (currentAge == 0) currentAge = randomAge;
    }
}
