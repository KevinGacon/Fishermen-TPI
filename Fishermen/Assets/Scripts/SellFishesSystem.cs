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

    public static SellFishesSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de SellFishesSystem dans la scène");
            return;
        }

        instance = this;

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

    void SelectFishToSell()
    { 
        
    }
}
