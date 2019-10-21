using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    public int saveFrequency = 30;

    /// <summary>
    /// Saves player stats frequently.
    /// </summary>
    /// <param name="saveFrequency">The frequency of saving.</param>
    /// <returns></returns>
    private IEnumerator DoSave(int saveFrequency)
    {
        yield return new WaitForSeconds(saveFrequency);
        SaveDataManagement.SaveState();
    }

    private void Start()
    {
        StartCoroutine(DoSave(saveFrequency));
    }
}
