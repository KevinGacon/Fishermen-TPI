using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUINaviguation : MonoBehaviour
{
    private bool isOpen=false;

    public GameObject inventoryInterface;

    private void Start()
    {
        inventoryInterface.SetActive(false);
    }

    private void Update()
    {
        //if ()
    }

    //fonction qui permet de retourner dans la zone de s�l�ction de zone
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

    void CloseInventory()
    {
        isOpen = false;
        inventoryInterface.SetActive(false);
    }
}
