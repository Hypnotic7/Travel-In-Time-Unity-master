using Assets.Scripts;
using Assets.Scripts.Inventory;
using UnityEngine;

namespace Interactables
{
    public class DoorScript : Interactable
    {

        public bool ContainsKey;
        public bool OpenClose;
        public bool IsTriggered;
        public int counter;
        private Color startcolor;


        // Use this for initialization
        void Start ()
        {
            IsTriggered = false;
            ContainsKey = false;
            counter = 0;

            //Checks are the doors open or not and opens/closes them depending on the result
            if (!GameplayChecker.AreDoorsOpen && GameplayChecker.SafePuzzleSolved && GameplayChecker.FirstTimeOpened)
            {
                ContainsKey = true;
                OpenClose = false;
                counter++;
            }
            else if (GameplayChecker.AreDoorsOpen && GameplayChecker.SafePuzzleSolved)
            {
                OpenDoors();
                OpenClose = true;
                counter++;
            }
               
        }
        //Whenever the doors is hovered over it changes the material of the object
        void OnMouseEnter()
        {
            startcolor = this.GetComponent<Renderer>().material.color;
            this.GetComponent<Renderer>().material.color = Color.magenta;
            
        }
        //Whenever the doors are not hovered over it changes the material of the object
        void OnMouseExit()
        {
            this.GetComponent<Renderer>().material.color = startcolor;
        }

        //Checks is the key in the inventory
        public bool CheckForKey()
        {
            var inv = GameObject.Find("Inventory").GetComponent<Inventory>();
            foreach (var item in inv.items)
            {
                if (item.ID == 2)
                {
                    ContainsKey = true;
                    return ContainsKey;
                }
            }
            return ContainsKey;
        }

        //Interacting with the doors and opens/closes them
        public override void Interact()
        {
            IsTriggered = true;

            if (CheckForKey())
            {
                if (IsTriggered)
                {
                    if (ContainsKey)
                    {
                        if (GameplayChecker.AreDoorsOpen && counter != 0)
                        {
                            CloseDoors();

                        }
                        else if (!GameplayChecker.AreDoorsOpen)
                        {
                            OpenDoors();

                        }
                        counter++;
                    }
                }

            }
        }

        //Open doors
        private void OpenDoors()
        {
            transform.Rotate(0, 90, 0, Space.Self);
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            OpenClose = !OpenClose;
            IsTriggered = false;
            GameplayChecker.FirstTimeOpened = true;
            GameplayChecker.AreDoorsOpen = true;
            
        }

        //Close doors
        private void CloseDoors()
        {
            transform.Rotate(0, -90, 0, Space.Self);
            transform.localScale = new Vector3(0.7f, transform.localScale.y, transform.localScale.z);
            OpenClose = !OpenClose;
            IsTriggered = false;
            GameplayChecker.AreDoorsOpen = false;
        }
    }
           
    }

