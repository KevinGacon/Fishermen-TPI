using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomySystem : MonoBehaviour
{
    public TMP_Text currentCashDisplayText;
    public static int currentCash = 500;

    public int maxCash = 999999;

    public static EconomySystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de EconomySystem dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        UpdateCash();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            AddCoins(20);
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            RemoveCoins(20);
        }
    }

    public void AddCoins(int count)
    {
        if (currentCash <= maxCash)
        {
            currentCash += count;

            if (currentCash > maxCash)
            {
                currentCash = maxCash;
            }

            UpdateCash();
        }
    }

    public void RemoveCoins(int count)
    {
        if (count <= currentCash)
        {
            currentCash -= count;
            UpdateCash();
        }
    }

    void UpdateCash()
    {
        if (currentCash.ToString().Length > 3)
        {
            currentCashDisplayText.text = currentCash.ToString().Insert(currentCash.ToString().Length - 3, "'") + " $";
        }
        else
        {
            currentCashDisplayText.text = currentCash.ToString() + " $";
        }
    }
}