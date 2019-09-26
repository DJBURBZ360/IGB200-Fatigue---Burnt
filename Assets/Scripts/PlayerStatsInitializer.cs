using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsInitializer : MonoBehaviour
{
    [SerializeField] private Text txt_numFails;
    [SerializeField] private Text txt_numFatiguedDrivers;
    [SerializeField] private Text txt_numItemsGiven;
    [SerializeField] private Text txt_numItemsDropped;
    [SerializeField] private Text txt_numItemsGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        txt_numFails.text = string.Format("Number of times you've failed: {0}", PlayerStats.NumFails);
        txt_numFatiguedDrivers.text = string.Format("Number of drivers left fatigued: {0}", PlayerStats.NumFatiguedDrivers);
        txt_numItemsGiven.text = string.Format("Number of items given: {0}", PlayerStats.NumItemsGiven);
        txt_numItemsDropped.text = string.Format("Number of items dropped: {0}", PlayerStats.NumItemsDropped);
        txt_numItemsGrabbed.text = string.Format("Number of items grabbed: {0}", PlayerStats.NumItemsGrabbed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
