using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingArea : MonoBehaviour
{
    public void PriceToPay(int price)
    {
        EconomySystem.instance.RemoveCoins(price);
    }

    // Fonction qui permet de changer de scénes en fonctions de la zone choisit
    public void BringToFishingArea(string areaToLoad)
    {
        SceneManager.LoadScene("FishingArea " + areaToLoad);
    }
}
