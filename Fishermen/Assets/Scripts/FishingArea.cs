using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingArea : MonoBehaviour
{
    private int price = 0;

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
            EconomySystem.instance.SeeCurrentCash();

            if (EconomySystem.instance.seeCurrentCash < price)
            {

            }
            else
            {
                EconomySystem.instance.RemoveCoins(price);
                SceneManager.LoadScene("FishingArea " + areaToLoad);
            }
        }
    }
}
