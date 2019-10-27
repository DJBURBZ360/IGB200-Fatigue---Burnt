using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsInitializer : MonoBehaviour
{
    [SerializeField] private Text txt_numFails;
    [SerializeField] private Text txt_numFatiguedDrivers;
    [SerializeField] private Text txt_numSavedDrivers;
    [SerializeField] private Text txt_numItemsGiven;
    [SerializeField] private Text txt_numItemsDropped;
    [SerializeField] private Text txt_numItemsGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        //if (txt_numFails != null) txt_numFails.text = string.Format("Number of times you've failed: {0}", PlayerStats.NumFails);
        //if (txt_numFatiguedDrivers != null) txt_numFatiguedDrivers.text = string.Format("Number of drivers left fatigued: {0}", PlayerStats.NumFatiguedDrivers);
        //if (txt_numSavedDrivers != null) txt_numSavedDrivers.text = string.Format("Number of drivers you've saved: {0}", PlayerStats.NumSavedDrivers);
        //if (txt_numItemsGiven != null) txt_numItemsGiven.text = string.Format("Number of items given: {0}", PlayerStats.NumItemsGiven);
        //if (txt_numItemsDropped != null) txt_numItemsDropped.text = string.Format("Number of items dropped: {0}", PlayerStats.NumItemsDropped);
        //if (txt_numItemsGrabbed != null) txt_numItemsGrabbed.text = string.Format("Number of items grabbed: {0}", PlayerStats.NumItemsGrabbed);

        if (txt_numFails != null) txt_numFails.text = PlayerStats.NumFails.ToString();
        if (txt_numFatiguedDrivers != null) txt_numFatiguedDrivers.text = PlayerStats.NumFatiguedDrivers.ToString();
        if (txt_numSavedDrivers != null) txt_numSavedDrivers.text = PlayerStats.NumSavedDrivers.ToString();
        if (txt_numItemsGiven != null) txt_numItemsGiven.text = PlayerStats.NumItemsGiven.ToString();
        if (txt_numItemsDropped != null) txt_numItemsDropped.text = PlayerStats.NumItemsDropped.ToString();
        if (txt_numItemsGrabbed != null) txt_numItemsGrabbed.text = PlayerStats.NumItemsGrabbed.ToString();

        Time.timeScale = 1;
    }
}
