using System.Collections.Generic;
using System.IO;
using Assets.Scripts.DataAccess.Constants;
using Assets.Scripts.Items;
using LitJson;
using UnityEngine;

namespace Items
{
    public class ItemDatabase : MonoBehaviour
    {
        private List<Item> database = new List<Item>();
        private JsonData itemData;

        //Reads all text from json file.
        void Start()
        {
            itemData = JsonMapper.ToObject(File.ReadAllText(TravelInTimeConstants.ItemsDatabase));
            ConstructionDatabase();

            Debug.Log(database[1].Title);
        }


        //Finds the item by id
        public Item FetchItemByID(int id)
        {
            for (int i = 0; i < database.Count; i++)
                if (database[i].ID == id)
                    return database[i];
                return null;
            
            
        }

        //Constructs the database with items
        public void ConstructionDatabase()
        {
            for (int index = 0; index < itemData.Count; index++)
            {
                database.Add(new Item((int)itemData[index]["id"], itemData[index]["title"].ToString(),
                                      (int)itemData[index]["value"],(bool)itemData[index]["stackable"],
                                      itemData[index]["slug"].ToString(),itemData[index]["description"].ToString(),
                                      (int)itemData[index]["cooldown_in_seconds"]));

            }
        }
    }
}
