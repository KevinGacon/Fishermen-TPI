using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUINaviguation : MonoBehaviour
{
    //fonction qui permet de retourner dans la zone de séléction de zone
    public void BringToSelectFishingArea()
    {
        SceneManager.LoadScene("MainGame");
    }
}
