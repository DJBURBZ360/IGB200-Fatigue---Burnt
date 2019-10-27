using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    [Header("Lerp to what relative position?")] [SerializeField] private Vector2 lerpPos;
    [SerializeField] private float interpolationSpeed = 10.0f;
    private float interpolation;
    private Vector2 originalPos;
    private RectTransform uiTransform;
    private bool doLerp = false;

    public bool DoLerp
    {
        get { return doLerp; }
        set { doLerp = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiTransform = gameObject.GetComponent<RectTransform>();
        originalPos = uiTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (doLerp)
        {
            interpolation += interpolationSpeed * Time.deltaTime;
            interpolation = interpolation > 1 ? 1 : interpolation;
        }
        else
        {
            interpolation -= interpolationSpeed * Time.deltaTime;
            interpolation = interpolation < 0 ? 0 : interpolation;
        }
        uiTransform.localPosition = Vector2.Lerp(originalPos, originalPos + lerpPos, interpolation);
    }
}
