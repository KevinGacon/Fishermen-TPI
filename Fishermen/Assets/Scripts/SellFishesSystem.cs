/**********************************************
 * Projet : Fishermen
 * Nom du fichier : SellFishesSystem.cs
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

public class SellFishesSystem : MonoBehaviour
{
    // Variables

    //sert à stocker dans une liste tous les poissons que le joueur a sélectionné
    public List<GameObject> selectedFishToSell;
    //sert à stocker tous les poissons de l'inventaires
    public GameObject[] myFishes;

    //l'objet parent où les boutons des différents poissons doivent être généré
    public GameObject Parent;
    //le prefab d'un bouton poisson
    public GameObject aFishPrefab;

    //variable du prix de revente
    public int priceToSell = 0;
    //le texte de prix de vente qui devra être affiché
    public TMP_Text priceToSellText;

    //variable du prix de l'amende
    public int fine = 0;
    //le texte de prix de l'amende qui devra être affiché
    public TMP_Text fineText;
    //l'objet du message de l'amende
    public GameObject fineMenu;

    //stock une instance qui permet d'appeler le script sur n'importe quel script
    public static SellFishesSystem instance;

    /// <summary>
    /// Awake est appelé quand l'instance de script est chargée
    /// </summary>
    private void Awake()
    {
        //vériife si le script est unique sur la scène
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de SellFishesSystem dans la scène");
            return;
        }

        instance = this;

        //désactive l'affichage de l'amende
        fineMenu.SetActive(false);

        //créer un objet fish dans l'inventaire su shop avec tous les éléments qui doivent être affiché
        myFishes = GameObject.FindGameObjectsWithTag("myFish");

        foreach (GameObject afish in myFishes)
        {
            GameObject thisFish = Instantiate(aFishPrefab, Parent.transform) as GameObject;

            thisFish.transform.GetChild(0).GetComponent<TMP_Text>().text = afish.GetComponent<myFishSpecificData>().FishData.commonName;
            thisFish.transform.GetChild(1).GetComponent<Image>().sprite = afish.GetComponent<myFishSpecificData>().FishData.image;
            thisFish.transform.GetChild(2).GetComponent<TMP_Text>().text = afish.GetComponent<myFishSpecificData>().currentSize.ToString() + " cm";
            thisFish.transform.GetChild(3).GetComponent<TMP_Text>().text = afish.GetComponent<myFishSpecificData>().currentAge.ToString() + " y";
            thisFish.transform.GetChild(4).GetComponent<TMP_Text>().text = afish.GetComponent<myFishSpecificData>().FishData.price.ToString() + " $";
            thisFish.transform.GetComponent<SelectFish>().currentFish = afish;
        }
    }

    /// <summary>
    /// UpdatePriceToSellText est une fonction qui permet de mettre à jour l'affichage du prix
    /// </summary>
    /// <param name="price"></param>
    public void UpdatePriceToSellText(int price)
    {
        priceToSell += price;

        if (priceToSell.ToString().Length > 3)
        {
            priceToSellText.text = "Somme : " + priceToSell.ToString().Insert(priceToSell.ToString().Length - 3, "'") + " $";
        }
        else
        {
            priceToSellText.text = "Somme : " + priceToSell.ToString() + " $";
        }
    }

    /// <summary>
    /// SellFishes est une fonction qui permet de vendre tous les poissons séléctionné
    /// </summary>
    public void SellFishes()
    {
        //recherche tous les poissons qui ont été séléctionné
        foreach (GameObject fish in selectedFishToSell)
        {
            //vérifie si le poisson est légale
            if (!fish.GetComponent<myFishSpecificData>().haveRequiredSize || !fish.GetComponent<myFishSpecificData>().haveRequiredAge || !fish.GetComponent<myFishSpecificData>().isFresh)
            {
                //ajoute +1000 à l'amende si le poisson n'est pas légale
                fine = fine + 1000;
            }

            //retire le poissons de l'inventaire en détruisant l'objet
            Destroy(fish);
        }

        //vérifie si l'amende est plus grand que 0
        if (fine > 0)
        {
            //active l'affichage de l'amende
            fineMenu.SetActive(true);

            //met à jour le texte
            fineText.text = "Des poissons illégaux ont été vendu. Vous recevez une amende de " + fine + " $ .";
        }
        else //sinon on affiche pas l'amende
        {
        //ajoute le prix des poissons vendu au joueur
        EconomySystem.instance.AddCoins(priceToSell);

        //met à jour le menu du shop et l'inventaire du shop
        GUINaviguation.instance.GoToMarket();
        }
    }

    /// <summary>
    /// CloseFineMenu est une fonction qui permet de fermer le menu de l'amende
    /// </summary>
    public void CloseFineMenu()
    {
        //désactive l'affichage de l'amende
        fineMenu.SetActive(false);

        //ajoute le prix des poissons vendu au joueur
        EconomySystem.instance.AddCoins(priceToSell);

        //fait payer l'amende au joueur
        EconomySystem.instance.RemoveCoins(fine);

        //met à jour le menu du shop et l'inventaire du shop
        GUINaviguation.instance.GoToMarket();
    }
}
