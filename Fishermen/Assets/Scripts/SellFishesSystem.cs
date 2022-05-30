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

    //sert � stocker dans une liste tous les poissons que le joueur a s�lectionn�
    public List<GameObject> selectedFishToSell;
    //sert � stocker tous les poissons de l'inventaires
    public GameObject[] myFishes;

    //l'objet parent o� les boutons des diff�rents poissons doivent �tre g�n�r�
    public GameObject Parent;
    //le prefab d'un bouton poisson
    public GameObject aFishPrefab;

    //variable du prix de revente
    public int priceToSell = 0;
    //le texte de prix de vente qui devra �tre affich�
    public TMP_Text priceToSellText;

    //variable du prix de l'amende
    public int fine = 0;
    //le texte de prix de l'amende qui devra �tre affich�
    public TMP_Text fineText;
    //l'objet du message de l'amende
    public GameObject fineMenu;

    //stock une instance qui permet d'appeler le script sur n'importe quel script
    public static SellFishesSystem instance;

    /// <summary>
    /// Awake est appel� quand l'instance de script est charg�e
    /// </summary>
    private void Awake()
    {
        //v�riife si le script est unique sur la sc�ne
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de SellFishesSystem dans la sc�ne");
            return;
        }

        instance = this;

        //d�sactive l'affichage de l'amende
        fineMenu.SetActive(false);

        //cr�er un objet fish dans l'inventaire su shop avec tous les �l�ments qui doivent �tre affich�
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
    /// UpdatePriceToSellText est une fonction qui permet de mettre � jour l'affichage du prix
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
    /// SellFishes est une fonction qui permet de vendre tous les poissons s�l�ctionn�
    /// </summary>
    public void SellFishes()
    {
        //recherche tous les poissons qui ont �t� s�l�ctionn�
        foreach (GameObject fish in selectedFishToSell)
        {
            //v�rifie si le poisson est l�gale
            if (!fish.GetComponent<myFishSpecificData>().haveRequiredSize || !fish.GetComponent<myFishSpecificData>().haveRequiredAge || !fish.GetComponent<myFishSpecificData>().isFresh)
            {
                //ajoute +1000 � l'amende si le poisson n'est pas l�gale
                fine = fine + 1000;
            }

            //retire le poissons de l'inventaire en d�truisant l'objet
            Destroy(fish);
        }

        //v�rifie si l'amende est plus grand que 0
        if (fine > 0)
        {
            //active l'affichage de l'amende
            fineMenu.SetActive(true);

            //met � jour le texte
            fineText.text = "Des poissons ill�gaux ont �t� vendu. Vous recevez une amende de " + fine + " $ .";
        }
        else //sinon on affiche pas l'amende
        {
        //ajoute le prix des poissons vendu au joueur
        EconomySystem.instance.AddCoins(priceToSell);

        //met � jour le menu du shop et l'inventaire du shop
        GUINaviguation.instance.GoToMarket();
        }
    }

    /// <summary>
    /// CloseFineMenu est une fonction qui permet de fermer le menu de l'amende
    /// </summary>
    public void CloseFineMenu()
    {
        //d�sactive l'affichage de l'amende
        fineMenu.SetActive(false);

        //ajoute le prix des poissons vendu au joueur
        EconomySystem.instance.AddCoins(priceToSell);

        //fait payer l'amende au joueur
        EconomySystem.instance.RemoveCoins(fine);

        //met � jour le menu du shop et l'inventaire du shop
        GUINaviguation.instance.GoToMarket();
    }
}
