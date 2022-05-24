using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFishSpecificData : MonoBehaviour
{
    //stock des données spécifiques d'un poisson
    public FishData FishData;

    public float currentSize = 0;
    public int currentAge = 0;

    public int dayFished;
    public int hourFished;
    public int minuteFished;

    public int notFreshDay;

    public bool haveRequiredSize = true;
    public bool haveRequiredAge = true;
    public bool isFresh = true;


    void Awake()
    {
        int randomFish = Random.Range(0, FishingGame.instance.FishesCanBeCaughtInThisArea.Count);
        FishData = FishingGame.instance.FishesCanBeCaughtInThisArea[randomFish];

        float randomSize = Random.Range(FishData.minSize, FishData.maxSize);
        if (currentSize == 0)
        {
            currentSize = Mathf.Round(randomSize * 10.0f) * 0.1f;

            if (currentSize.ToString().Length > 6)
            {
                string str = currentSize.ToString();

                str = str.Substring(0, str.Length - 5);

                currentSize = float.Parse(str);

                Debug.Log(str);
                Debug.Log(currentSize);
            }
        }

        int randomAge = Random.Range(0, FishData.ageMax);
        if (currentAge == 0) currentAge = randomAge;

        dayFished = DayTimeSystem.instance.checkDay;
        minuteFished = DayTimeSystem.instance.checkMinute;
        hourFished = DayTimeSystem.instance.checkHour;

        notFreshDay = dayFished + 1;

        if (currentSize < FishData.minSizeRequired) haveRequiredSize = false;
        if (currentAge < FishData.minAgeRequired) haveRequiredAge = false;
    }

    public void UpdateFresh()
    {
        if (DayTimeSystem.instance.checkDay == notFreshDay && DayTimeSystem.instance.checkHour >= hourFished && DayTimeSystem.instance.checkMinute >= minuteFished)
        {
            isFresh = false;
        }
        else if (DayTimeSystem.instance.checkDay > notFreshDay)
        {
            isFresh = false;
        }
    }
}
