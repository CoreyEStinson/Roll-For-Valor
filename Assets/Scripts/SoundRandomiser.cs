using UnityEngine;

public class SoundRandomiser : MonoBehaviour
{

    public AudioClip[] sounds;
    private AudioSource source;

    [Range(0.1f, 0.5f)]
    public float volumeChangeMultiplier = 0.2f;
    [Range(0.1f, 0.5f)]
    public float pitchChangeMultiplier = 0.2f;
    [Range(0f, 1f)]
    public float volumeMinus = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    
    public void PlaySound()
    {
        source.clip = sounds[Random.Range(0, sounds.Length)];
        source.volume = Random.Range(1 - volumeChangeMultiplier, 1) - volumeMinus;
        source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        source.PlayOneShot(source.clip);
    }
}
