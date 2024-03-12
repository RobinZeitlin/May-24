using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public int currentTrack = 0;
    public AudioClip[] clips;
    private void Start()
    {
        clips = Resources.LoadAll<AudioClip>("Music/") as AudioClip[];

        if(clips.Length > 0)
        {
            PlaySound(clips[currentTrack]);
            currentTrack++;
        }
        else
        {
            Debug.LogError("No music found in Resources/Music/");
        }
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource, clip.length);
    }
}
