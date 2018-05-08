using Assets.Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Interactables.Gramophone
{
    public class UnlockPuzzle : MonoBehaviour, IPuzzle
    {
        public int Amount;
        public int RequirementID;
        public GameObject RequirementObject;

        //Checks for ingrediants
        public void Unlock()
        {
            CheckForRequirements();
        }

        //Checks ingrediants and removes the vinyls from the inventory
        public bool CheckForRequirements()
        {
            if (RequirementObject == null) return false;

            var counter = 0;
            if (RequirementObject.transform.GetChild(1) != null)
            {
                var requirementText = RequirementObject.transform.GetChild(1).GetComponent<TMP_Text>().text
                    .Substring(0, 5);
                var contains = CheckInventory(requirementText);
                if (!contains) return false;
                contains = CheckForAmount(Amount);
                if (!contains) return false;
                removeItemsThatWasNeeded();
                deactivateAndActivate("Gramophone");
            }
            return true;
        }

        //Activates or Deactivates the interaction window
        private void deactivateAndActivate(string interaction)
        {
            GameplayChecker.GramophoneVinylsUnlockPuzzle = true;
            GameObject.Find("Interaction").GetComponent<InteractableManager>().Activate(interaction);
            Destroy(GameObject.Find("Vinyl_Interaction_Panel(Clone)"));
        }

        //removes the item from inventory
        private void removeItemsThatWasNeeded()
        {
            GameObject.Find("Inventory").GetComponent<Inventory.Inventory>().RemoveItem(RequirementID);
        }

        //Checks the amount of the vinyls
        private bool CheckForAmount(int amount)
        {
            var inv = GameObject.Find("Inventory").GetComponent<Inventory.Inventory>();

            for (var i = 0; i < inv.items.Count; i++)
                if (inv.items[i].ID == RequirementID)
                {
                    var data = inv.slots[i].transform.GetChild(1).GetComponent<ItemData>();
                    if (data.amount == amount) break;
                    return false;
                }
            return true;
        }

        //Checks does the vinyls are in the inventory
        private bool CheckInventory(string requirementText)
        {
            return GameObject.Find("Inventory").GetComponent<Inventory.Inventory>().items
                .Exists(f => f.Title == requirementText);
        }

        public void Clean()
        {
            //nothing
        }
    }
}