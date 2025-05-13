using UnityEngine;

public class PlaySoundOnGrab : MonoBehaviour
{
    public AudioClip grabSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = grabSound; 
    }

    public void OnGrab()
    {
        audioSource.Play(); 
    }
}