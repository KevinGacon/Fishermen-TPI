/**********************************************
 * Projet : Fishermen
 * Nom du fichier : SelectFish.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Kevin Gacon
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectFish : MonoBehaviour
{
    // Variables

    //sert à savoir si oui ou non le poisson est sélectionné
    public bool isSelected = false;
    //stock l'objet du poisson
    public GameObject currentFish;

    //variables de text
    string dayTimeFishedText;
    string dayTimeNotFreshText;

    /// <summary>
    /// GetSelectFish est une fonction qui permet de sélectionner des poissons dans le shop
    /// </summary>
    public void GetSelectFish()
    {
        if (!isSelected)
        {
            isSelected = true;

            this.GetComponent<Image>().color = new Color(0.2075472f, 0.2075472f, 0.2075472f, 1f);
            this.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
            this.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.white;
            this.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.white;
            this.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.white;

            SellFishesSystem.instance.selectedFishToSell.Add(currentFish);

            SellFishesSystem.instance.UpdatePriceToSellText(currentFish.GetComponent<myFishSpecificData>().FishData.price);
        }
        else if (isSelected)
        {
            isSelected = false;

            this.GetComponent<Image>().color = Color.white;
            this.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
            this.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.black;
            this.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.black;
            this.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.black;

            SellFishesSystem.instance.selectedFishToSell.Remove(currentFish);

            SellFishesSystem.instance.UpdatePriceToSellText(-currentFish.GetComponent<myFishSpecificData>().FishData.price);
        }
    }

    /// <summary>
    /// SelectAFish est une fonction qui permet de sélectionner un seul poisson dans l'inventaire
    /// </summary>
    public void SelectAFish()
    {
        //affiche les infos du poissons et le bouton pour rêlacher lorsqu'on clique sur un poisson
        InventoryFishes.instance.releaseButton.SetActive(true);
        InventoryFishes.instance.infoFishText.SetActive(true);

        //stock tous les poissons de l'inventaires
        GameObject[] fishInInventory = GameObject.FindGameObjectsWithTag("FishObjectInventory");
        //recherche tous les poissons de l'inventaires
        foreach(GameObject afish in fishInInventory)
        {
            //affiche le fond de l'image en blanc
            afish.GetComponent<Image>().color = Color.white;

            // si le poisson est trop petit, il l'affiche en rouge
            if (!afish.GetComponent<SelectFish>().currentFish.GetComponent<myFishSpecificData>().haveRequiredSize)
            {
                afish.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            else
            {
                afish.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.black;
            }

            // si le poisson est trop jeûne, il l'affiche en rouge
            if (!afish.GetComponent<SelectFish>().currentFish.GetComponent<myFishSpecificData>().haveRequiredAge)
            {
                afish.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.red;
            }
            else
            {
                afish.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.black;
            }

            // si le poisson est plus frais il met le texte en rouge
            if (!afish.GetComponent<SelectFish>().currentFish.GetComponent<myFishSpecificData>().isFresh)
            {
                afish.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.red;
                afish.transform.GetChild(4).GetComponent<TMP_Text>().text = "Non";
            }
            else
            {
                afish.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.black;
                afish.transform.GetChild(4).GetComponent<TMP_Text>().text = "Oui";
            }

            //affiche le nom du poisson en blanc
            afish.transform.GetChild(1).GetComponent<TMP_Text>().color = Color.black;
        }

        //stock le poisson sélectionné actuel
        InventoryFishes.instance.selectedFish = currentFish;
        //met l'image de fond en noir du poisson sélectionné
        this.GetComponent<Image>().color = new Color(0.2075472f, 0.2075472f, 0.2075472f, 1f);

        // si le poisson est trop petit, il l'affiche en rouge
        if (!this.GetComponent<SelectFish>().currentFish.GetComponent<myFishSpecificData>().haveRequiredSize)
        {
            this.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }
        else
        {
            this.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.white;
        }

        // si le poisson est trop jeûne, il l'affiche en rouge
        if (!this.GetComponent<SelectFish>().currentFish.GetComponent<myFishSpecificData>().haveRequiredAge)
        {
            this.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.red;
        }
        else
        {
            this.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.white;
        }

        // si le poisson est plus frais il met le texte en rouge
        if (!this.GetComponent<SelectFish>().currentFish.GetComponent<myFishSpecificData>().isFresh)
        {
            this.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.red;
            this.transform.GetChild(4).GetComponent<TMP_Text>().text = "Non";
        }
        else
        {
            this.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.white;
            this.transform.GetChild(4).GetComponent<TMP_Text>().text = "Oui";
        }

        //affiche le nom du poisson en blanc
        this.transform.GetChild(1).GetComponent<TMP_Text>().color = Color.white;

        //met à jour le texte des infos des poissons
        UpdateFishInfo(currentFish);

    }

    /// <summary>
    /// UpdateFishInfo est une fonction qui met à jour le texte des infos des poissons en fonction du poisson inséré
    /// </summary>
    /// <param name="fish"></param>
    void UpdateFishInfo(GameObject fish)
    {
        if (fish.GetComponent<myFishSpecificData>().hourFished < 10 && fish.GetComponent<myFishSpecificData>().minuteFished < 10)
        {
            dayTimeFishedText = "0" + fish.GetComponent<myFishSpecificData>().hourFished + ":0" + fish.GetComponent<myFishSpecificData>().minuteFished;
        }
        else if (fish.GetComponent<myFishSpecificData>().hourFished < 10)
        {
            dayTimeFishedText = "0" + fish.GetComponent<myFishSpecificData>().hourFished + ":" + fish.GetComponent<myFishSpecificData>().minuteFished;
        }
        else if (fish.GetComponent<myFishSpecificData>().minuteFished < 10)
        {
            dayTimeFishedText = fish.GetComponent<myFishSpecificData>().hourFished + ":0" + fish.GetComponent<myFishSpecificData>().minuteFished;
        }
        else
        {
            dayTimeFishedText = fish.GetComponent<myFishSpecificData>().hourFished + ":" + fish.GetComponent<myFishSpecificData>().minuteFished;
        }

        if (fish.GetComponent<myFishSpecificData>().hourFished < 10 && fish.GetComponent<myFishSpecificData>().minuteFished < 10)
        {
            dayTimeNotFreshText = "0" + fish.GetComponent<myFishSpecificData>().hourFished + ":0" + fish.GetComponent<myFishSpecificData>().minuteFished;
        }
        else if (fish.GetComponent<myFishSpecificData>().hourFished < 10)
        {
            dayTimeNotFreshText = "0" + fish.GetComponent<myFishSpecificData>().hourFished + ":" + fish.GetComponent<myFishSpecificData>().minuteFished;
        }
        else if (fish.GetComponent<myFishSpecificData>().minuteFished < 10)
        {
            dayTimeNotFreshText = fish.GetComponent<myFishSpecificData>().hourFished + ":0" + fish.GetComponent<myFishSpecificData>().minuteFished;
        }
        else
        {
            dayTimeNotFreshText = fish.GetComponent<myFishSpecificData>().hourFished + ":" + fish.GetComponent<myFishSpecificData>().minuteFished;
        }

        InventoryFishes.instance.infoFishText.transform.GetChild(0).GetComponent<TMP_Text>().text =
            "Nom : " + fish.GetComponent<myFishSpecificData>().FishData.commonName +
            "\nNom latin : " + fish.GetComponent<myFishSpecificData>().FishData.latinName +
            "\nTaille : " + fish.GetComponent<myFishSpecificData>().currentSize + " cm" +
            "\nTaille minimum : " + fish.GetComponent<myFishSpecificData>().FishData.minSizeRequired + " cm" +
            "\nPlus petite taille : " + fish.GetComponent<myFishSpecificData>().FishData.minSize + " cm" +
            "\nPlus Grande taille : " + fish.GetComponent<myFishSpecificData>().FishData.maxSize + " cm" +
            "\nÂge : " + fish.GetComponent<myFishSpecificData>().currentAge + " années" +
            "\nÂge minimum : " + fish.GetComponent<myFishSpecificData>().FishData.minAgeRequired + " années" +
            "\nlongévité : " + fish.GetComponent<myFishSpecificData>().FishData.ageMax + " années" +
            "\nPêché le Jour " + fish.GetComponent<myFishSpecificData>().dayFished + " à " + dayTimeFishedText +
            "\nN'est plus frais le Jour " + fish.GetComponent<myFishSpecificData>().notFreshDay + " à " + dayTimeNotFreshText +
            "\nPrix de vente : " + fish.GetComponent<myFishSpecificData>().FishData.price + " $";
    }
}
