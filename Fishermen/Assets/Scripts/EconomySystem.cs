/**********************************************
 * Projet : Fishermen
 * Nom du fichier : EconomySystem.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Kevin Gacon
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomySystem : MonoBehaviour
{
    // Variables

    //le texte de l'argent
    public TMP_Text currentCashDisplayText;
    //l'argent actel du joueur
    public static int currentCash;
    //voir l'argent actuel du joueur
    public int seeCurrentCash;

    //argent maximum du joueur
    public int maxCash = 999999;

    //stock une instance qui permet d'appeler le script sur n'importe quel script
    public static EconomySystem instance;

    /// <summary>
    /// Awake est appelé quand l'instance de script est chargée
    /// </summary>
    private void Awake()
    {
        //vériife si le script est unique sur la scène
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de EconomySystem dans la scène");
            return;
        }

        instance = this;
    }

    /// <summary>
    /// Start est appelé une fois avant update()
    /// </summary>
    void Start()
    {
        // met a jour l'affichage de l'argent au lancement du jeu
        UpdateCash();
    }

    //Touche de DEBUG pour ajouter ou retirer de l'argent
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

    /// <summary>
    /// AddCoins est une fonction qui ajoute de l'argent au joueur
    /// </summary>
    /// <param name="count"></param>
    public void AddCoins(int count)
    {
        //vérifie si l'argent actuel du joueur est inférieur ou égale à l'argent maximum
        if (currentCash <= maxCash)
        {
            currentCash += count;

            //vérifie si l'argent actuel du joueur est supérieur  à l'argent maximum
            if (currentCash > maxCash)
            {
                //met l'argent actuel au maximum
                currentCash = maxCash;
            }

            //met à jour l'affichage de l'argent
            UpdateCash();
        }
    }

    /// <summary>
    /// RemoveCoins est une fonction qui retire de l'argent au joueur
    /// </summary>
    /// <param name="count"></param>
    public void RemoveCoins(int count)
    {
        //vérifie si l'argent à retirer du joueur est inférieur ou égale à l'argent actuel du joueur
        if (count <= currentCash)
        {
            //retire le montant
            currentCash -= count;
            //met à jour l'affichage de l'argent
            UpdateCash();
        }
        else
        {
            //sinon il le met à 0 pour pas avoir de valeur négatif
            currentCash = 0;
        }
    }

    /// <summary>
    /// UpdateCash est une fonction qui met à jour l'affichegae de l'argent
    /// </summary>
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

    /// <summary>
    /// SeeCurrentCash est une fonction qui permet de vérifier l'argent actuel du joueur
    /// </summary>
    public void SeeCurrentCash()
    {
        seeCurrentCash = currentCash;

    }
}