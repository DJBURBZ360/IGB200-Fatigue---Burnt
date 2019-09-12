using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the necessary UIs during game runtime.
/// </summary>
public class UI_Manager : MonoBehaviour
{
    public GameObject failUI_Prefab;
    private GameObject failUI_Instance;

    public GameObject winUI_Prefab;
    private GameObject winUI_Instance;

    public GameObject pauseMenu_Prefab;
    private GameObject pauseMenu_Instance;

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
}
