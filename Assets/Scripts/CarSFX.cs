using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarSFX : MonoBehaviour
{
    [SerializeField] private AudioClip arrivalSFX;
    [SerializeField] private AudioClip idleSFX;
    [SerializeField] private AudioClip departSFX;
    private AudioSource source;
    private CarManager car;
    private bool forceStop = false;

    // Start is called before the first frame update
    void Start()
    {
        car = gameObject.GetComponent<CarManager>();
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arrivalSFX != null &&
            car.IsArriving &&
            !car.IsParked)
        {
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

        //prevents the audio from playing repeatedly
        if (!source.isPlaying &&
            !forceStop)
        {
            source.Play();
        }
    }
}
