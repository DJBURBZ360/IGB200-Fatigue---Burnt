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

    public void ShowFailUI()
    {
        failUI_Instance = Instantiate(failUI_Prefab, GameObject.FindWithTag("UI").transform);
    }

    public void HideFailUI()
    {
        Destroy(failUI_Instance);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
