using UnityEngine;

namespace Assets.Scripts.Interactables.Craft
{
    public class CraftController : Interactable
    {
        private Color startcolor;

        //Whenever the Alchemy Table is hovered over it changes the material of the object
        private void OnMouseEnter()
        {
            startcolor = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = Color.magenta;
        }

        //Whenever the Alchemy table is not hovered over it changes the material of the object
        private void OnMouseExit()
        {
            GetComponent<Renderer>().material.color = startcolor;
        }

        //Whenever the user interacts with alchemy table it activates the craft puzzle.
        public override void Interact()
        {
            if (!GameplayChecker.CraftedInvisiblityFlask)
            {
                var interactableManager = GameObject.Find("Interaction").GetComponent<InteractableManager>();
                interactableManager.Activate("Craft");
            }

        }
    }
}
