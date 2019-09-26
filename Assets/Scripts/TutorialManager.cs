using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Destroy the tutorial game object and set the time scale to 1.
    /// </summary>
    public void FinishTutorial()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
}
