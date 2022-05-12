using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFishes : MonoBehaviour
{
    public List<FishData> myFishes;

    public GameObject Parent;
    public GameObject aFishPrefab;

    public static InventoryFishes instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de InventoryFishes dans la scène");
            return;
        }

        instance = this;
    }

    public void UpdateInventory()
    {
        foreach (FishData afish in myFishes)
        {
            GameObject thisDog = Instantiate(aFishPrefab, Parent.transform) as GameObject;
        }
    }
}
