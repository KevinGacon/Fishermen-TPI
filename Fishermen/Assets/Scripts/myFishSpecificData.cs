/**********************************************
 * Projet : Fishermen
 * Nom du fichier : myFishSpecificData.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Kevin Gacon
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFishSpecificData : MonoBehaviour
{
    // Variables

    //le type de poisson
    public FishData FishData;

    //la taille du poisson
    public float currentSize = 0;
    //l'âge de poisson
    public int currentAge = 0;

    //le jour pêché
    public int dayFished;
    //l'heure pêché
    public int hourFished;
    //la minute pêché
    public int minuteFished;

    //le jour où il ne sera plus frais
    public int notFreshDay;

    //s'il a la taille requis
    public bool haveRequiredSize = true;
    //s'il a l'âge requis
    public bool haveRequiredAge = true;
    //si le poisson est frais
    public bool isFresh = true;

    /// <summary>
    /// Awake est appelé quand l'instance de script est chargée
    /// </summary>
    void Awake()
    {
        //choisit aléatoirement un poisson en fonction des poissons dispoible dans la zone de pêche
        int randomFish = Random.Range(0, FishingGame.instance.FishesCanBeCaughtInThisArea.Count);
        FishData = FishingGame.instance.FishesCanBeCaughtInThisArea[randomFish];

        //génére une taille aléatoire
        float randomSize = Random.Range(FishData.minSize, FishData.maxSize);
        //arrondit le nombre généré
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

        //génére un âge aléatoire
        int randomAge = Random.Range(0, FishData.ageMax);
        if (currentAge == 0) currentAge = randomAge;

        //stock le moment où il a été pêché
        dayFished = DayTimeSystem.instance.checkDay;
        minuteFished = DayTimeSystem.instance.checkMinute;
        hourFished = DayTimeSystem.instance.checkHour;

        //stock le moment où le poisson ne sera plus frais
        notFreshDay = dayFished + 1;

        //vérifie s'il a l'âge et la taille requis
        if (currentSize < FishData.minSizeRequired) haveRequiredSize = false;
        if (currentAge < FishData.minAgeRequired) haveRequiredAge = false;
    }

    /// <summary>
    /// UpdateFresh est une fonction qui permet de mettre à jour la fraîcheur du poisson
    /// </summary>
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
