using UnityEngine;

[ExecuteAlways]
public class UI_Initializer : MonoBehaviour
{
    [SerializeField] private GameObject timerUI;
    [SerializeField] private GameObject fatigueUI;
    [SerializeField] private Vector3 UIoffset;
    [SerializeField] private Vector3 UIspacing;

    private void Awake()
    {
        CarManager carManager = this.gameObject.GetComponent<CarManager>();
        Employee employee = this.gameObject.transform.GetChild(0).GetComponent<Employee>();

        if (carManager.TimerUI == null) carManager.TimerUI = timerUI;
        if (employee.FatigueUI == null) employee.FatigueUI = fatigueUI;
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (timerUI != null)
                timerUI.transform.localPosition = UIoffset;

            if (fatigueUI != null)
                fatigueUI.transform.localPosition = UIoffset + UIspacing;
        }
    }
}
