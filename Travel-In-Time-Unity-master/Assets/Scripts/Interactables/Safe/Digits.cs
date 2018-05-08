using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Interactables.Safe
{
    public class Digits : MonoBehaviour, IPointerClickHandler
    {
        private Safe safe;
        private int numberClicked;
        void Start()
        {
            safe = GameObject.Find("Safe_Interaction_Panel(Clone)").GetComponent<Safe>();
        }

        //Gets the number from button clicked
        public void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log("HELLOO" + this.transform.GetChild(0).GetComponent<TMP_Text>().text);
            int.TryParse(this.transform.GetChild(0).GetComponent<TMP_Text>().text.Trim(), out numberClicked);
            safe.DisplayPinNumbers(numberClicked);
        }
    }

}
