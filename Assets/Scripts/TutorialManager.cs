using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TutorialManager : MonoBehaviour
{
    #region Variables    
    [SerializeField] private Sprite[] tutorialCues = new Sprite[17];
    [SerializeField] private GameObject employeeInstance;
    private CarManager car;
    private Employee employee;
    private Image image;

    private int scenarioNum = 0;
    [SerializeField][Range(0.0f, 1.0f)]private float carInterpolationValue = 1;
    #endregion

    #region Public Methods
    public void DoNext()
    {
        scenarioNum++;
        image.sprite = tutorialCues[scenarioNum];
    }

    public void ForceArriveCar()
    {
        car.ForceArrive();
    }

    public void ForceDepartCar()
    {
        car.ForceDepart();
    }

    public void OverrideEmployeeFatigueLevel(int level)
    {
        employee.OverrideFatigueLevel(level);
    }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        image = this.gameObject.GetComponent<Image>();
        image.sprite = tutorialCues[scenarioNum];

        car = employeeInstance.GetComponent<CarManager>();
        employee = employeeInstance.transform.GetChild(0).GetComponent<Employee>();

        //force the car to intially moved away
        car.OverrideInterpolationValue(1);
        ForceDepartCar();
    }

    private void Update()
    {

    }
}
