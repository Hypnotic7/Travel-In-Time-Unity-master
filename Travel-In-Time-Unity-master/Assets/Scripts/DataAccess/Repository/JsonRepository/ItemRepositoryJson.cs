using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Assets.Scripts.DataAccess.Constants;
using Assets.Scripts.DataAccess.Repository.Item;

namespace Assets.Scripts.DataAccess.Repository.JsonRepository
{
    public class ItemRepositoryJson : JsonRepository, IDataAccess<ItemEntity>
    {
        public static readonly Func<string, ItemRepositoryJson> CreateItemRepository = c => new ItemRepositoryJson();
        public List<ItemEntity> ItemsDatabase { get; set; }
        public string CollectionName
        {
            get { return CollectionName; }
            set { this.CollectionName = "Items"; }
        }

        //Adds item entity to the database
        public void Add(ItemEntity entity)
        {
            try
            {
                var itemData = OpenFileAndReadData(TravelInTimeConstants.ItemsDatabase).GetCollection();
                for (var i = 0; i < itemData.Count; i++)
                {
                    ItemsDatabase.Add(new ItemEntity((int) itemData[i]["id"], itemData[i]["title"].ToString(),
                        (int) itemData[i]["value"], (bool) itemData[i]["stackable"],
                        itemData[i]["slug"].ToString()));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("something went wrong with adding the item");
            }
        }

        public void Remove(ItemEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Modify(ItemEntity entity)
        {
            throw new NotImplementedException();
        }

        //Finds the item by ID
        public ItemEntity Fetch(ItemEntity property)
        {
            for (int i = 0; i < ItemsDatabase.Count; i++)
                if (ItemsDatabase[i].ID == property.ID)
                    return ItemsDatabase[i];
                return null;
        }
    }
}
