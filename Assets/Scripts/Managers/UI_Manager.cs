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

    public GameObject numFatiguedDriversUI;
    private Slider numFatiguedDriversSlider;
    private Text numFatiguedDriversText;

    private GameManager gameManager;
    private float originalNumDrivers = 0;

    public GameObject statsUI_Prefab;
    private GameObject statsUI_Instance;

    [SerializeField] private float notificationTime = 3.0f;
    public FlashObject notifyDelivery_Instance;
    public FlashObject notifyNumFatigued_Instance;
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

    public void ShowPlayerStats()
    {
        statsUI_Instance = Instantiate(statsUI_Prefab, GameObject.FindWithTag("UI").transform);
    }

    public void HidePlayerStats()
    {
        Destroy(statsUI_Instance);
    }
    #endregion

    #region Private Methods
    private void UpdateDeliveryUI()
    {
        float oldVal = numDeliveriesSlider.value;
        numDeliveriesSlider.value = Percentage.GetPercentage(gameManager.NumDrivers, 
                                                             originalNumDrivers,
                                                             numDeliveriesSlider.value);
        numDeliveriesText.text = string.Format("{0}/{1}", 
                                               originalNumDrivers - gameManager.NumDrivers, 
                                               originalNumDrivers);
        
        if (oldVal != numDeliveriesSlider.value)
        {
            StartCoroutine(notifyDelivery_Instance.DoFlash(notificationTime));
            oldVal = numDeliveriesSlider.value;
        }        
    }

    private void UpdateFatiguedDriversUI()
    {
        float oldVal = numFatiguedDriversSlider.value;
        numFatiguedDriversSlider.value = Percentage.GetReversePercentage(gameManager.NumFatiguedDrivers, 
                                                                         gameManager.NumFatiguedDriversThreshold, 
                                                                         numFatiguedDriversSlider.value);
        numFatiguedDriversText.text = string.Format("{0}/{1}", 
                                      gameManager.NumFatiguedDrivers, 
                                      gameManager.NumFatiguedDriversThreshold);

        if (oldVal != numFatiguedDriversSlider.value)
        {
            StartCoroutine(notifyNumFatigued_Instance.DoFlash(notificationTime));
            oldVal = numFatiguedDriversSlider.value;
        }
    }
    #endregion

    private void Start()
    {
        gameManager = this.gameObject.GetComponent<GameManager>();
        originalNumDrivers = gameManager.NumDrivers;

        numDeliveriesSlider = deliveryUI.transform.GetChild(0).GetComponent<Slider>();
        numDeliveriesText = deliveryUI.transform.GetChild(1).GetComponent<Text>();

        numFatiguedDriversSlider = numFatiguedDriversUI.transform.GetChild(0).GetComponent<Slider>();
        numFatiguedDriversText = numFatiguedDriversUI.transform.GetChild(1).GetComponent<Text>();
    }

    private void Update()
    {
        UpdateDeliveryUI();
        UpdateFatiguedDriversUI();
    }
}
