/**********************************************
 * Projet : Fishermen
 * Nom du fichier : SwimmingFish.cs
 * 
 * Date des derniers changements : 23.05.2022
 * Version : 1.0
 * Auteur : Kevin Gacon
 **********************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingFish : MonoBehaviour
{
    // Variables

    //peremet de d�finir la vitesse de d�placement des poissons
    public float speed;

    //sert � v�rifier si le timer pour NewPoint() est active
    private bool randomTimerIsActive = false;

    //stock des positions al�atoire
    private float randomPointToMoveX;
    private float randomPointToMoveY;

    //sert � v�rifier la posititon actuel du poisson
    private float currentXPosition;

    /// <summary>
    /// Start est appel� une fois avant update()
    /// </summary>
    void Start()
    {
        //Choisis un point al�atoire au lancement
        randomPointToMoveX = Random.Range(-8f, 8f);
        randomPointToMoveY = Random.Range(-4.5f, -1f); 

        //fait apparaitre le poisson sur ce point
        this.transform.position = new Vector2(randomPointToMoveX, randomPointToMoveY);

        //stock l'endroit actuel du poisson
        currentXPosition = this.transform.position.x;

        //tourne le poisson dans la bonne direction
        if (currentXPosition < randomPointToMoveX)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    /// <summary>
    /// Update est appel� une fois par mise � jour de trame
    /// </summary>
    void Update()
    {
        // change de point de direction tout les x temps 
        if (!randomTimerIsActive) StartCoroutine(NewPoint());

        //d�place le poisson vers la direction du point 
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(randomPointToMoveX, randomPointToMoveY), speed * Time.deltaTime);
    }

    /// <summary>
    /// NewPoint est une Coroutine de faire une pause entre chauqe changement de point de position
    /// </summary>
    IEnumerator NewPoint()
    {
        randomTimerIsActive = true;

        //stock un nouveau point de d�placement
        randomPointToMoveX = Random.Range(-8f, 8f);
        randomPointToMoveY = Random.Range(-4.5f, -1f);


        //tourne le poisson en fonction de l'ancien point
        currentXPosition = this.transform.position.x;

        if (currentXPosition < randomPointToMoveX)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        //stock une vitesse al�atoire
        speed = Random.Range(1f, 2f);

        //temp de d�placement al�atoire entre chaque nouveau point de d�placement
        float randomTimerBtwPatrol;
        randomTimerBtwPatrol = Random.Range(5f, 10f);
        yield return new WaitForSeconds(randomTimerBtwPatrol);


        randomTimerIsActive = false;
    }
}
