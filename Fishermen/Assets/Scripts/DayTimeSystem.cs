/**********************************************
 * Projet : Fishermen
 * Nom du fichier : DayTimeSystem.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Kevin Gacon
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayTimeSystem : MonoBehaviour
{
    // Variables
    static int day = 1;
    static int hour = 8;
    static int minute = 0;

    public int checkDay;
    public int checkHour;
    public int checkMinute;

    public TMP_Text dayTimeText;

    private bool timerIsActive;

    //stock une instance qui permet d'appeler le script sur n'importe quel script
    public static DayTimeSystem instance;

    /// <summary>
    /// Awake est appel� quand l'instance de script est charg�e
    /// </summary>
    private void Awake()
    {
        //v�riife si le script est unique sur la sc�ne
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DayTimeSystem dans la sc�ne");
            return;
        }

        instance = this;
    }

    /// <summary>
    /// Start est appel� une fois avant update()
    /// </summary>
    private void Start()
    {
        // met � jour le timer � chaque chargement
        UpdateDayTime();
    }

    /// <summary>
    /// Update est appel� une fois par mise � jour de trame
    /// </summary>
    void Update()
    {
        // chaque seconde le timer augmente 
        if (!timerIsActive) StartCoroutine(NextSecond());

        //touche de DEBUG qui permet d'acc�lerer le temp
        if (Input.GetKey(KeyCode.T))
        {
            if (minute != 59)
            {
                minute++;
            }
            else if (hour != 23)
            {
                hour++;
                minute = 0;
            }
            else
            {
                day++;
                hour = 0;
                minute = 0;
            }

            UpdateDayTime();
        }
    }

    /// <summary>
    /// NextSecond est une coroutine qui augmente le timer chauqe seconde
    /// </summary>
    IEnumerator NextSecond()
    {
        timerIsActive = true;

        yield return new WaitForSeconds(1);

        // verifie les valeurs des timers pour changer en fonction
        if (minute != 59)
        {
            minute++;
        }
        else if (hour != 23)
        {
            hour++;
            minute = 0;
        }
        else
        {
            day++;
            hour = 0;
            minute = 0;
        }

        UpdateDayTime();

        timerIsActive = false;
    }

    /// <summary>
    /// UpdateDayTime est une fonction qui permet met � jour le texte du timer
    /// </summary>
    void UpdateDayTime()
    {   
        //affiche corretement le timer
        if (hour < 10 && minute < 10)
        {
            dayTimeText.text = "Jour " + day + " | 0" + hour + ":0" + minute;
        }
        else if (hour < 10)
        {
            dayTimeText.text = "Jour " + day + " | 0" + hour + ":" + minute;
        }
        else if (minute < 10)
        {
            dayTimeText.text = "Jour " + day + " | " + hour + ":0" + minute;
        }
        else
        {
            dayTimeText.text = "Jour " + day + " | " + hour + ":" + minute;
        }

        checkDay = day;
        checkHour = hour;
        checkMinute = minute;
    }
}
