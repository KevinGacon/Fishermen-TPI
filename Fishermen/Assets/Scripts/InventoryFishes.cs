using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryFishes : MonoBehaviour
{
    public GameObject[] myFishes;

    public GameObject Parent;
    public GameObject aFishPrefab;

    public GameObject selectedFish;

    public GameObject infoFishText;
    public GameObject releaseButton;

    public static InventoryFishes instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de InventoryFishes dans la sc�ne");
            return;
        }

        instance = this;
    }

    public void UpdateInventory()
    {
        releaseButton.SetActive(false);
        infoFishText.SetActive(false);

        //Supprime tous les elements pr�sent dans l'inventaire
        GameObject[] taggedObjectsToDelete = GameObject.FindGameObjectsWithTag("FishObjectInventory");

        foreach (GameObject objectToDelete in taggedObjectsToDelete)
        {
            Destroy(objectToDelete);
        }

        //cr�er un objet fish dans l'inventaire avec tous les �l�ments qui doivent �tre affich�
        myFishes = GameObject.FindGameObjectsWithTag("myFish");

        foreach (GameObject afish in myFishes)
        {
            afish.GetComponent<myFishSpecificData>().UpdateFresh();

            GameObject thisFish = Instantiate(aFishPrefab, Parent.transform) as GameObject;

            thisFish.transform.GetChild(0).GetComponent<Image>().sprite = afish.GetComponent<myFishSpecificData>().FishData.image;
            thisFish.transform.GetChild(1).GetComponent<TMP_Text>().text = afish.GetComponent<myFishSpecificData>().FishData.commonName;

            thisFish.transform.GetChild(2).GetComponent<TMP_Text>().text = afish.GetComponent<myFishSpecificData>().currentSize.ToString() + " cm";
            thisFish.transform.GetChild(3).GetComponent<TMP_Text>().text = afish.GetComponent<myFishSpecificData>().currentAge.ToString() + " y";

            thisFish.GetComponent<SelectFish>().currentFish = afish;

            // si le poisson est trop petit il l'affiche en rouge
            if (!afish.GetComponent<myFishSpecificData>().haveRequiredSize)
            {
                thisFish.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }

            // si le poisson est trop petit il l'affiche en rouge
            if (!afish.GetComponent<myFishSpecificData>().haveRequiredAge)
            {
                thisFish.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.red;
            }

            // si le poisson est plus frais il met le texte en rouge
            if (!afish.GetComponent<myFishSpecificData>().isFresh)
            {
                thisFish.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.red;
                thisFish.transform.GetChild(4).GetComponent<TMP_Text>().text = "Non";
            }                                                     
        }
    }

    public void ReleaseAFish()
    {
        DestroyImmediate(selectedFish);

        UpdateInventory();
    }
}
