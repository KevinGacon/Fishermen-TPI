using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingGame : MonoBehaviour
{
    public List<FishData> FishesCanBeCaughtInThisArea;
    public GameObject whatIsFish;

    public Slider fishSlider;
    public Slider fishingRodSlider;

    public Slider completeFishingGauge;

    public GameObject fishHook;

    public GameObject fishingHUD;
    public GameObject startFishingButton;

    public GameObject returnButton;
    public GameObject inventoryButton;

    private bool randomTimerIsActive = false;
    float randomPosFish;

    bool gameIsLaunch;

    public static FishingGame instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de FishingGame dans la sc�ne");
            return;
        }

        instance = this;
    }

    void Update()
    {
        if (gameIsLaunch)
        {
            //trouve un nouveau point pour l'icon poisson
            if (!randomTimerIsActive) StartCoroutine(NewPoint());
            //d�place l'icone poisson vers ce nouveau point
            fishSlider.GetComponent<Slider>().value = Mathf.MoveTowards(fishSlider.GetComponent<Slider>().value, fishSlider.GetComponent<Slider>().value + randomPosFish, 0.6f * Time.deltaTime);
    
            //lorsque qu'on appui espace ou le click guauche de la souris notre icon monte
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
            {
                fishingRodSlider.GetComponent<Slider>().value += 0.005f;
            }

            //fais descendre la barre progressivement 
            fishingRodSlider.GetComponent<Slider>().value = Mathf.MoveTowards(fishingRodSlider.GetComponent<Slider>().value, 0, 0.4f * Time.deltaTime);

            //verifie si notre icone est sur l'icone du poisson
            OnTriggerVerif();


            //verrifie si le jauge de complete est � 100%
            if (completeFishingGauge.GetComponent<Slider>().value == 1 || Input.GetKey(KeyCode.O))
            {
                Destroy(GameObject.FindGameObjectWithTag("FishSwimming"));

                stopFishing();

                GameObject thisFish = Instantiate(whatIsFish, GameObject.FindGameObjectWithTag("myFishList").transform) as GameObject;
            }
        }
    }

    IEnumerator NewPoint()
    {
        randomTimerIsActive = true;

        randomPosFish = Random.Range(-0.2f, 0.2f);

        float randomTimerBtwPatrol;
        randomTimerBtwPatrol = Random.Range(0f, 0.5f);
        yield return new WaitForSeconds(randomTimerBtwPatrol);


        randomTimerIsActive = false;
    }

    //fonction qui verifie si notre icon est sur l'icone du poisson
    void OnTriggerVerif()
    {
        float fishingRodSliderValue = fishingRodSlider.GetComponent<Slider>().value;
        float fishSliderValue = fishSlider.GetComponent<Slider>().value;

        if (Mathf.Abs(fishingRodSliderValue - fishSliderValue) <= 0.17f)
        {
            fishHook.GetComponent<Image>().color = new Color(0.2814168f, 0.6415094f, 0.4121354f);
            completeFishingGauge.GetComponent<Slider>().value += 0.001f;
        }
        else
        {
            fishHook.GetComponent<Image>().color = new Color(0.3018868f, 0.3018868f, 0.3018868f);
            completeFishingGauge.GetComponent<Slider>().value -= 0.00025f;
        }
    }

    // fonction qui affiche le HUD et met les valeur � 0
    public void startFishing()
    {
        gameIsLaunch = true;

        startFishingButton.SetActive(false);
        fishingHUD.SetActive(true);
        returnButton.SetActive(false);
        inventoryButton.SetActive(false);

        completeFishingGauge.GetComponent<Slider>().value = 0;
        fishingRodSlider.GetComponent<Slider>().value = 0;

        fishSlider.GetComponent<Slider>().value = 1;
    }    


    //fonction qui arrete la fonctionnalit� de p�che
    public void stopFishing()
    {
        gameIsLaunch = false;

        startFishingButton.SetActive(true);
        fishingHUD.SetActive(false);
        returnButton.SetActive(true);
        inventoryButton.SetActive(true);
    }
}
