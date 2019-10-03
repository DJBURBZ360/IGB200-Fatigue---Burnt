using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour
{
    #region Variables
    private float interpolate = 0;
    [SerializeField] private float interpolationSpeed = 1;

    private GameObject sprite;
    private Vector2 originalPos,
                    originalScale;
    public GameObject exitPos;

    [Header("Generate Random Departure Time Between (in seconds)")]
    [SerializeField] private float[] departureTimeRange = new float[2];
    private float departureTime = 0f;
    private float currentDepartureDelay;

    [Header("Generate Random Arrival Time Between (in seconds)")]
    [SerializeField] private float[] arrivalTimeRange = new float[2];
    private float arrivalTime = 0f;
    private float currentArrivalTimeDelay;

    private bool isDeparting = false,
                 isArriving = true,
                 isParked = true,
                 hasTimeStamped = false,
                 isTimerPaused = false;

    private GameObject timerUI;
    private Slider sliderTimer;
    private Text textTimer;
    private Employee employee;
    private GameManager gameManager;
    #endregion

    #region Accessors    
    public bool IsParked
    {
        get { return isParked; }
    }

    public bool IsDeparting
    {
        get { return isDeparting; }
    }

    public bool IsArriving
    {
        get { return isArriving; }
    }

    public GameObject TimerUI
    {
        get { return timerUI; }
        set { timerUI = value; }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Make the sprite look like it's moving away.
    /// </summary>
    private void Depart()
    {
        if (interpolate < 1)
        {
            interpolate += interpolationSpeed * Time.deltaTime;
        }
        else if (interpolate > 1)
        {
            interpolate = 1;

            employee.ChangeSpriteColor();
            employee.GenerateRandomFatigueType();
        }        
    }

    /// <summary>
    /// Make the sprite look like it's closing in.
    /// </summary>
    private void Arrive()
    {
        if (interpolate > 0)
        {
            interpolate -= interpolationSpeed * Time.deltaTime;
        }
        else if (interpolate < 0)
        {
            interpolate = 0;
        }
    }

    private void GenerateDepartureTime()
    {
        if (!hasTimeStamped)
        {
            //Generate between numbers 5 & 10 if fatigue level is 3
            if (employee.CurrentFatigueLevel < 3)
                departureTime = Random.Range(departureTimeRange[0], departureTimeRange[1]);
            else
                departureTime = Random.Range(5, 10 + 1);

            currentDepartureDelay = departureTime + Time.time;

            employee.FatigueUI.SetActive(true);
            employee.ResetFatigueLevel();
            
            hasTimeStamped = true;
        }
    }

    private void GenerateArrivalTime()
    {
        if (!hasTimeStamped)
        {
            arrivalTime = Random.Range(arrivalTimeRange[0], arrivalTimeRange[1]);
            currentArrivalTimeDelay = arrivalTime + Time.time;

            gameManager.DoRNG(employee.CurrentFatigueLevel);
            gameManager.NumDrivers--;
            employee.FatigueUI.SetActive(false);

            hasTimeStamped = true;
        }
    }

    private void CheckCurrentState()
    {
        if (interpolate >= 1)
        {
            if (currentArrivalTimeDelay < Time.time)
            {
                {
                    isDeparting = false;
                    isArriving = true;
                    hasTimeStamped = false;
                }
            }
            else
            {
                hasTimeStamped = true;
            }
        }
        else if (interpolate <= 0)
        {
            if (currentDepartureDelay < Time.time)
            {
                isDeparting = true;
                isArriving = false;
                hasTimeStamped = false;
            }
            else
            {
                hasTimeStamped = true;
            }
        }
    }

    private void ManageTimer()
    {
        //ready up for departure
        if (!isDeparting &&
            isArriving)
        {
            GenerateDepartureTime();
            sliderTimer.value = 100 - Percentage.GetPercentage(currentDepartureDelay - Time.time, departureTime, sliderTimer.value);
            textTimer.text = string.Format("Departing in {0:0.00}s", currentDepartureDelay - Time.time);
        }

        //ready up for arrival
        else if (!isArriving &&
                 isDeparting)
        {
            GenerateArrivalTime();
            sliderTimer.value = Percentage.GetPercentage(currentArrivalTimeDelay - Time.time, arrivalTime, sliderTimer.value);
            textTimer.text = string.Format("Arriving in {0:0.00}s", currentArrivalTimeDelay - Time.time);
        }
    }
    #endregion

    #region Public Methods
    public void ChangeDriver()
    {
        isTimerPaused = true;

        //dispatch car
        isDeparting = true;
        isArriving = false;

        //reset employee
        gameManager.DoRNG(employee.CurrentFatigueLevel);
        employee.ResetFatigueLevel();        
        employee.GenerateRandomFatigueLevel();
        gameManager.NumDrivers--;
        if (!gameManager.IsTutorialActive) employee.FatigueUI.SetActive(false);

        //reset timers
        departureTime = 0;
        currentArrivalTimeDelay = 0;

        hasTimeStamped = false;
        isTimerPaused = false;
    }

    public void OverrideInterpolationValue(float value)
    {
        interpolate = value;
    }

    public void ForceArrive()
    {
        isArriving = true;
        isDeparting = false;
    }

    public void ForceDepart()
    {
        isArriving = false;
        isDeparting = true;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        departureTime = Random.Range(departureTimeRange[0], departureTimeRange[1]);

        employee = transform.GetChild(0).transform.GetComponent<Employee>();
        gameManager = GameObject.FindWithTag("Managers").GetComponent<GameManager>();
        sprite = this.gameObject;

        if (!gameManager.IsTutorialActive)
        {
            sliderTimer = timerUI.transform.GetChild(0).GetComponent<Slider>();
            textTimer = timerUI.transform.GetChild(1).GetComponent<Text>();
        }

        originalPos = this.gameObject.transform.position;
        originalScale = this.gameObject.transform.localScale;

        if (exitPos == null)
        {
            print(this.gameObject.name +
                  "\nPoints going from and to are not assigned, please assign a game object in the exitPos variable.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (exitPos != null)
        {
            //Imitate perspective movement
            sprite.transform.position = Vector2.Lerp(originalPos, exitPos.transform.position, interpolate);
            sprite.transform.localScale = Vector2.Lerp(originalScale, exitPos.transform.localScale, interpolate);
        }

        if (!gameManager.IsTutorialActive)
        {
            if (!isTimerPaused) ManageTimer();
            
            CheckCurrentState();
        }

        if (isDeparting) Depart();
        if (isArriving) Arrive();

        if (interpolate <= 0)
        {
            isParked = true;
        }
        else
        {
            isParked = false;
        }
    }
}
