using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPage : MonoBehaviour
{

    public GameObject pageOne;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void finishTutorial()
    {
        pageOne.SetActive(false);
        Time.timeScale = 1f;
    }

}
