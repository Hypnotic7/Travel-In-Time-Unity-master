using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Interactables.Craft
{
    public class CraftVisiblityFlask : MonoBehaviour, ICraft<GameObject>, IPuzzle
    {
        public GameObject[] gameObjects = new GameObject[2];
        public int[] ingrediantsID;
        public int rewardID;

        //Checks for ingrediants needed to craft visibility flask
        public bool CheckForIngrediants()
        {
            if (gameObjects == null) return false;

            var counter = 0;
            var gameObjectsText = new string[gameObjects.Length];
            var contains = new bool[gameObjects.Length];
            for (var i = 0; i < gameObjects.Length; i++)
            {
                gameObjectsText[i] = gameObjects[i].transform.GetChild(1).transform.GetComponent<TMP_Text>().text;
                contains[i] = checkInventory(gameObjectsText[i]);
                if (contains[i]) counter++;
            }

            if (counter == gameObjects.Length)
            {
                Reward();
            }
            else
            {
                var count = 1;
                for (var i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[count].GetComponent<Image>().color = contains[i] ? Color.green : Color.red;
                    count--;
                }
            }
            return false;
        }

        //Adds the item to the inventory
        public void Reward()
        {
            removeItemsThatWasNeeded();
            GameObject.Find("Inventory").GetComponent<Inventory.Inventory>().AddItem(rewardID);
            GameplayChecker.CraftedInvisiblityFlask = true;
            Destroy(GameObject.Find("Craft_Interaction_Panel(Clone)"));
            GameObject.Find("Interaction").GetComponent<InteractableManager>().DeactivateInteraction();
        }

        //Crafts the invisibility flask by checking the ingrediants needed to craft it
        public void Craft()
        {
            CheckForIngrediants();
        }

        //Checks the inventory for the item
        private bool checkInventory(string gameObjectsText)
        {
            return GameObject.Find("Inventory").GetComponent<Inventory.Inventory>().items
                .Exists(f => f.Title == gameObjectsText);
        }

        //Removes the item from the inventory
        private void removeItemsThatWasNeeded()
        {
            for (var i = 0; i < ingrediantsID.Length; i++)
                GameObject.Find("Inventory").GetComponent<Inventory.Inventory>().RemoveItem(ingrediantsID[i]);
        }

        public void Clean()
        {
            //do nothing
        }
    }
}