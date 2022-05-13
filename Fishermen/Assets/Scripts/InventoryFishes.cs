using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Debug.LogWarning("Il y a plus d'une instance de InventoryFishes dans la sc�ne");
            return;
        }

        instance = this;
    }

    public void UpdateInventory()
    {
        //Supprime tous les elements pr�sent dans l'inventaire
        GameObject[] taggedObjectsToDelete = GameObject.FindGameObjectsWithTag("FishObjectInventory");

        foreach (GameObject objectToDelete in taggedObjectsToDelete)
        {
            Destroy(objectToDelete);
        }


        //cr�er un objet fish dans l'inventaire avec tous les �l�ments qui doivent �tre affich�
        foreach (FishData afish in myFishes)
        {
            GameObject thisFish = Instantiate(aFishPrefab, Parent.transform) as GameObject;

            thisFish.transform.GetChild(1).GetComponent<Image>().sprite = afish.image;
        }
    }
}
