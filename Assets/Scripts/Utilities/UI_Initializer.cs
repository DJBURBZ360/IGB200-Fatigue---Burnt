using UnityEngine;

/// <summary>
/// Assigns the Respective UI to the Employee prefab, then parent the given UI prefab to the UI tree.
/// </summary>
[ExecuteAlways]
public class UI_Initializer : MonoBehaviour
{
    [SerializeField] private GameObject timerUI;
                     private GameObject instanciatedTimer;

    [SerializeField] private GameObject fatigueUI;
                     private GameObject instanciatedFatigue;

    [SerializeField] private Vector2 UIoffset;
    [SerializeField] private Vector2 UIspacing;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            if (timerUI != null)
                gameObject.GetComponent<CarManager>().TimerUI = timerUI;

            if (fatigueUI != null)
                transform.GetChild(0).GetComponent<Employee>().FatigueUI = fatigueUI;
        }
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            Vector2 currentScreenPos = this.gameObject.transform.position;

            if (timerUI != null)
                timerUI.transform.position = Camera.main.WorldToScreenPoint(currentScreenPos + UIoffset + UIspacing);

            if (fatigueUI != null)
                fatigueUI.transform.position = Camera.main.WorldToScreenPoint(currentScreenPos + UIoffset);
        }
    }
}
