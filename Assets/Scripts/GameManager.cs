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
    private GameObject[] employees;

    private UI_Manager uiManager;
    #endregion

    #region Accessors
    public int NumFatiguedDrivers
    {
        get { return numFatiguedDrivers; }
        set { numFatiguedDrivers = value; }
    }

    public int NumDrivers
    {
        get { return numDrivers; }
        set { numDrivers = value; }
    }

    public bool IsPlaying
    {
        get { return isPlaying; }
        set { isPlaying = value; }
    }
    #endregion

    #region Methods
    private void CheckGameState()
    {
        //if passed threshold, do fail event
        if (numFatiguedDrivers > numFatiguedDriversThreshold)
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
        else
        {

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

        if (!isPlaying)
        {
            DoFailEvent();
        }
    }
}
