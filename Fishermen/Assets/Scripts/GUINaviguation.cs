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
    /// Awake est appel� quand l'instance de script est charg�e
    /// </summary>
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GUINaviguation dans la sc�ne");
            return;
        }

        instance = this;
    }

    /// <summary>
    /// Start est appel� une fois avant update()
    /// </summary>
    private void Start()
    {
        //d�sactive l'affichage de l'interface de l'inventaire au lancement du script apr s�curit�
        inventoryInterface.SetActive(false);
    }

    /// <summary>
    /// BringToSelectFishingArea est une fonction qui am�ne le joueur dans le menu de s�l�ction de zones
    /// </summary>
    public void BringToSelectFishingArea()
    {
        SceneManager.LoadScene("MainGame");
    }

    /// <summary>
    /// GoToMarket est une fonction qui am�ne le joueur sur la sc�ne du march�
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
