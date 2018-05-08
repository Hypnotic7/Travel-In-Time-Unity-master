using Assets.Scripts.Audio;
using UnityEngine;

public class AudioBackground : MonoBehaviour, IAudio
{
    public AudioClip MusicClip;
    public AudioSource MusicSource;
    public bool isPlaying;
    public bool Interacted;

    void Start()
    {
        MusicSource.clip = MusicClip;
        isPlaying = false;
        Interacted = false;
    }

    //Checks is the audio background playing if not plays it
    void Update()
    {
        if(!isPlaying)
            Play();
    }

    //Play audio background
    public void Play()
    {
        MusicSource.Play();
        isPlaying = true;
    }

    public void Pause()
    {
        throw new System.NotImplementedException();
    }
}
