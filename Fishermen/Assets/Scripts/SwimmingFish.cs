using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingFish : MonoBehaviour
{
    public float speed;

    private bool randomTimerIsActive = false;

    private float randomPointToMoveX;
    private float randomPointToMoveY;

    private float currentXPosition;


    void Start()
    {
        //Fais apparaitre le poisson à un endroit aléatoire au lancement
        randomPointToMoveX = Random.Range(-8f, 8f);
        randomPointToMoveY = Random.Range(-4.5f, -1f); 

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

    void Update()
    {
        // change de point de direction tout les x temps
        if (!randomTimerIsActive) StartCoroutine(NewPoint());

        //déplace le poisson vers la direction du point 
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(randomPointToMoveX, randomPointToMoveY), speed * Time.deltaTime);
    }

    IEnumerator NewPoint()
    {
        randomTimerIsActive = true;

        //stock un nouveau point de déplacement
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


        //stock une vitesse aléatoire
        speed = Random.Range(1f, 2f);

        //temp de déplacement aléatoire entre chaque nouveu déplacement
        float randomTimerBtwPatrol;
        randomTimerBtwPatrol = Random.Range(5f, 10f);
        yield return new WaitForSeconds(randomTimerBtwPatrol);


        randomTimerIsActive = false;
    }
}
