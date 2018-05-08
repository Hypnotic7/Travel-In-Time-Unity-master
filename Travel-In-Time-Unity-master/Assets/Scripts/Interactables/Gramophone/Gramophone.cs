using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interactables.Gramophone
{
    public class Gramophone : Interactable, IPuzzle
    {
        public InteractableManager interactableManager;
        private List<Color> startcolor;

        private void Start()
        {
            startcolor = new List<Color>();
        }

        //Whenever the gramohpone is hovered over it changes the material of the object
        private void OnMouseEnter()
        {
            for (var i = 0; i < transform.GetChild(0).childCount; i++)
            {
                startcolor.Add(transform.GetChild(0).transform.GetChild(i).GetComponent<Renderer>().material.color);
                transform.GetChild(0).transform.GetChild(i).GetComponent<Renderer>().material.color = Color.magenta;
            }
        }

        //Whenever the gramohpone is not hovered over it changes the material of the object
        private void OnMouseExit()
        {
            for (var i = 0; i < transform.GetChild(0).childCount; i++)
                transform.GetChild(0).transform.GetChild(i).GetComponent<Renderer>().material.color = startcolor[i];
        }

        //Whenever the gramophone is clicked it initializes the unlock vinyls puzzle if it wasn't solved else it initializes the gramophone notes puzzle
        public override void Interact()
        {
            if (!GameplayChecker.GramophonePuzzle)
            {
                interactableManager = GameObject.Find("Interaction").GetComponent<InteractableManager>();
                interactableManager.currentWindow = gameObject;
                if (GameplayChecker.GramophoneVinylsUnlockPuzzle)
                    interactableManager.Activate("Gramophone");
                else
                    interactableManager.Activate("Vinyl");
            }
        }

        public void Clean()
        {
            //do nothing
        }
    }
}