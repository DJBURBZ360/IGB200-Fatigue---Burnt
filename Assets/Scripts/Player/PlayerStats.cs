using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class PlayerStats
{
    #region Variables
    private static int numFails;
    private static int numFatiguedDrivers;
    private static int numSavedDrivers;
    private static int numItemsGiven;
    private static int numItemsDropped;
    private static int numItemsGrabbed;
    #endregion

    #region Accessors
    public static int NumFails
    {
        get { return numFails; }
        set { numFails = value; }
    }

    public static int NumFatiguedDrivers
    {
        get { return numFatiguedDrivers; }
        set { numFatiguedDrivers = value; }
    }

    public static int NumSavedDrivers
    {
        get { return numSavedDrivers; }
        set { numSavedDrivers = value; }
    }

    public static int NumItemsGiven
    {
        get { return numItemsGiven; }
        set { numItemsGiven = value; }
    }

    public static int NumItemsDropped
    {
        get { return numItemsDropped; }
        set { numItemsDropped = value; }
    }

    public static int NumItemsGrabbed
    {
        get { return numItemsGrabbed; }
        set { numItemsGrabbed = value; }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Resets all player stats to 0.
    /// </summary>
    public static void ResetStats()
    {
        numFails = 0;
        numFatiguedDrivers = 0;
        numSavedDrivers = 0;
        numItemsDropped = 0;
        numItemsGiven = 0;
    }
    #endregion
}
