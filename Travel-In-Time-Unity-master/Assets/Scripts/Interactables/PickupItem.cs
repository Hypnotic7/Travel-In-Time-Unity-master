using Assets.Scripts;
using Assets.Scripts.Inventory;
using UnityEngine;

public class PickupItem : Interactable
{
    public int ItemID;
    private Color startcolor;

    //Checks to destroy already picked up items
    void Start()
    {
        if (gameObject.name == "vinyl" && GameplayChecker.VinylPickUpSouth)
           Destroy(gameObject);
        if (gameObject.name == "vinyl1" && GameplayChecker.VinylPickUpWest)
            Destroy(gameObject);
        if (gameObject.name == "vinyl2" && GameplayChecker.VinylPickUpEast)
            Destroy(gameObject);
        if (gameObject.name == "vinyl3" && GameplayChecker.VinylPickUpNorthEast)
            Destroy(gameObject);
        if (GameplayChecker.EmptyFlaskPickedUp && gameObject.name == "Flask")
            Destroy(gameObject);
    }

    //Whenever the pick up item is hovered over it changes the material of the object
    void OnMouseEnter()
    {
        startcolor = this.GetComponent<Renderer>().material.color;
        this.GetComponent<Renderer>().material.color = Color.magenta;

    }

    //Whenever it's not hovered over anymore it changes back the material to the start color
    void OnMouseExit()
    {
        this.GetComponent<Renderer>().material.color = startcolor;
    }

    //Interaction with item sets the gameplay flag and adds the item to the inventory
    public override void Interact()
    {
        var inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        if (inventory == null) return;


        if (inventory.items.Exists(f => f.ID == ItemID))
        {
            if (ItemID == 9)
            {
                inventory.AddItem(ItemID);
                if (gameObject.name == "vinyl")
                    GameplayChecker.VinylPickUpSouth = true;
                else if (gameObject.name == "vinyl1")
                    GameplayChecker.VinylPickUpWest = true;
                else if (gameObject.name == "vinyl2")
                    GameplayChecker.VinylPickUpEast = true;
                else if (gameObject.name == "vinyl3")
                    GameplayChecker.VinylPickUpNorthEast = true;

                Destroy(this.gameObject);
            }
        }else if (!inventory.items.Exists(f => f.ID == ItemID))
        {
            inventory.AddItem(ItemID);
            if (ItemID == 6)
            {
                GameplayChecker.EmptyFlaskPickedUp = true;
                GameObject.Find("Flask").SetActive(false);
            }
                
            if (ItemID == 9)
            {
                if (gameObject.name == "vinyl")
                    GameplayChecker.VinylPickUpSouth = true;
                else if (gameObject.name == "vinyl1")
                    GameplayChecker.VinylPickUpWest = true;
                else if (gameObject.name == "vinyl2")
                    GameplayChecker.VinylPickUpEast = true;
                else if (gameObject.name == "vinyl3")
                    GameplayChecker.VinylPickUpNorthEast = true;
                Destroy(gameObject);
            }
              
            
        }



    }
}
