using UnityEngine;

namespace Assets.Scripts.DataAccess.Repository.Item
{
    public class ItemEntity
    {
        public int ID { get; private set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public Sprite Sprite { get; set; }
        public bool Stackable { get; set; }
        public string Slug { get; set; }
        public string Draggable { get; set; }
        public bool ChangedScenes { get; set; }

        public ItemEntity(int id, string title, int value, bool stackable, string slug)
        {
            this.ID = id;
            this.Title = title;
            this.Value = value;
            this.Stackable = stackable;
            this.Slug = slug;
            this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug) as Sprite;

        }

        public ItemEntity()
        {
            ID = -1;
        }

    }
}
