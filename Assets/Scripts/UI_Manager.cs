using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the necessary UIs during game runtime.
/// </summary>
public class UI_Manager : MonoBehaviour
{
    #region Variables
    public GameObject failUI_Prefab;
    private GameObject failUI_Instance;

    public GameObject winUI_Prefab;
    private GameObject winUI_Instance;

    public GameObject pauseMenu_Prefab;
    private GameObject pauseMenu_Instance;

    public GameObject deliveryUI;
    private Slider numDeliveriesSlider;
    private Text numDeliveriesText;

    public Slider numDriversFatiguedUI;

    private GameManager gameManager;
    private float originalNumDrivers = 0;
    #endregion

    #region Public Methods
    public void ShowFailUI()
    {
        failUI_Instance = Instantiate(failUI_Prefab, GameObject.FindWithTag("UI").transform);
    }

    public void ShowWinUI()
    {
        winUI_Instance = Instantiate(winUI_Prefab, GameObject.FindWithTag("UI").transform);
    }

    public void HideFailUI()
    {
        Destroy(failUI_Instance);
    }

    public void ShowPauseMenu()
    {
        pauseMenu_Instance = Instantiate(pauseMenu_Prefab, GameObject.FindWithTag("UI").transform);
    }

    public void HidePauseMenu()
    {
        Destroy(pauseMenu_Instance);
    }
    #endregion

    #region Private Methods
    private void UpdateDeliveryUI()
    {
        numDeliveriesSlider.value = 100 - ((gameManager.NumDrivers / originalNumDrivers) * 100);
    }

    private void UpdateFatiguedDriversUI()
    {
        numDriversFatiguedUI.value = 100 - ((gameManager.NumFatiguedDrivers / gameManager.NumFatiguedDriversThreshold) * 100);
        numDeliveriesText.text = string.Format("{0} / {1}", gameManager.NumFatiguedDrivers, gameManager.NumFatiguedDriversThreshold);
    }
    #endregion

    private void Start()
    {
        gameManager = this.gameObject.GetComponent<GameManager>();
        originalNumDrivers = gameManager.NumDrivers;

        numDeliveriesSlider = deliveryUI.transform.GetChild(0).GetComponent<Slider>();
        numDeliveriesText = deliveryUI.transform.GetChild(1).GetComponent<Text>();
    }

    private void Update()
    {
        UpdateDeliveryUI();
        UpdateFatiguedDriversUI();
    }
}
