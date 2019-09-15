﻿using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarSFX : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioClip arrivalSFX;
    [SerializeField] private AudioClip idleSFX;
    [SerializeField] private AudioClip departSFX;
    private AudioSource source;
    private CarManager car;
    private bool forceStop = false;
    #endregion

    #region Methods
    private void ChangeClips()
    {
        if (arrivalSFX != null &&
            car.IsArriving &&
            !car.IsParked)
        {
            source.pitch = Random.Range(-0.8f, 1.3f);

            source.clip = arrivalSFX;
            source.loop = false;

            forceStop = false;
        }
        else if (idleSFX != null &&
                 car.IsParked &&
                 !source.isPlaying)
        {
            source.clip = idleSFX;
            source.loop = true;
        }
        else if (departSFX != null &&
                 car.IsDeparting)
        {
            source.clip = departSFX;
            source.loop = false;

            //forces the audio to play once
            if (!forceStop) source.Play();
            forceStop = true;
        }
    }

    /// <summary>
    /// prevents the audio from playing repeatedly
    /// </summary>
    private void LimitAudioPlayback()
    {
        
        if (!source.isPlaying &&
            !forceStop)
        {
            source.Play();
        }
    }

    private void MuteOnPause()
    {
        if (Time.timeScale == 0)
        {
            source.mute = true;
        }
        else
        {
            source.mute = false;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        car = gameObject.GetComponent<CarManager>();
        source = gameObject.GetComponent<AudioSource>();
        source.pitch = Random.Range(-1.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeClips();
        LimitAudioPlayback();
        MuteOnPause();
    }
}
