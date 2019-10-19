using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFX_Randomizer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float duration;
    private AudioSource source;

    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }

    private IEnumerator DestroyOnDuration(float time)
    {
        source.Play();
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        int i = Random.Range(0, clips.Length);
        source.PlayOneShot(clips[i]);

        if (duration > 0)
        {
            StartCoroutine(DestroyOnDuration(duration));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (duration <= 0)
        {
            if (!source.isPlaying) Destroy(this.gameObject);
        }
    }
}
