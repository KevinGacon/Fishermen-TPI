using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
