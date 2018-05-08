using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Interactables.Pictures
{
    public class PicturesOnTheWall : Interactable
    {
        public InteractableManager InteractableManager;
        public string FinalCombination;
        private Color startcolor;


        //Whenever the pictures on the wall is hovered over it changes the material of the object
        void OnMouseEnter()
        {
            for (int i = 0; i < 3; i++)
            {
                startcolor = transform.GetChild(i).GetComponent<Renderer>().material.color;
                transform.GetChild(i).GetComponent<Renderer>().material.color = Color.magenta;
            }
        }
        //Whenever the pictures on the wall is not hovered over it changes the material of the object
        void OnMouseExit()
        {
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = startcolor;
            }
        }

        //Pictures on the wall interacted and it initializes/activates the puzzle interaction window
        public override void Interact()
        {
            var inv = GameObject.Find("Inventory").GetComponent<Inventory.Inventory>();
            if (inv != null)
            {
                if (GameplayChecker.CurrentTime.Contains("Past"))
                {
                    if(!GameplayChecker.PicturesPastPuzzleSolved)
                        InstantiateAndActivate();
                }

                else if (GameplayChecker.CurrentTime.Contains("Present"))
                {
                    if(!GameplayChecker.PicturesPresentPuzzleSolved)
                    InstantiateAndActivate();
                }
            }
        }

        //Activates the puzzle interaction window
        private void InstantiateAndActivate()
        {
            InteractableManager = GameObject.Find("Interaction").GetComponent<InteractableManager>();
            InteractableManager.Activate("Pictures");
        }
    }
}
