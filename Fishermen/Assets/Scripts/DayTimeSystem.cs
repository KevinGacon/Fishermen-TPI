using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayTimeSystem : MonoBehaviour
{
    static int day = 1;
    static int hour = 8;
    static int minute = 0;

    public int checkDay;
    public int checkHour;
    public int checkMinute;

    public TMP_Text dayTimeText;

    private bool timerIsActive;

    public static DayTimeSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DayTimeSystem dans la scène");
            return;
        }

        instance = this;
    }

    // met à jour le timer à chaque chargement
    private void Start()
    {
        UpdateDayTime();
    }

    void Update()
    {
        // chaque seconde le time augmente 
        if (!timerIsActive) StartCoroutine(NextSecond());

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

    //fonction qui permet met à jour le texte du timer
    void UpdateDayTime()
    {   
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
