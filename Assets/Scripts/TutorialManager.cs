using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject pageOne;
    public GameObject pageTwo;
    public GameObject pageThree;

    private bool tutComplete = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    public void SkipTutorial()
    {
        pageOne.SetActive(false);
        pageTwo.SetActive(false);
        Time.timeScale = 1f;
        tutComplete = true;

        gameObject.SetActive(false);
    }

    public void FinishedTutorial()
    {
        pageThree.SetActive(false);
        Time.timeScale = 1f;
        tutComplete = true;

        gameObject.SetActive(false);
    }

    public void SecondPage()
    {
        pageOne.SetActive(false);
        pageTwo.SetActive(true);
    }

    public void ThirdPage()
    {
        pageTwo.SetActive(false);
        pageThree.SetActive(true);
    }
}
