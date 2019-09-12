using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ItemSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips = new AudioClip[4];
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        if (clips.Length > 0)
        {
            source = gameObject.GetComponent<AudioSource>();
            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
}
