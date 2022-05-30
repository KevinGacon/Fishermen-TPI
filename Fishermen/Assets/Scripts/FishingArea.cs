/**********************************************
 * Projet : Fishermen
 * Nom du fichier : FishingArea.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Kevin Gacon
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FishingArea : MonoBehaviour
{
    // Variables

    //prix d'une zone
    private int price = 0;

    //voit si l'interface est ouvert ou non
    private bool isOpen = false;

    //l'objet de l'interface de confirmation de paiement
    public GameObject confirmPayInterface;
    //le texte de message de paiement
    public TMP_Text confirmPayText;

    //l'objet de l'interface de refusement de paiement
    public GameObject refusePaiementInterface;

    //la zone a charger
    private string areaToLoad;

    /// <summary>
    /// Start est appelé une fois avant update()
    /// </summary>
    private void Start()
    {
        CloseConfirmPayInterface();
    }

    /// <summary>
    /// PriceToPay est une fonction qui permet d'insigner le prix d'une zone
    /// </summary>
    public void PriceToPay(int price)
    {
        this.price = price;
    }

    /// <summary>
    /// BringToFishingArea est une fonction qui permet de changer de scénes en fonctions de la zone choisit
    /// </summary>
    public void BringToFishingArea(string areaToLoad)
    {
        //vérifie si la zone est gratuite ou non
        if (price <= 0)
        {
            //charge la zone de pêche
            SceneManager.LoadScene("FishingArea " + areaToLoad);
        }
        else
        {
            this.areaToLoad = areaToLoad;

            confirmPayText.text = "Voulez-vous payer " + price + " $ pour accéder à la zone ?";

            OpenConfirmPayInterface();
        }
    }

    /// <summary>
    /// OpenConfirmPayInterface est une fonction qui ouvre le menu de confirmation de paymenet
    /// </summary>
    public void OpenConfirmPayInterface()
    {
        isOpen = true;
        confirmPayInterface.SetActive(true);
    }

    /// <summary>
    /// CloseConfirmPayInterface est une fonction qui ferme le menu de confirmation de paymenet
    /// </summary>
    public void CloseConfirmPayInterface()
    {
        isOpen = false;
        confirmPayInterface.SetActive(false);
    }

    /// <summary>
    /// OpenRefusePaiementInterface est une fonction qui ouvre le menu de confirmation de paymenet
    /// </summary>
    public void OpenRefusePaiementInterface()
    {
        refusePaiementInterface.SetActive(true);
    }

    /// <summary>
    /// OpenRefusePaiementInterface est une fonction qui ferme le menu de confirmation de paymenet
    /// </summary>
    public void CloseRefusePaiementInterface()
    {
        refusePaiementInterface.SetActive(false);

        CloseConfirmPayInterface();
    }

    /// <summary>
    /// OpenRefusePaiementInterface est une fonction confirme le paiement
    /// </summary>
    public void ConfirmPayment()
    {
        EconomySystem.instance.SeeCurrentCash();

        //vérifie si le joueur possède l'argent
        if (EconomySystem.instance.seeCurrentCash < price)
        {
            OpenRefusePaiementInterface();
        }
        else
        {
            //retire l'argent au joueur
            EconomySystem.instance.RemoveCoins(price);
            //charge la zone de pêche
            SceneManager.LoadScene("FishingArea " + areaToLoad);
        }
    }
}
