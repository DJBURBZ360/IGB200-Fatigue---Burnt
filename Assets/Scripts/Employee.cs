using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{
    #region Variables
    [SerializeField]
    [Header("Generate Random Fatigue Rate Between")]
    private float[] randomNumRange = new float[2];

    [SerializeField] private int currentFatigueLevel = 0,
                                 maxFatigueLevel = 0;

    private float currentFatigueRate = 0,
                  currentFatigueDelay = 0;

    //private enum SnackType {level1, level2, level3};
    #endregion

    #region Methods
    private void IncreaseFatigueLevel()
    {
        //randomly generate a number
        currentFatigueRate = Random.Range(randomNumRange[0], randomNumRange[1]);

        //countdown then incease fatigue level
        if (currentFatigueDelay < Time.time)
        {
            currentFatigueDelay = currentFatigueRate + Time.time;
            currentFatigueLevel = currentFatigueLevel > maxFatigueLevel ? maxFatigueLevel : currentFatigueLevel++;
        }
    }

    private void DecreaseFatigueLevel()
    {
        //decrease fatigue level when given the appropriate snack
        currentFatigueLevel = currentFatigueLevel < 0 ? 0 : currentFatigueLevel--;
    }

    private void CheckForSnack(Collider2D collision)
    {
        Snack givenSnack = collision.GetComponent<Snack>();
        switch (currentFatigueLevel)
        {
            case 1:
                if (givenSnack.SnackLevel == Snack.SnackLevels.level1)
                {

                }
                else
                {
                    if (givenSnack.SnackLevel > Snack.SnackLevels.level1)
                    {
                        //no effects given
                    }
                }
                break;

            case 2:
                if (givenSnack.SnackLevel == Snack.SnackLevels.level2)
                {

                }
                else
                {
                    if (givenSnack.SnackLevel < Snack.SnackLevels.level2)
                    {
                        //give 25% effectiveness
                    }
                    else if (givenSnack.SnackLevel > Snack.SnackLevels.level2)
                    {
                        //no effects given 
                    }
                }
                break;

            case 3:
                if (givenSnack.SnackLevel == Snack.SnackLevels.level3)
                {

                }
                else
                {
                    if (givenSnack.SnackLevel < Snack.SnackLevels.level3)
                    {
                        //give 25% effectiveness
                    }
                }
                break;

            default:Debug.Log(currentFatigueLevel);
                break;
        }
    }

    private void CheckFatigueLevel()
    {
        if (currentFatigueLevel >= 4)
        {
            //fire employee
            //destroy gameobject
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //initialize fatigue timer
        currentFatigueRate = Random.Range(randomNumRange[0], randomNumRange[1]);
        currentFatigueDelay = currentFatigueRate + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //snacks will be prefabs with snack script attached to ID snack level
        //instanciate snack when clicked on snack icon from UI
        if (collision.GetComponent<Snack>() != null)
        {
            CheckForSnack(collision);
            Player.instance.HasSnack = false;
            Snack.numInstance--;
            Destroy(collision.gameObject);
        }
    }
}
