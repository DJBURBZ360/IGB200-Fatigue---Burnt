using UnityEngine;
using UnityEngine.UI;

public class OneShotFade : MonoBehaviour
{
    private enum FadeType { FadeOut, FadeIn }
    [SerializeField] private Graphic renderer;
    [SerializeField] private float fadeRate = 0.3f;
    [SerializeField] private FadeType fadeType;
    private Fader fader = new Fader();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color color = renderer.color;   

        if (fadeType == FadeType.FadeOut)
        {
            fader.DoFadeOut(ref color.a, fadeRate);

            if (renderer.color.a <= 0) Destroy(this.gameObject);
        }
        else if (fadeType == FadeType.FadeIn)
        {
            fader.DoFadeIn(ref color.a, fadeRate);

            //if (renderer.color.a >= 1) Destroy(this.gameObject);
        }
        renderer.color = color;
    }
}
