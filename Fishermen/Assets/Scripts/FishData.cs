/**********************************************
 * Projet : Fishermen
 * Nom du fichier : FishData.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Kevin Gacon
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//permet de cr�er un nouveau scriptableobject de poisson sur unity
[CreateAssetMenu(fileName = "FishData", menuName = "New Fish")]
public class FishData : ScriptableObject
{
    //scriptableobject d'un poisson
    public Sprite image;
    public string commonName;
    public string latinName;
    public float minSize;
    public float maxSize;
    public float minSizeRequired;
    public int ageMax;
    public int minAgeRequired;
    public int price;
}
