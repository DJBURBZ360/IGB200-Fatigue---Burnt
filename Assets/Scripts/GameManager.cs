using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    //level 0, 1, 2, 3, 4
    public Color[] fatigueLevelColors = new Color[5];
    private bool isPlaying = true;

    [SerializeField] private int numDrivers = 15;
    [SerializeField] private int numFatiguedDrivers = 0;
    [SerializeField] private int numFatiguedDriversThreshold = 1;
    [SerializeField] private float[] fatiguedChances = new float[4]; //0 = lvl0, 1 = lvl1...
    [SerializeField] private Employee.FatigueTypes[] availableFatigueTypes;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject SFX_Pause;
    [SerializeField] private GameObject SFX_Unpause;
    private GameObject[] employees;
    private UI_Manager uiManager;
    #endregion

    #region Accessors
    public float NumFatiguedDrivers
    {
        get { return numFatiguedDrivers; }
        set { numFatiguedDrivers = (int)value; }
    }

    public int NumDrivers
    {
        get { return numDrivers; }
        set { numDrivers = value; }
    }

    public float NumFatiguedDriversThreshold
    {
        get { return (float) numFatiguedDriversThreshold; }
    }

    public bool IsPlaying
    {
        get { return isPlaying; }
        set { isPlaying = value; }
    }

    public Employee.FatigueTypes[] AvailableFatigueTypes
    {
        get { return availableFatigueTypes; }
    }
    #endregion

    #region Methods
    private void CheckGameState()
    {
        //if passed threshold, do fail event
        if (numFatiguedDrivers >= numFatiguedDriversThreshold)
        {
            isPlaying = false;
        }
    }

    private void DoFailEvent()
    {
        if (Time.timeScale != 0)
        {
            //show fail UI
            uiManager.ShowFailUI();

            //pause game   
            Time.timeScale = 0;
            Item.numInstance = 0;
        }
    }

    private void DoWinEvent()
    {
        if (Time.timeScale != 0)
        {
            //show fail UI
            uiManager.ShowWinUI();

            //pause game   
            Time.timeScale = 0;
            Item.numInstance = 0;
        }
    }

    private void CountEmployees()
    {
        if (numDrivers <= 0)
        {
            DoWinEvent();
        }
    }

    public void DoRNG(int fatigueLevel)
    {
        if (fatigueLevel == 1)
        {
            var list = new[] {
                ProportionValue.Create(0.2, "true"),
                ProportionValue.Create(0.8, "false")
            };

            if (list.ChooseByRandom() == "true")
            {
                numFatiguedDrivers++;
            }
        }
        else if (fatigueLevel == 2)
        {
            var list = new[] {
                ProportionValue.Create(0.5, "true"),
                ProportionValue.Create(0.5, "false")
            };

            if (list.ChooseByRandom() == "true")
            {
                numFatiguedDrivers++;
            }
        }
        else if (fatigueLevel == 3)
        {
            numFatiguedDrivers++;
        }
    }

    private void PauseGame()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                uiManager.ShowPauseMenu();
                if (SFX_Pause != null) Instantiate(SFX_Pause);
            }
            else
            {
                Time.timeScale = 1;
                uiManager.HidePauseMenu();
                if (SFX_Unpause != null) Instantiate(SFX_Unpause);
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        uiManager = this.gameObject.GetComponent<UI_Manager>();
        employees = GameObject.FindGameObjectsWithTag("Employee");
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameState();
        CountEmployees();

        if (tutorialUI == null) PauseGame();
        if (!isPlaying) DoFailEvent();
    }
}
