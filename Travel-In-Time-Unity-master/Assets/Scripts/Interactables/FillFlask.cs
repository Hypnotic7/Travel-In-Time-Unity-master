using Assets.Scripts.Inventory;
using UnityEngine;

public class FillFlask : Interactable
{
    public int ItemID;
    private Color startcolor;

    //Whenever the flask item is hovered over it changes the material of the object
    void OnMouseEnter()
    {
        startcolor = this.transform.GetComponent<Renderer>().material.color;
        this.transform.GetComponent<Renderer>().material.color = Color.magenta;
    }

    //Whenever the flask item is not hovered over it changes the material color to the start color
    void OnMouseExit()
    {
        this.transform.GetComponent<Renderer>().material.color = startcolor;
    }

    //Picks up the flask and removes it from the scene
    public override void Interact()
    {
        var inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        if (inventory == null) return;

        if (inventory.items.Exists(f => f.ID == ItemID))
        {
            inventory.RemoveItem(ItemID);
            inventory.AddItem(7);
        }
    }
}
