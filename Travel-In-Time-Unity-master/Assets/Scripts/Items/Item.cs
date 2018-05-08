using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Item 
    {
        public int ID { get; private set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public Sprite Sprite { get; set; }
        public bool Stackable { get; set; }
        public string Slug { get; set; }
        public string Draggable { get; set; }
        public bool ChangedScenes { get; set; }
        public bool IsCoolingdown { get; set; }
        public int CooldownInSeconds { get; set; }
        public string Description { get; set; }

        public Item(int id, string title, int value, bool stackable, string slug,string description,int cooldownInSeconds)
        {
            this.ID = id;
            this.Title = title;
            this.Value = value;
            this.Stackable = stackable;
            this.Slug = slug;
            this.Description = description;
            this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug) as Sprite;
            CooldownInSeconds = cooldownInSeconds;
        }

        public Item()
        {
            ID = -1;
        }

    }
}
