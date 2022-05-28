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
    //l'�ge de poisson
    public int currentAge = 0;

    //le jour p�ch�
    public int dayFished;
    //l'heure p�ch�
    public int hourFished;
    //la minute p�ch�
    public int minuteFished;

    //le jour o� il ne sera plus frais
    public int notFreshDay;

    //s'il a la taille requis
    public bool haveRequiredSize = true;
    //s'il a l'�ge requis
    public bool haveRequiredAge = true;
    //si le poisson est frais
    public bool isFresh = true;

    /// <summary>
    /// Awake est appel� quand l'instance de script est charg�e
    /// </summary>
    void Awake()
    {
        //choisit al�atoirement un poisson en fonction des poissons dispoible dans la zone de p�che
        int randomFish = Random.Range(0, FishingGame.instance.FishesCanBeCaughtInThisArea.Count);
        FishData = FishingGame.instance.FishesCanBeCaughtInThisArea[randomFish];

        //g�n�re une taille al�atoire
        float randomSize = Random.Range(FishData.minSize, FishData.maxSize);
        //arrondit le nombre g�n�r�
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

        //g�n�re un �ge al�atoire
        int randomAge = Random.Range(0, FishData.ageMax);
        if (currentAge == 0) currentAge = randomAge;

        //stock le moment o� il a �t� p�ch�
        dayFished = DayTimeSystem.instance.checkDay;
        minuteFished = DayTimeSystem.instance.checkMinute;
        hourFished = DayTimeSystem.instance.checkHour;

        //stock le moment o� le poisson ne sera plus frais
        notFreshDay = dayFished + 1;

        //v�rifie s'il a l'�ge et la taille requis
        if (currentSize < FishData.minSizeRequired) haveRequiredSize = false;
        if (currentAge < FishData.minAgeRequired) haveRequiredAge = false;
    }

    /// <summary>
    /// UpdateFresh est une fonction qui permet de mettre � jour la fra�cheur du poisson
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
