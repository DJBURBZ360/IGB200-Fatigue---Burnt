using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader
{
    private bool isFadeIn = true,
                 isFadeOut = false;

    private float delay;

    public Fader()
    { }
    
    public void DoFadeIn(ref float opacity, float fadeRate)
    {
        if (opacity <= 1)
        {
            opacity += fadeRate * Time.deltaTime;
        }
    }

    public void DoFadeIn(ref float opacity, float fadeRate, float maxOpacity)
    {
        if (opacity <= maxOpacity)
        {
            opacity += fadeRate * Time.deltaTime;
        }
    }

    public void DoFadeOut(ref float opacity, float fadeRate)
    {
        if (opacity >= 0)
        {
            opacity -= fadeRate * Time.deltaTime;
        }
    }

    public void DoFadeOut(ref float opacity, float fadeRate, float minOpacity)
    {
        if (opacity >= minOpacity)
        {
            opacity -= fadeRate * Time.deltaTime;
        }
    }

    public void DoFade(ref float opacity, float fadeRate, float upTime)
    {
        if (isFadeIn &&
            !isFadeOut)
        {
            DoFadeIn(ref opacity, fadeRate);

            if (opacity >= 1)
            {
                delay = upTime + Time.time;
                isFadeIn = false;
                isFadeOut = true;
            }
        }

        if (isFadeOut &&
            !isFadeIn &&
            delay < Time.time)
        {
            DoFadeOut(ref opacity, fadeRate);
        }
    }

    public void DoFade(ref float opacity, float fadeRate, float upTime, float maxOpacity)
    {
        if (isFadeIn &&
            !isFadeOut)
        {
            DoFadeIn(ref opacity, fadeRate, maxOpacity);

            if (opacity >= maxOpacity)
            {
                delay = upTime + Time.time;
                isFadeIn = false;
                isFadeOut = true;
            }
        }

        if (isFadeOut &&
            !isFadeIn &&
            delay < Time.time)
        {
            DoFadeOut(ref opacity, fadeRate);
        }
    }

    public void DoFade(ref float opacity, float fadeRate, float upTime, float minOpacity, float maxOpacity)
    {
        if (isFadeIn &&
            !isFadeOut)
        {
            DoFadeIn(ref opacity, fadeRate, maxOpacity);

            if (opacity >= maxOpacity)
            {
                delay = upTime + Time.time;
                isFadeIn = false;
                isFadeOut = true;
            }
        }

        if (isFadeOut &&
            !isFadeIn &&
            delay < Time.time)
        {
            DoFadeOut(ref opacity, fadeRate, minOpacity);
        }
    }

    public void ResetFader()
    {
        isFadeIn = true;
        isFadeOut = false;
        delay = 0;
    }
}
