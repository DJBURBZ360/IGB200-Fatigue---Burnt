using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManagement : MonoBehaviour
{
    #region Public Methods
    public static void SaveState()
    {
        PlayerPrefs.SetInt("NumFails", PlayerStats.NumFails);
        PlayerPrefs.SetInt("NumFatiguedDrivers", PlayerStats.NumFatiguedDrivers);
        PlayerPrefs.SetInt("NumSavedDrivers", PlayerStats.NumSavedDrivers);
        PlayerPrefs.SetInt("NumItemsGiven", PlayerStats.NumItemsGiven);
        PlayerPrefs.SetInt("NumItemsDropped", PlayerStats.NumItemsDropped);
        PlayerPrefs.SetInt("NumItemsGrabbed", PlayerStats.NumItemsGrabbed);

        PlayerPrefs.Save();
    }

    public static void LoadState()
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
    #endregion
    
    public int saveFrequency = 30;

    /// <summary>
    /// Saves player stats frequently.
    /// </summary>
    /// <param name="saveFrequency">The frequency of saving.</param>
    /// <returns></returns>
    private IEnumerator AutoSave(int saveFrequency)
    {
        yield return new WaitForSeconds(saveFrequency);
        SaveState();
    }

    private void Start()
    {
        StartCoroutine(AutoSave(saveFrequency));
    }
}
