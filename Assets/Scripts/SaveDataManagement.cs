using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManagement : MonoBehaviour
{
    public void SaveState()
    {
        PlayerPrefs.SetInt("NumFails", PlayerStats.NumFails);
        PlayerPrefs.SetInt("NumFatiguedDrivers", PlayerStats.NumFatiguedDrivers);
        PlayerPrefs.SetInt("NumSavedDrivers", PlayerStats.NumSavedDrivers);
        PlayerPrefs.SetInt("NumItemsGiven", PlayerStats.NumItemsGiven);
        PlayerPrefs.SetInt("NumItemsDropped", PlayerStats.NumItemsDropped);
        PlayerPrefs.SetInt("NumItemsGrabbed", PlayerStats.NumItemsGrabbed);

        PlayerPrefs.Save();
    }

    public void LoadState()
    {
        PlayerStats.NumFails = PlayerPrefs.GetInt("NumFails");
        PlayerStats.NumFatiguedDrivers = PlayerPrefs.GetInt("NumFatiguedDrivers");
        PlayerStats.NumSavedDrivers = PlayerPrefs.GetInt("NumSavedDrivers");
        PlayerStats.NumItemsGiven = PlayerPrefs.GetInt("NumItemsGiven");
        PlayerStats.NumItemsDropped = PlayerPrefs.GetInt("NumItemsDropped");
        PlayerStats.NumItemsGrabbed = PlayerPrefs.GetInt("NumItemsGrabbed");
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerStats.ResetStats();
    }
}
