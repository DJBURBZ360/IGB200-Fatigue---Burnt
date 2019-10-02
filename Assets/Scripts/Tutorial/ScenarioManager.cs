using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScenarioManager : MonoBehaviour
{   
    public enum TriggerType { OnSpawn, OnDestroy, OnKeyPress }

    #region Variables
    private TutorialManager tutorial;
    [SerializeField] private TriggerType trigger;
    [SerializeField] private GameObject thisScenarioButton; //the button for this task   
    [SerializeField] private GameObject nextTask;

    [SerializeField] private bool showButtonOnNext = true;
    [SerializeField] private GameObject nextButton; //the button AFTER this task

    [Header("For OnKeypress Trigger")]
    //on key press
    [SerializeField] private KeyCode[] keys;
    [SerializeField] private float destroyDelay;
    private bool isCoroutineStarted = false;

    [Header("For OnSpawn or OnDestroy Trigger")]
    //on spawn or destroy
    [SerializeField] private string targetGameObjectTag;
    private bool isAttached = false;
    #endregion

    #region Co-Routines
    private IEnumerator TimedCue(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DoNextTask();
    }
    #endregion


    #region Private Methods
    private void TriggerOnKeyPress()
    {
        if (!isCoroutineStarted)
        {
            foreach (KeyCode input in keys)
            {
                if (Input.GetKeyDown(input))
                {
                    StartCoroutine(TimedCue(destroyDelay));
                    isCoroutineStarted = true;
                }
            }
        }
    }

    /// <summary>
    /// Shows the next task and button, then destroy the previous task.
    /// </summary>
    private void DoNextTask()
    {        
        Destroy(this.gameObject);       
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        tutorial = GameObject.FindWithTag("Tutorial").GetComponent<TutorialManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetGameObjectTag != null)
        {
            if (trigger == TriggerType.OnSpawn)
            {
                if (GameObject.FindWithTag(targetGameObjectTag) != null)
                {
                    DoNextTask();
                }
            }
            else if (trigger == TriggerType.OnDestroy)
            {
                if (!isAttached)
                {
                    if (GameObject.FindWithTag(targetGameObjectTag) != null)
                    {
                        this.gameObject.transform.parent = GameObject.FindWithTag(targetGameObjectTag).transform;
                        isAttached = true;
                    }
                }
            }
        }

        if (trigger == TriggerType.OnKeyPress)
        {
            TriggerOnKeyPress();
        }
    }

    private void OnDestroy()
    {
        if (showButtonOnNext)
        {
            if (thisScenarioButton != null) thisScenarioButton.SetActive(true);
        }
        else
        {
            tutorial.DoNext();
            if (nextButton != null) nextButton.SetActive(true);
        }

        if (nextTask != null) nextTask.SetActive(true);
    }
}

