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
                 isParked = true;

    private GameObject timerUI;
    private Slider sliderTimer;
    private Text textTimer;
    #endregion

    #region Accessors    
    public bool IsParked
    {
        get { return isParked; }
    }

    public GameObject TimerUI
    {
        get { return timerUI; }
        set { timerUI = value; }
    }
    #endregion

    #region IEnumerators
    private IEnumerator ManageCar()
    {
        while (true)
        {
            departureTime = Random.Range(departureTimeRange[0], departureTimeRange[1]);
            yield return new WaitForSeconds(departureTime);
            isDeparting = true;
            isArriving = false;

            arrivalTime = Random.Range(arrivalTimeRange[0], arrivalTimeRange[1]);
            yield return new WaitForSeconds(arrivalTime);
            isArriving = true;
            isDeparting = false;
        }
    }
    #endregion

    #region Methods
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

    private void ManageTimer()
    {
        if (!isDeparting &&
            isArriving)
        {
            if (currentDepartureDelay < Time.time)
            {
                currentDepartureDelay = departureTime + Time.time;
                sliderTimer.maxValue = departureTime;
            }

            //slider drains down
            sliderTimer.value = currentDepartureDelay - Time.time;
            textTimer.text = string.Format("Departing in {0:0.00}s", currentDepartureDelay - Time.time);
        }
        else if (!isArriving &&
                 isDeparting)
        {
            if (currentArrivalTimeDelay < Time.time)
            {
                currentArrivalTimeDelay = arrivalTime + Time.time;
                sliderTimer.maxValue = arrivalTime;
            }
            
            //slider fills up
            sliderTimer.value = arrivalTime - (currentArrivalTimeDelay - Time.time);
            textTimer.text = string.Format("Arriving in {0:0.00}s", currentArrivalTimeDelay - Time.time);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.gameObject;
        StartCoroutine(ManageCar());

        sliderTimer = timerUI.transform.GetChild(0).GetComponent<Slider>();
        textTimer = timerUI.transform.GetChild(1).GetComponent<Text>();

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

        ManageTimer();

        if (isDeparting)
        {
            Depart();
        }

        if (isArriving)
        {
            Arrive();
        }

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
