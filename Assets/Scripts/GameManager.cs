using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    //level 0, 1, 2, 3, 4
    public Color[] fatigueLevelColors = new Color[5];
    public bool isPlaying = true;

    [SerializeField] private int numFatiguedDrivers = 0;
    [SerializeField] private int numFatiguedDriversThreshold = 1;
    private GameObject[] employees;

    private UI_Manager uiManager;
    #endregion

    #region Accessors
    public int NumFatiguedDrivers
    {
        get { return numFatiguedDrivers; }
        set { numFatiguedDrivers = value; }
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

    private void CountEmployees()
    {
        if (employees.Length <= 0)
        {
            isPlaying = false;
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

        if (!isPlaying)
        {
            DoFailEvent();
        }
    }
}
