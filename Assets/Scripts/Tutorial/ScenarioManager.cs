using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScenarioManager : MonoBehaviour
{   
    public enum TriggerType { OnSpawn, OnDestroy, OnKeyPress, OnCollision }

    #region Variables
    private TutorialManager tutorial;
    [SerializeField] private TriggerType trigger;
    //[SerializeField] private ScenarioManager secondTrigger;
    [SerializeField] private bool automateNextTask = false;
    [SerializeField] private GameObject thisScenarioButton; //the button for this task   
    [SerializeField] private GameObject[] nextTasks;
    [SerializeField] private GameObject[] hideTasks;
    [SerializeField] private GameObject[] destroyTasks;    

    [Header("For OnKeypress Trigger")]
    //on key press
    [SerializeField] private KeyCode[] keys;
    [SerializeField] private float destroyDelay;
    private bool isCoroutineStarted = false;

    [Header("For OnSpawn or OnDestroy Trigger")]
    //on spawn or destroy
    [SerializeField] private string targetGameObjectName;
    [SerializeField] private string targetGameObjectTag;
    private bool isAttached = false;

    [Header("For OnCollision Trigger")]
    [SerializeField] private string collisionTag = "Player";
    private Collider2D collider;
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
        //tutorial = GameObject.FindWithTag("Tutorial").GetComponent<TutorialManager>();  
        if (trigger == TriggerType.OnCollision)
        {
            collider = this.gameObject.GetComponent<Collider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger == TriggerType.OnSpawn)
        {
            if (targetGameObjectName != null)
            {
                if (targetGameObjectName != "" &&
                    GameObject.Find(targetGameObjectName) != null)
                {
                    DoNextTask();
                }
            }

            if (targetGameObjectTag != null)
            {
                if (targetGameObjectTag != "" && 
                    GameObject.FindWithTag(targetGameObjectTag) != null)
                {
                    DoNextTask();
                }
            }
        }
        else if (trigger == TriggerType.OnDestroy)
        {
            if (!isAttached)
            {
                if (targetGameObjectName != null)
                {
                    if (targetGameObjectName != "" &&
                        GameObject.Find(targetGameObjectName) != null)
                    {
                        this.gameObject.transform.parent = GameObject.Find(targetGameObjectName).transform;
                        isAttached = true;
                    }
                }

                if (targetGameObjectTag != null)
                {
                    if (targetGameObjectTag != "" &&
                        GameObject.FindWithTag(targetGameObjectTag) != null)
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
        if (thisScenarioButton != null) thisScenarioButton.SetActive(true);
        if (nextTasks.Length > 0)
        {
            foreach (GameObject task in nextTasks)
            {
                task.SetActive(true);
            }
        }

        if (hideTasks.Length > 0)
        {
            foreach (GameObject task in hideTasks)
            {
                task.SetActive(false);
            }
        }

        if (destroyTasks.Length > 0)
        {
            foreach (GameObject task in destroyTasks)
            {
                Destroy(task);
            }
        }

        if (automateNextTask) GameObject.FindWithTag("Tutorial").GetComponent<TutorialManager>().DoNext();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trigger == TriggerType.OnCollision)
        {
            if (collision.tag == collisionTag)
            {
                DoNextTask();
            }
        }
    }
}

