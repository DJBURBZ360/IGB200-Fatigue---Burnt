using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject pageOne;
    public GameObject pageTwo;
    public GameObject pageThree;
    public GameObject pageFour;
    public GameObject pageFive;
    public GameObject pageSix;

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
        pageThree.SetActive(false);
        pageFour.SetActive(false);
        pageFive.SetActive(false);
        pageSix.SetActive(false);
        Time.timeScale = 1f;
        tutComplete = true;

        gameObject.SetActive(false);
    }

    public void FinishedTutorial()
    {
        pageSix.SetActive(false);
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


    public void FourthPage()
    {
        pageThree.SetActive(false);
        pageFour.SetActive(true);
    }

    public void FifthPage()
    {
        pageFour.SetActive(false);
        pageFive.SetActive(true);
    }

    public void SixthPage()
    {
        pageFive.SetActive(false);
        pageSix.SetActive(true);
    }

}
