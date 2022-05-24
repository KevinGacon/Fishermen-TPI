using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUINaviguation : MonoBehaviour
{
    public bool isOpen=false;

    public GameObject inventoryInterface;


    public static GUINaviguation instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GUINaviguation dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        inventoryInterface.SetActive(false);
    }

    //fonction qui permet de retourner dans le menu de séléction de zones
    public void BringToSelectFishingArea()
    {
        SceneManager.LoadScene("MainGame");
    }

    // amène le joueur sur la scène du marché
    public void GoToMarket()
    {
        SceneManager.LoadScene("Market");
    }

    //ouvrir l'inventaire
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

    //fermer l'inventaire
    void CloseInventory()
    {
        isOpen = false;
        inventoryInterface.SetActive(false);
    }
}
