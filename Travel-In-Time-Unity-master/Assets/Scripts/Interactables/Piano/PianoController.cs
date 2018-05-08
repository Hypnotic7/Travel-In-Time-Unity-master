using Assets.Scripts.Interactables.Lever;
using UnityEngine;

namespace Assets.Scripts.Interactables.Piano
{
    public class PianoController : Interactable
    {
        public bool isPlaying;
        private bool isActive;
        public AudioSource MusicSource;
        private Color startcolor;

        //Whenever the piano is hovered over it changes the material of the object
        void OnMouseEnter()
        {
            startcolor = transform.GetChild(0).GetComponent<Renderer>().material.color;
            transform.GetChild(0).GetComponent<Renderer>().material.color = Color.magenta;
        }

        //Whenever the pictures on the wall is not hovered over it changes the material of the object
        void OnMouseExit()
        {
            transform.GetChild(0).GetComponent<Renderer>().material.color = startcolor;
        }

        //Whenever the piano has been interacted depending on the current time.
        //If it is the past it checks is the lever active and plays the final melody.
        //If it present it activates the piano puzzle
        public override void Interact()
        {
            if (GameplayChecker.CurrentTime.Contains("Past") && !isPlaying)
            {
                isActive = GameObject.Find("Lever_Right").GetComponent<LeverRight>().activated;

                if (isActive)
                    Play();
            }
            else
            {
                if (!GameplayChecker.PianoPuzzleSolved)
                {
                    var interactableManager = GameObject.Find("Interaction").GetComponent<InteractableManager>();
                    interactableManager.Activate("Piano");
                }
            }
        }

        //Plays the melody
        public void Play()
        {
            isPlaying = true;
            MusicSource.Play();
            isPlaying = false;
        }
    }
}
