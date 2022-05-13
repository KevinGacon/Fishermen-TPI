using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUINaviguation : MonoBehaviour
{
    public bool isOpen=false;

    public GameObject inventoryInterface;

    private void Start()
    {
        inventoryInterface.SetActive(false);
    }

    private void Update()
    {
        
    }

    //fonction qui permet de retourner dans la zone de séléction de zone
    public void BringToSelectFishingArea()
    {
        SceneManager.LoadScene("MainGame");
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
