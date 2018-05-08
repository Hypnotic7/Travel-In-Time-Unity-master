using UnityEngine;

namespace Assets.Scripts.Interactables.Safe
{
    public class SafeController : Interactable
    {
        private Color startcolor;

        //Whenever the safe is hovered over it changes the material of the object
        void OnMouseEnter()
        {
            startcolor = this.GetComponent<Renderer>().material.color;
            this.GetComponent<Renderer>().material.color = Color.magenta;
            this.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.magenta;
            this.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.color = Color.magenta;
        }

        //Whenever the safe is not hovered over it changes the material of the object to the default one
        void OnMouseExit()
        {
            this.GetComponent<Renderer>().material.color = startcolor;
            this.transform.GetChild(0).GetComponent<Renderer>().material.color = startcolor;
            this.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.color = startcolor;
        }

        //Whenever interacted with safe
        public override void Interact()
        {
            if (!GameplayChecker.SafePuzzleSolved)
            {   var interactableManager = GameObject.Find("Interaction").GetComponent<InteractableManager>();
                interactableManager.Activate("Safe");
            }
         
        }
    }
}
