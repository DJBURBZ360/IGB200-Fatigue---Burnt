using UnityEngine;

/// <summary>
/// Cleans up SFX prefabs when the clip played has ended.
/// </summary>
public class SFX_Cleanup : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!source.isPlaying) Destroy(this.gameObject);
    }
}
