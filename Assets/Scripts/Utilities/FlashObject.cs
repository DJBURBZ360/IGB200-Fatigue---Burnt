using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashObject : MonoBehaviour
{
    #region Variables
    [SerializeField] private float appearaceTime = 0.1f;
    [SerializeField] private float fadeSpeed = 3.0f;
    [SerializeField] private bool enableFlash = true;
    private float originalOpacity;
    [SerializeField] private Graphic renderer;
    private Fader fader = new Fader();
    #endregion

    #region Accessors
    public bool EnableFlash
    {
        get { return enableFlash; }
        set { enableFlash = value; }
    }

    public float FadeSpeed
    {
        get { return fadeSpeed; }
    }

    public float AppearanceTime
    {
        get { return appearaceTime; }
    }

    public float OriginalOpacity
    {
        get { return originalOpacity; }
    }
    #endregion

    public IEnumerator DoFlash(float upTime)
    {
        enableFlash = true;
        yield return new WaitForSeconds(upTime);

        //set to transparent when done
        Color color = renderer.color;
        color.a = originalOpacity;
        renderer.color = color;

        enableFlash = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<Graphic>();
        originalOpacity = renderer.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableFlash)
        {
            Color color = renderer.color;
            if (color.a <= 0)
            {
                fader.ResetFader();
            }
            fader.DoFade(ref color.a, fadeSpeed, appearaceTime, originalOpacity);
            renderer.color = color;
        }
        else
        {
            Color color = renderer.color;
            color.a = originalOpacity;
            renderer.color = color;
        }
    }
}
