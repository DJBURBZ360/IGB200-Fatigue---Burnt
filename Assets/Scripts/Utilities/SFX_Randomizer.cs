using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFX_Randomizer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        int i = Random.Range(0, clips.Length);
        source.PlayOneShot(clips[i]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying) Destroy(this.gameObject);
    }
}
