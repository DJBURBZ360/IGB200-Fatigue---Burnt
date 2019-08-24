using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Employee : MonoBehaviour
{
    #region Variables
    [SerializeField]
    [Header("Generate Random Fatigue Time Between (in seconds)")]
    private float[] randomNumRange = new float[2];

    private int currentFatigueLevel = 0;
    [SerializeField] private int maxFatigueLevel = 0;

    private float currentFatigueRate = 0,
                  currentFatigueDelay = 0;

    private bool isChecking = true;

    private GameObject fatigueUI;
    private Slider fatigueSlider;
    private Text fatigueText;
    private Image fillColor;

    private GameManager gameManager;
    private CarManager car;
    #endregion

    #region Accessors
    public CarManager Car
    {
        get { return car; }
    }

    public GameObject FatigueUI
    {
        get { return fatigueUI; }
        set { fatigueUI = value; }
    }
    #endregion

    #region Methods
    private void IncreaseFatigueLevel()
    {
        //countdown then incease fatigue level
        if (currentFatigueDelay < Time.time)
        {
            //randomly generate a number
            currentFatigueRate = Random.Range(randomNumRange[0], randomNumRange[1]);
            currentFatigueDelay = currentFatigueRate + Time.time;
            
            //limits the current fatigue level
            int temp = ++currentFatigueLevel;
            if (temp > maxFatigueLevel)
            {
                temp = maxFatigueLevel;
            }
            currentFatigueLevel = temp;
            
            //reset slider fill then change color
            fatigueSlider.value = 0;
            fatigueSlider.maxValue = currentFatigueRate;
            fillColor.color = gameManager.fatigueLevelColors[currentFatigueLevel];
        }
    }

    private void DecreaseFatigueLevel()
    {
        //decrease fatigue level when given the appropriate snack
        currentFatigueLevel = currentFatigueLevel < 0 ? 0 : --currentFatigueLevel;
    }

    private void CheckForSnack(Collider2D collision)
    {
        Snack givenSnack = collision.GetComponent<Snack>();
        switch (currentFatigueLevel)
        {
            case 1:
                if (givenSnack.SnackLevel == Snack.SnackLevels.level1)
                {
                    ResetFatigueLevel();
                }
                else
                {
                    if (givenSnack.SnackLevel > Snack.SnackLevels.level1)
                    {
                        //no effects given
                    }
                }
                break;

            case 2:
                if (givenSnack.SnackLevel == Snack.SnackLevels.level2)
                {
                    ResetFatigueLevel();
                }
                else
                {
                    if (givenSnack.SnackLevel < Snack.SnackLevels.level2)
                    {
                        //give 25% effectiveness
                        float temp = currentFatigueDelay - Time.time < 0 ? 0 : currentFatigueDelay - Time.time;
                        temp = (temp + (currentFatigueRate * 0.25f)) + Time.time;
                        currentFatigueDelay = temp;
                    }
                    else if (givenSnack.SnackLevel > Snack.SnackLevels.level2)
                    {
                        //no effects given 
                    }
                }
                break;

            case 3:
                if (givenSnack.SnackLevel == Snack.SnackLevels.level3)
                {
                    ResetFatigueLevel();
                }
                else
                {
                    if (givenSnack.SnackLevel < Snack.SnackLevels.level3)
                    {
                        //give 25% effectiveness
                        float temp = currentFatigueDelay - Time.time < 0 ? 0 : currentFatigueDelay - Time.time;
                        temp = (temp + (currentFatigueRate * 0.25f)) + Time.time;
                        currentFatigueDelay = temp;
                    }
                }
                break;

            default:Debug.Log(this.gameObject.name + "'s fatigue level: " + currentFatigueLevel);
                break;
        }
    }

    private void CheckFatigueLevel()
    {
        if (currentFatigueLevel >= 4)
        {
            //fire employee
            //destroy gameobject
        }
    }

    /// <summary>
    /// Resets fatigue level and fatigue timer.
    /// </summary>
    private void ResetFatigueLevel()
    {
        currentFatigueLevel = 0;
        currentFatigueRate = Random.Range(randomNumRange[0], randomNumRange[1]);
        currentFatigueDelay = currentFatigueRate + Time.time;
        fillColor.color = gameManager.fatigueLevelColors[0];
    }

    /// <summary>
    /// Checks the current status upon departure
    /// </summary>
    private void CheckStatus()
    {
        //if departed at fatigue lvl 3
        //then add fatigued employee count to game manager

        if (car.IsParked)
        {
            isChecking = true;
        }
        else
        {
            if (isChecking)
            {
                if (currentFatigueLevel > 2)
                    gameManager.NumFatiguedDrivers++;
            }
            isChecking = false;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();

        //UI setup
        fatigueSlider = fatigueUI.transform.GetChild(0).GetComponent<Slider>();
        fatigueText = fatigueUI.transform.GetChild(1).GetComponent<Text>();
        fillColor = fatigueSlider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
        fillColor.color = gameManager.fatigueLevelColors[currentFatigueLevel];

        //initialize fatigue timer
        currentFatigueRate = Random.Range(randomNumRange[0], randomNumRange[1]);
        currentFatigueDelay = currentFatigueRate + Time.time;
        fatigueSlider.maxValue = currentFatigueRate;

        car = this.gameObject.transform.parent.GetComponent<CarManager>();
    }

    // Update is called once per frame
    void Update()
    {
        fatigueSlider.value = currentFatigueRate - (currentFatigueDelay - Time.time);
        fatigueText.text = string.Format("Fatigue Level: {0}", currentFatigueLevel);

        //increase only when below max fatigue level
        if (currentFatigueLevel < maxFatigueLevel)
        {
            IncreaseFatigueLevel();
        }
        CheckFatigueLevel();
        CheckStatus();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //snacks will be prefabs with snack script attached to ID snack level
        //instanciate snack when clicked on snack icon from UI
        if (collision.GetComponent<Snack>() != null)
        {
            CheckForSnack(collision);
            Player.instance.HasSnack = false;
            Snack.numInstance--;
            Destroy(collision.gameObject);
        }
    }
}
