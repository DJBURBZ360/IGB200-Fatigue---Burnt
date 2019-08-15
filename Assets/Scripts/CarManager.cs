using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour
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

    private bool isDeparting = false,
                 isArriving = true,
                 isParked = true;

    public GameObject timerUI;
    private Slider sliderTimer;
    private Text textTimer;
    #endregion

    #region Accessors    
    public bool IsParked
    {
        get { return isParked; }
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

            //slider drains down
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

        sliderTimer = timerUI.transform.GetChild(0).GetComponent<Slider>();
        textTimer = timerUI.transform.GetChild(1).GetComponent<Text>();

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
