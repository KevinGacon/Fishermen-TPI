using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomySystem : MonoBehaviour
{
    public TMP_Text currentCashDisplayText;
    public static int currentCash;
    public int seeCurrentCash;

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
        // met a jour l'affichage de l'argent au lancement du jeu
        UpdateCash();
    }

    //Touche de debug pour ajouter ou retirer de l'argent
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

    //fonction qui ajoute de l'argent
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

    //fonction qui retire de l'argent
    public void RemoveCoins(int count)
    {

        if (count <= currentCash)
        {
            currentCash -= count;
            UpdateCash();
        }
    }

    //fonction qui met à jour l'affichegae de l'argent
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

    public void SeeCurrentCash()
    {
        seeCurrentCash = currentCash;

    }
}