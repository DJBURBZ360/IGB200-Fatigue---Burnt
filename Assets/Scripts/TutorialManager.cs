using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TutorialManager : MonoBehaviour
{
    #region Variables    
    [SerializeField] private Sprite[] tutorialCues = new Sprite[17];
    [SerializeField] private GameObject employeeInstance;
    private CarManager car;
    private Employee employee;
    private SpriteRenderer renderer;

    private int cueNum = 0;
    [SerializeField][Range(0.0f, 1.0f)]private float carInterpolationValue = 1;
    #endregion

    #region Public Methods
    public void DoNext()
    {
        cueNum++;
        renderer.sprite = tutorialCues[cueNum];
    }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = tutorialCues[cueNum];

        car = employeeInstance.GetComponent<CarManager>();
        employee = employeeInstance.transform.GetChild(0).GetComponent<Employee>();

        //force the car to intially moved away
        car.OverrideInterpolationValue(1);
    }

    private void Update()
    {
        car.OverrideInterpolationValue(carInterpolationValue);
    }
}
