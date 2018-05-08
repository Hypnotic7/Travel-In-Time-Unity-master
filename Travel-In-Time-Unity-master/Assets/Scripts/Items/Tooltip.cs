using TMPro;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Tooltip : MonoBehaviour
    {
        private Item item;
        private string data;
        private GameObject tooltip;

        void Start()
        {
            tooltip = GameObject.Find("Tooltip");
            tooltip.SetActive(false);
        }

        //If the tooltip is active sets the position to the mouse position
        void Update()
        {
            if (tooltip.activeSelf)
            {
                tooltip.transform.position = Input.mousePosition;
            }
        }

        //Activates the tool tip
        public void Activate(Item item)
        {
            this.item = item;
           
            ConstructDataString();
            tooltip.SetActive(true);
        }

        //Deactivates the tool tip
        public void Deactivate()
        {
            tooltip.SetActive(false);
        }

        //Constructs the string for a tooltip
        public void ConstructDataString()
        {
            data = "<color=#000000>" + item.Title + "</color>";
            tooltip.transform.GetChild(0).GetComponent<TMP_Text>().text = data;
            tooltip.transform.GetChild(1).GetComponent<TMP_Text>().text = item.Description;

        }
    }
}
