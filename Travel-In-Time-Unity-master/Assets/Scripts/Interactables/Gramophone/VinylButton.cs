using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Interactables.Gramophone
{
    public class VinylButton : MonoBehaviour, IPointerClickHandler
    {
        private GramophoneInteraction gramophone;
        private int vinylClicked;

        void Start()
        {
            gramophone = GameObject.Find("Gramophone_Interaction_Panel(Clone)").GetComponent<GramophoneInteraction>();
        }
        
        //After vinyl clicked it sets the output slot with the corresponding image
        public void OnPointerClick(PointerEventData eventData)
        {
            string buttonClicked = this.gameObject.name.Substring(6);

            int.TryParse(buttonClicked, out vinylClicked);
            vinylClicked -= 1;
            gramophone.SetOutputSlot(vinylClicked);
        }


    }
}
