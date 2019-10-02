using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot normal;
    [SerializeField] private AudioMixerSnapshot paused;

    // Update is called once per frame
    void Update()
    {
        if (normal != null &&
            paused != null)
        {
            if (Time.timeScale == 1)
            {
                normal.TransitionTo(0);
            }
            else
            {
                paused.TransitionTo(0);
            }
        }
    }
}
