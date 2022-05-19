using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FishingArea : MonoBehaviour
{
    private int price = 0;

    private bool isOpen = false;

    public GameObject confirmPayInterface;
    public TMP_Text confirmPayText;

    public GameObject refusePaiementInterface;

    private string areaToLoad;

    private void Start()
    {
        CloseConfirmPayInterface();
    }

    // fonction qui permet d'insigner le prix d'une zone
    public void PriceToPay(int price)
    {
        this.price = price;
    }

    // Fonction qui permet de changer de scénes en fonctions de la zone choisit
    public void BringToFishingArea(string areaToLoad)
    {
        if (price <= 0)
        {
            SceneManager.LoadScene("FishingArea " + areaToLoad);
        }
        else
        {
            this.areaToLoad = areaToLoad;

            confirmPayText.text = "Voulez-vous payer " + price + " $ pour accéder à la zone ?";

            OpenConfirmPayInterface();
        }
    }

    //ouvre le menu de confirmation de paymenet
    public void OpenConfirmPayInterface()
    {
        isOpen = true;
        confirmPayInterface.SetActive(true);
    }

    //ferme le menu de confirmation de paymenet
    public void CloseConfirmPayInterface()
    {
        isOpen = false;
        confirmPayInterface.SetActive(false);
    }

    //ouvre le menu de confirmation de paymenet
    public void OpenRefusePaiementInterface()
    {
        refusePaiementInterface.SetActive(true);
    }

    //ferme le menu de confirmation de paymenet
    public void CloseRefusePaiementInterface()
    {
        refusePaiementInterface.SetActive(false);

        CloseConfirmPayInterface();
    }

    //confirme le payement
    public void ConfirmPayment()
    {
        EconomySystem.instance.SeeCurrentCash();

        if (EconomySystem.instance.seeCurrentCash < price)
        {
            OpenRefusePaiementInterface();
        }
        else
        {
            EconomySystem.instance.RemoveCoins(price);
            SceneManager.LoadScene("FishingArea " + areaToLoad);
        }
    }
}
