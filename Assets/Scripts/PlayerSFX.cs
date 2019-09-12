using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips = new AudioClip[4];
    [SerializeField] private float walkRate;
    [SerializeField] private float sprintRate;
    private float currentWalkRate;
    private float currentWalkInterval;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.HasSpeedBoost)
        {
            currentWalkRate = sprintRate;
        }
        else
        {
            currentWalkRate = walkRate;
        }

        if (audioClips.Length > 0)
        {
            if (Input.GetButton("Horizontal") ||
                Input.GetButton("Vertical"))
            {
                if (currentWalkInterval < Time.time)
                {
                    currentWalkInterval = currentWalkRate + Time.time;
                    source.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
                }
            }
        }
    }
}
