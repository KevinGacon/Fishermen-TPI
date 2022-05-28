/**********************************************
 * Projet : Fishermen
 * Nom du fichier : InventoryFishes.cs
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

public class InventoryFishes : MonoBehaviour
{
    // Variables

    //sert � stocker tous les poissons de l'inventaires
    public GameObject[] myFishes;

    //l'objet parent o� les boutons des diff�rents poissons doivent �tre g�n�r�
    public GameObject Parent;
    //le prefab d'un bouton poisson
    public GameObject aFishPrefab;

    //l'objet du poisson s�lectionn�
    public GameObject selectedFish;

    //l'objet de l'info du poisson
    public GameObject infoFishText;
    //l'objet du bouton pour rel�cher le poisson
    public GameObject releaseButton;

    //stock une instance qui permet d'appeler le script sur n'importe quel script
    public static InventoryFishes instance;

    /// <summary>
    /// Awake est appel� quand l'instance de script est charg�e
    /// </summary>
    private void Awake()
    {
        //v�riife si le script est unique sur la sc�ne
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de InventoryFishes dans la sc�ne");
            return;
        }

        instance = this;
    }

    /// <summary>
    /// UpdateInventory est une fonction qui update l'affichage de l'inventaire
    /// </summary>
    public void UpdateInventory()
    {
        //d�sactive l'affichage du bouton et du texte
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

    /// <summary>
    /// ReleaseAFish est une fonction qui permet de rel�cher un poisson
    /// </summary>
    public void ReleaseAFish()
    {
        DestroyImmediate(selectedFish);

        UpdateInventory();
    }
}
