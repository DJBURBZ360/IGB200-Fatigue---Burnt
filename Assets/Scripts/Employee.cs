using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class Employee : MonoBehaviour
{
    public enum FatigueTypes { Dizziness, Headache, Sleepiness };
    public const int NUM_FATIGUE_TYPES = 3;

    #region Variables
    [SerializeField] private GameObject groanSFX;

    [SerializeField]
    [Header("Appear Fatigue Gauge at Gauge [min, max] %")]
    [Range(0.0f, 100.0f)]
    private float[] percentageThreshold = { 20, 80 };

    [SerializeField]
    [Header("Fatigue Gauge Text Per Level")]
    private string[] fatigueLevelMessages = new string[5];

    [SerializeField]
    [Header("Fatigue Gauge Components With FlashObject Script Attached")]
    private FlashObject[] fatigueGaugeComponents;
    [SerializeField] private float gaugeFlashTime = 1;
    private Fader fader = new Fader();

    [SerializeField]
    [Header("Generate Random Fatigue Time Between (in seconds)")]
    private float[] randomNumRange = new float[2];

    [SerializeField] private int currentFatigueLevel = 0;
    [SerializeField] private int maxFatigueLevel = 0;

    private float currentFatigueRate = 0,
                  currentFatigueDelay = 0;
    [SerializeField] private FatigueTypes currentFatigueType;

    private bool isChecking = true;
    private bool isSentHome = false;
    private bool isGaugeFlashed = false;

    private GameObject fatigueUI;
    private Slider fatigueSlider;
    private Text fatigueText;
    private Image fillColor;

    private GameManager gameManager;
    private CarManager car;

    [SerializeField] private bool doGenerateRandomFatigueLevel = false;
    [SerializeField] private int maxRandomFatigueLevel = 3;
    private int driverColorArrayIndex = 0;

    private SpriteRenderer renderer;
    [SerializeField] private Sprite[] defaultDriverStates = new Sprite[3];
    [SerializeField] Sprites3DArray[] driverStates; //axis Z = color type
                                                    //axis Y = fatigue type
                                                    //axis X = fatigue level
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

    public int CurrentFatigueLevel
    {
        get { return currentFatigueLevel; }
    }

    public int MaxFatigueLevel
    {
        get { return maxFatigueLevel; }
    }
    #endregion

    #region Private Methods
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

            //play SFX
            Instantiate(groanSFX, this.transform.position, Quaternion.identity);
        }


        Color color = gameManager.fatigueLevelColors[currentFatigueLevel];
        color.a = fillColor.color.a;
        if (fillColor.color != color)
        {
            //fillColor.color = gameManager.fatigueLevelColors[currentFatigueLevel];
            fillColor.color = color;
        }
    }

    private void DecreaseFatigueLevel()
    {
        //decrease fatigue level when given the appropriate snack
        currentFatigueLevel = currentFatigueLevel < 0 ? 0 : --currentFatigueLevel;
    }

    /// <summary>
    /// Used to check what snack was given upon collision
    /// </summary>
    private void CheckForItem(Collider2D collision)
    {
        Item givenItem = collision.GetComponent<Item>();

        //Only proceed to further checks when the item corresponds to the fatigue type
        if (currentFatigueType == givenItem.ForFatigueType)
        {
            switch (currentFatigueLevel)
            {
                case 1:
                    if (givenItem.ItemLevel == Item.ItemLevels.level1)
                    {
                        ResetFatigueLevel();
                    }
                    break;

                case 2:
                    if (givenItem.ItemLevel == Item.ItemLevels.level2)
                    {
                        ResetFatigueLevel();
                    }
                    else if (givenItem.ItemLevel < Item.ItemLevels.level2)
                    {
                        DecreaseFatigueGauge();
                    }
                    break;

                case 3:
                    if (givenItem.ItemLevel < Item.ItemLevels.level3)
                    {
                        DecreaseFatigueGauge();
                    }
                    break;
            }
        }
    }    

    /// <summary>
    /// -25% from the current gauge.
    /// </summary>
    private void DecreaseFatigueGauge()
    {
        float temp = currentFatigueDelay - Time.time <= 0 ? 0 : currentFatigueDelay - Time.time;
        temp = (temp + (currentFatigueRate * 0.25f)) + Time.time;
        currentFatigueDelay = temp;
    }    

    /// <summary>
    /// Do fail event when reached fatigue level 4.
    /// </summary>
    private void CheckStatus()
    {
        //quick switch to prevent unecesarry events
        if (car.IsParked)
        {
            isChecking = true;
            if (currentFatigueLevel >= 4)
                gameManager.IsPlaying = false;
        }
        else
        {
            //naive delayed checking
            if (isChecking)
            {
                //do speed boost
                //resets timer only
                if (currentFatigueLevel <= 0 &&
                    !isSentHome)
                    Player.instance.ApplySpeedBoost();

                //if reached fatigue level 4, do fail event
                if (currentFatigueLevel >= 3)
                    gameManager.IsPlaying = false;

                isChecking = false;
                isSentHome = false;
            }
        }
    }

    /// <summary>
    /// Swaps out into a new sprite image that corresponds to the current fatigue level.
    /// </summary>
    private void ChangeState()
    {
        int axisY = (int)currentFatigueType;

        if (currentFatigueLevel == 0 &&
            defaultDriverStates.Length > 0)
        {
            renderer.sprite = defaultDriverStates[driverColorArrayIndex];
        }

        else if (currentFatigueLevel > 0 &&
                 currentFatigueLevel < maxFatigueLevel &&
                 driverStates.Length > 0)
        {
            renderer.sprite = driverStates[driverColorArrayIndex].
                              arrayAxisY[axisY].
                              arrayAxisX[currentFatigueLevel - 1];
        }
    }

    private void UpdateTimerUI()
    {
        fatigueSlider.value = Percentage.GetPercentage(currentFatigueDelay - Time.time, currentFatigueRate, fatigueSlider.value);

        //Manage gauge appearance time
        //if the car is parked appear gauge only between thresholds
        //else hide gauge
        if (car.IsParked)
        {
            if (currentFatigueLevel < maxFatigueLevel)
            {
                if (fatigueSlider.value <= percentageThreshold[0] ||
                    fatigueSlider.value >= percentageThreshold[1])
                {
                    fatigueUI.SetActive(true);
                    if (!isGaugeFlashed)
                    {
                        foreach (FlashObject component in fatigueGaugeComponents)
                        {
                            StartCoroutine(component.DoFlash(gaugeFlashTime));
                        }
                        isGaugeFlashed = true;
                    }
                }
                else
                {
                    fatigueUI.SetActive(false);

                    isGaugeFlashed = false;
                }
            }
        }
        else
        {
            fatigueUI.SetActive(false);
        }

        //Manage gauge text
        if (fatigueLevelMessages.Length >= maxFatigueLevel) //prevents IndexArrayOutOfBounds error
        {
            fatigueText.text = fatigueLevelMessages[currentFatigueLevel];
        }

        /*
        if (currentFatigueLevel < 1)
        {
            fatigueText.text = "Normal";
        }
        else if (currentFatigueLevel < maxFatigueLevel &&
                 currentFatigueLevel > 0)
        {
            fatigueText.text = string.Format("Fatigue Level: {0}", currentFatigueLevel);
        }
        else
        {
            fatigueText.text = "Totally Fatigued!!!";
        }
        */
    }
    #endregion

    #region Public Methods
    public void ChangeSpriteColor()
    {
        driverColorArrayIndex = Random.Range(0, 3);
    }

    public void GenerateRandomFatigueType()
    {
        int num = Random.Range(0, gameManager.AvailableFatigueTypes.Length);
        currentFatigueType = gameManager.AvailableFatigueTypes[num];
    }

    public void GenerateRandomFatigueLevel()
    {
        if (doGenerateRandomFatigueLevel)
        { currentFatigueLevel = Random.Range(0, maxRandomFatigueLevel); }
    }

    public void SendHome()
    {
        isSentHome = true;
        car.ChangeDriver();
    }

    /// <summary>
    /// Resets fatigue level and fatigue timer.
    /// </summary>
    public void ResetFatigueLevel()
    {
        currentFatigueLevel = 0;
        currentFatigueRate = Random.Range(randomNumRange[0], randomNumRange[1]);
        currentFatigueDelay = currentFatigueRate + Time.time;

        if (!gameManager.IsTutorialActive)
        {
            Color currentColor = gameManager.fatigueLevelColors[0];
            currentColor.a = fillColor.color.a;
            fillColor.color = currentColor;
        }
    }

    public void OverrideFatigueLevel(int level)
    {
        currentFatigueLevel = level;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        car = this.gameObject.transform.parent.GetComponent<CarManager>();

        if (!gameManager.IsTutorialActive)
        {
            //UI setup
            fatigueSlider = fatigueUI.transform.GetChild(0).GetComponent<Slider>();
            fatigueText = fatigueUI.transform.GetChild(1).GetComponent<Text>();
            fillColor = fatigueSlider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
            fillColor.color = gameManager.fatigueLevelColors[currentFatigueLevel];

            //initialize fatigue timer
            currentFatigueRate = Random.Range(randomNumRange[0], randomNumRange[1]);
            currentFatigueDelay = currentFatigueRate + Time.time;
                        
            GenerateRandomFatigueLevel();
        }

        GenerateRandomFatigueType();

        //choose random sprite color variation
        ChangeSpriteColor();
        renderer.sprite = defaultDriverStates[driverColorArrayIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsTutorialActive)
        {
            //increase only when below max fatigue level
            if (currentFatigueLevel < maxFatigueLevel) IncreaseFatigueLevel();

            UpdateTimerUI();
            CheckStatus();
        }
        
        ChangeState();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //snacks will be prefabs with snack script attached to ID snack level
        //instanciate snack when clicked on snack icon from UI
        if (collision.GetComponent<Item>() != null)
        {
            if (!collision.GetComponent<Item>().IsDragged)
            {
                PlayerStats.NumItemsGiven++;

                CheckForItem(collision);
                Player.instance.HasItem = false;
                Item.numInstance--;
                collision.GetComponent<Item>().PlaySound();
                Destroy(collision.gameObject);
            }
        }
    }
}
