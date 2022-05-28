/**********************************************
 * Projet : Fishermen
 * Nom du fichier : DontDestroyOnLoad.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Kevin Gacon
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Variables
    static bool isActive;
    public GameObject objs;

    /// <summary>
    /// Awake est appel� quand l'instance de script est charg�e
    /// </summary>
    void Awake()
    {
        //permet de dupliquer les poissons de l'inventaires
        if (!isActive)
        {
            //permet de ne pas d�truire les objets des poissons entre chaque sc�ne
            DontDestroyOnLoad(objs);
            isActive = true;
        }
    }
}
