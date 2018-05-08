using System.Collections.Generic;
using Assets.Scripts.Items;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public GameObject inventoryPanel;
        public GameObject slotPanel;
        public ItemDatabase database;

        public GameObject inventorySlot;
        public GameObject inventoryItem;

        public const int slotAmount = 7;
        public List<Item> items = new List<Item>();
        public List<GameObject> slots = new List<GameObject>();
        
        //Initializes the inventory, slots and items
        void Start()
        {

            database = GetComponent<ItemDatabase>();

            inventoryPanel = GameObject.Find("Inventory");
            int controlNumber;
            for (int i = 0; i < slotAmount; i++)
            {
                controlNumber = i + 1;
                items.Add(new Item());
                slots.Add(Instantiate(inventorySlot));
                slots[i].GetComponent<Slot>().id = i;
                slots[i].transform.SetParent(inventoryPanel.transform, false);
                slots[i].transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = controlNumber.ToString();
            }


            AddItem(0);
        }

        //Adds item to the inventory
        public void AddItem(int id)
        {
            Item itemToAdd = database.FetchItemByID(id);
            if (itemToAdd.Stackable && CheckForItemInInventory(itemToAdd))
            {
                for (int j = 0; j < items.Count; j++)
                {
                    if (items[j].ID == id)
                    {
                        ItemData data = slots[j].transform.GetChild(1).GetComponent<ItemData>();
                        data.amount++;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].ID == -1)
                    {
                        items[i] = itemToAdd;

                        GameObject itemObj = Instantiate(inventoryItem);
                        itemObj.GetComponent<ItemData>().item = itemToAdd;
                        itemObj.GetComponent<ItemData>().amount = 1;
                        itemObj.GetComponent<ItemData>().slot = i;
                        itemObj.transform.SetParent(slots[i].transform);
                        itemObj.transform.localPosition = Vector2.zero;
                        itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                        itemObj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        itemObj.name = itemToAdd.Title;
                        break;
                    }
                }
            }
        }

        //Removes item from the inventory
        public void RemoveItem(int id)
        {
            Item itemToRemove = database.FetchItemByID(id);

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == itemToRemove)
                {
                    var index = items.IndexOf(itemToRemove);

                    if (index != -1)
                    {
                        items[i] = new Item();
                    }

                    for (int j = 0; j < slots.Count; j++)
                    {
                        if (slots[i].GetComponent<Slot>().transform.GetChild(1) != null)
                        {
                            var item = slots[i].GetComponent<Slot>().transform.GetChild(1);
                            if (item != null)
                            {
                                if (item.GetComponent<ItemData>().item.ID == itemToRemove.ID)
                                {
                                    Destroy(slots[i].transform.GetChild(1).gameObject);
                                }
                            }
                        }
                    }
                }
            }
        }

        //Checks does the item exists in the inventory
        public bool CheckForItemInInventory(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == item.ID)
                    return true;
            }
            return false;
        }
    }
}
