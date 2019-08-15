using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : Employee
{
    #region Variables
    [SerializeField]
    [Range(0, 1)]
    private float interpolate = 0;

    [SerializeField]
    private float interpolateSpeed = 1;

    private GameObject sprite;
    public GameObject[] points = new GameObject[2];

    [SerializeField]
    private float departureTime = 1f;
    private float currentDepartureDelay;

    [SerializeField]
    private float arrivalTime = 1f;
    private float currentArrivalTimeDelay;

    bool isDeparting = false,
         isArriving = true;

    public Slider sliderTimer;
    public Text textTimer;
    #endregion

    #region Accessors
    public float DepartureTime
    {
        get { return departureTime; }
    }

    public float ArrivalTime
    {
        get { return arrivalTime; }
    }
    #endregion

    #region IEnumerators
    private IEnumerator ManageCar()
    {
        while (true)
        {
            yield return new WaitForSeconds(departureTime);
            isDeparting = true;
            isArriving = false;

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
            interpolate += interpolateSpeed * Time.deltaTime;
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
            interpolate -= interpolateSpeed * Time.deltaTime;
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

            //slider fills down
            sliderTimer.value = currentDepartureDelay - Time.time;
            textTimer.text = string.Format("Departing in {0:0.00}s", currentDepartureDelay - Time.time);
        }
        else if (!isArriving &&
                 isDeparting)
        {
            if (currentArrivalTimeDelay <Time.time)
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

        //Instanciate UI then parent it to "UI" gameobject
    }

    // Update is called once per frame
    void Update()
    {
        //Imitate perspective movement
        sprite.transform.position = Vector2.Lerp(points[0].transform.position, points[1].transform.position, interpolate);
        sprite.transform.localScale = Vector2.Lerp(points[0].transform.localScale, points[1].transform.localScale, interpolate);

        ManageTimer();

        if (isDeparting)
        {
            Depart();
        }

        if (isArriving)
        {
            Arrive();
        }
    }
}
