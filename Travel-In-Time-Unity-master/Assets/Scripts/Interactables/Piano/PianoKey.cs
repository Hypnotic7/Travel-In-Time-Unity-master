using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Interactables.Piano
{
    public class PianoKey : MonoBehaviour, IPointerClickHandler
    {
        private PianoInteraction piano;
        private int keyClicked;

        void Start()
        {
            piano = GameObject.Find("Piano_Interaction_Panel(Clone)").GetComponent<PianoInteraction>();
        }

        //Checks which piano key was clicked
        public void OnPointerClick(PointerEventData eventData)
        {
            string numberKeyClicked = this.gameObject.name.Substring(8);

            if (numberKeyClicked.Length == 3)
            {
                int.TryParse(numberKeyClicked[0].ToString(), out keyClicked);
            }
            else if (numberKeyClicked.Length == 5)
            {
                int.TryParse(numberKeyClicked[0].ToString() + numberKeyClicked[1].ToString(), out keyClicked);
            }
            else
            {
                int.TryParse(numberKeyClicked, out keyClicked);
            }
            piano.DisplayOutput(keyClicked);
        }
    }
}
