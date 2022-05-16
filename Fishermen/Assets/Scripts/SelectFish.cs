using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectFish : MonoBehaviour
{
    public bool isSelected = false;
    public GameObject currentFish;

    public void GetSelectFish()
    {
        if (!isSelected)
        {
            isSelected = true;

            this.GetComponent<Image>().color = new Color(0.2075472f, 0.2075472f, 0.2075472f, 1f);
            this.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
            this.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.white;
            this.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.white;
            this.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.white;

            SellFishesSystem.instance.selectedFishToSell.Add(currentFish);
        }
        else if (isSelected)
        {
            isSelected = false;

            this.GetComponent<Image>().color = Color.white;
            this.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
            this.transform.GetChild(2).GetComponent<TMP_Text>().color = Color.black;
            this.transform.GetChild(3).GetComponent<TMP_Text>().color = Color.black;
            this.transform.GetChild(4).GetComponent<TMP_Text>().color = Color.black;

            SellFishesSystem.instance.selectedFishToSell.Remove(currentFish);
        }
    }
}
