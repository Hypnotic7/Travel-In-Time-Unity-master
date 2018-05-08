using System;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class AudioInteract : MonoBehaviour, IAudio
    {
        public AudioClip[] MusicClips;
        public AudioSource MusicSource;
        public bool Interacted;

        void Start()
        {
            Interacted = false;
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }
    }
}
