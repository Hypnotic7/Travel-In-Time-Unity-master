using Assets.Scripts.Inventory;
using UnityEngine;

public class Chest : Interactable
{
    public bool ContainsKey;
    public bool OpenClose;
    public bool IsTriggered;
    public int counter;

    private Color startcolor;

    // Use this for initialization
    void Start()
    {
        IsTriggered = false;
        ContainsKey = false;
        counter = 0;
    }

    //Whenever the chest is hovered over it changes the material of the object
    void OnMouseEnter()
    {
        startcolor = transform.parent.GetComponent<Renderer>().material.color;
        transform.parent.GetComponent<Renderer>().material.color = Color.magenta;
        this.GetComponent<Renderer>().material.color = Color.magenta;
    }

    //Whenever the chest is not hovered over it changes the material of the object
    void OnMouseExit()
    {
        transform.parent.GetComponent<Renderer>().material.color = startcolor;
        this.GetComponent<Renderer>().material.color = startcolor;
    }
    
    //Checks for the key
    public bool CheckForKey()
    {
        var inv = GameObject.Find("Inventory").GetComponent<Inventory>();

        if (inv != null)
        {
            foreach (var item in inv.items)
            {
                if (gameObject.name == "Chest_Lid1" && item.ID == 3)
                {
                    ContainsKey = true;
                }

                if (gameObject.name == "Chest_Lid2" && item.ID == 10)
                {
                    ContainsKey = true;
                }
            }
        }
        return ContainsKey;
    }

    //Whenever user interacts with the chest it opens/closes it and add the item if its not in the inventory
    public override void Interact()
    {
        IsTriggered = true;

        if (CheckForKey())
        {
            if (IsTriggered)
            {
                if (ContainsKey)
                {
                    var inv = GameObject.Find("Inventory").GetComponent<Inventory>();
                    if (OpenClose && counter != 0)
                    {
                        transform.Rotate(60, 0, 0, Space.Self);
                        OpenClose = !OpenClose;
                        if (gameObject.name == "Chest_Lid1")
                        {
                            if (!inv.items.Exists(f => f.ID == 4))
                            {
                                inv.AddItem(4);
                            }
                        }
                        else if (gameObject.name == "Chest_Lid2")
                        {
                            if (!inv.items.Exists(f => f.ID == 13))
                                inv.AddItem(13);
                        }
                    }
                    else if (!OpenClose)
                    {
                        transform.Rotate(-60, 00, 0, Space.Self);
                        OpenClose = !OpenClose;
                        IsTriggered = false;
                        if (gameObject.name == "Chest_Lid1")
                        {
                            if (!inv.items.Exists(f => f.ID == 4))
                                inv.AddItem(4);
                        }
                        else if (gameObject.name == "Chest_Lid2")
                        {
                            if (!inv.items.Exists(f => f.ID == 13))
                                inv.AddItem(13);
                        }
                    }
                    counter++;
                }
            }
        }
    }
}