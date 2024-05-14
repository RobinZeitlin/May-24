using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public int currentTrack = 0;
    public AudioClip[] clips;
    private AudioSource audioSource;
    private bool isMuted = false;

    private void Start()
    {
        clips = Resources.LoadAll<AudioClip>("Music/") as AudioClip[];

        if (clips.Length > 0)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = isMuted ? 0f : 0.5f;
            StartCoroutine(PlayMusic());
        }
        else
        {
            Debug.LogError("No music found in Resources/Music/");
        }
    }

    private IEnumerator PlayMusic()
    {
        while (true)
        {
            PlaySound(clips[currentTrack]);
            yield return new WaitForSeconds(clips[currentTrack].length);
            currentTrack = (currentTrack + 1) % clips.Length;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        audioSource.volume = isMuted ? 0f : 0.5f;
    }
}
