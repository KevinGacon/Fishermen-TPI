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
        randomPointToMoveX = Random.Range(-8f, 8f);
        randomPointToMoveY = Random.Range(-4.5f, -1f); 

        this.transform.position = new Vector2(randomPointToMoveX, randomPointToMoveY);

        currentXPosition = this.transform.position.x;

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
        if (!randomTimerIsActive) StartCoroutine(NewPoint());

        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(randomPointToMoveX, randomPointToMoveY), speed * Time.deltaTime);
    }

    IEnumerator NewPoint()
    {
        randomTimerIsActive = true;


        randomPointToMoveX = Random.Range(-8f, 8f);
        randomPointToMoveY = Random.Range(-4.5f, -1f);

        currentXPosition = this.transform.position.x;

        if (currentXPosition < randomPointToMoveX)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }



        speed = Random.Range(1f, 5f);

        float randomTimerBtwPatrol;
        randomTimerBtwPatrol = Random.Range(1f, 5f);
        yield return new WaitForSeconds(randomTimerBtwPatrol);


        randomTimerIsActive = false;
    }
}
