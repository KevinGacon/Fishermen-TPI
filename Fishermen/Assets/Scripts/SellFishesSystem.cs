using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellFishesSystem : MonoBehaviour
{
    public List<GameObject> selectedFishToSell;
    public GameObject[] myFishes;

    public GameObject Parent;
    public GameObject aFishPrefab;

    public int priceToSell = 0;
    public TMP_Text priceToSellText;

    public int fine = 0;
    public TMP_Text fineText;
    public GameObject fineMenu;

    public static SellFishesSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de SellFishesSystem dans la scène");
            return;
        }

        instance = this;

        //désactive l'affichage de l'amende
        fineMenu.SetActive(false);

        //créer un objet fish dans l'inventaire avec tous les éléments qui doivent être affiché
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

    public void SellFishes()
    {
        foreach (GameObject fish in selectedFishToSell)
        {
            if (!fish.GetComponent<myFishSpecificData>().haveRequiredSize || !fish.GetComponent<myFishSpecificData>().haveRequiredAge || !fish.GetComponent<myFishSpecificData>().isFresh)
            {
                fine = fine + 1000;
            }

            Destroy(fish);
        }

        if (fine > 0)
        {
            //active l'affichage de l'amende
            fineMenu.SetActive(true);

            fineText.text = "Des poissons illégaux ont été vendu. Vous recevez une amende de " + fine + " $ .";
        }
        else
        {
        EconomySystem.instance.AddCoins(priceToSell);

        GUINaviguation.instance.GoToMarket();
        }
    }

    public void CloseFineMenu()
    {
        //désactive l'affichage de l'amende
        fineMenu.SetActive(false);

        EconomySystem.instance.AddCoins(priceToSell);

        EconomySystem.instance.RemoveCoins(fine);

        GUINaviguation.instance.GoToMarket();
    }
}
