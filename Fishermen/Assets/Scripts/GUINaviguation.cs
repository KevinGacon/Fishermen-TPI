/**********************************************
 * Projet : Fishermen
 * Nom du fichier : GUINaviguation.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Kevin Gacon
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUINaviguation : MonoBehaviour
{
    // Variables

    //si l'interface est ouvert ou pas
    public bool isOpen=false;

    //l'objet de l'interface
    public GameObject inventoryInterface;

    //stock une instance qui permet d'appeler le script sur n'importe quel script
    public static GUINaviguation instance;

    /// <summary>
    /// Awake est appelé quand l'instance de script est chargée
    /// </summary>
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GUINaviguation dans la scène");
            return;
        }

        instance = this;
    }

    /// <summary>
    /// Start est appelé une fois avant update()
    /// </summary>
    private void Start()
    {
        //désactive l'affichage de l'interface de l'inventaire au lancement du script apr sécurité
        inventoryInterface.SetActive(false);
    }

    /// <summary>
    /// BringToSelectFishingArea est une fonction qui amène le joueur dans le menu de séléction de zones
    /// </summary>
    public void BringToSelectFishingArea()
    {
        SceneManager.LoadScene("MainGame");
    }

    /// <summary>
    /// GoToMarket est une fonction qui amène le joueur sur la scène du marché
    /// </summary>
    public void GoToMarket()
    {
        SceneManager.LoadScene("Market");
    }

    /// <summary>
    /// OpenInventory est une fonction qui ouvre l'interface de l'inventaire s'il n'est pas ouvert
    /// </summary>
    public void OpenInventory()
    {
        if (!isOpen)
        { 
            isOpen = true;
            inventoryInterface.SetActive(true);
            InventoryFishes.instance.UpdateInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    /// <summary>
    /// CloseInventory est une fonction qui ferme l'interface de l'inventaire
    /// </summary>
    void CloseInventory()
    {
        isOpen = false;
        inventoryInterface.SetActive(false);
    }
}
