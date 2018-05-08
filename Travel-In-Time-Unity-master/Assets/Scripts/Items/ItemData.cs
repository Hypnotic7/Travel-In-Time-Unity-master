using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Wilberforce;

namespace Assets.Scripts.Items
{
    public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler
    {
        public int amount;
        public bool ChangedScenes;
        private Inventory.Inventory inv;
        public Item item;
        public string levelToLoad;

        private Vector2 offset;
        public int slot;
        private float timeStamp;
        private Tooltip tooltip;

        //Executed when the items started being dragged
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log(item.ID);
            if (item.ID != -1)
            {
                offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
                transform.position = eventData.position - offset;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }

        //Executed when the item is dragged
        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log(item.ID);
            if (item.ID != -1)
                transform.position = eventData.position - offset;
        }

        //Executed when the item has stopped being dragged
        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(inv.slots[slot].transform);
            transform.position = inv.slots[slot].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        //Executed when the item has been clicked
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!item.IsCoolingdown)
                if (item.ID == 0)
                    ActivateTimeWatch();
                else if (item.ID == 8)
                    ActivateInvisibilityFlask();
                else if (item.ID == 13)
                    ActivatePuzzle();
        }

        //When the mouse is being hovered over the item
        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.Activate(item);
        }
        //When the mouse is not being hovered over the item
        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.Deactivate();
        }

        private void Start()
        {
            inv = GameObject.Find("Inventory").GetComponent<Inventory.Inventory>();
            tooltip = inv.GetComponent<Tooltip>();
        }


        //If the item is being used the cooldown is set.
        private void Update()
        {
            if (item.IsCoolingdown)
                if (timeStamp <= Time.time)
                {
                    transform.GetComponent<Image>().color = Color.white;
                    inv.slots[item.ID].GetComponent<Image>().color = Color.white;
                    item.IsCoolingdown = false;
                }
        }

        //Activates the time watch and manages changing between Past and Present scenes.
        public void ActivateTimeWatch()
        {
            if (!item.IsCoolingdown)
            {
                var go = GameObject.Find("GameManager");
                if (go == null)
                {
                    Debug.LogError("Failed to find an object named 'GameManager'");
                    enabled = false;
                    return;
                }

                if (GameplayChecker.CurrentTime.Equals(string.Empty))
                    GameplayChecker.CurrentTime = "Past_Time_Test";

                var gm = go.GetComponent<GameManager>();

                item.IsCoolingdown = true;
                timeStamp = Time.time + item.CooldownInSeconds;
                transform.GetComponent<Image>().color = Color.white;
                inv.slots[item.ID].GetComponent<Image>().color = Color.yellow;

                if (GameplayChecker.CurrentTime.Contains("Present"))
                {
                    GameplayChecker.CurrentTime = "Past_Time_Test";
                    gm.LoadScene(GameplayChecker.CurrentTime);
                }
                else if (GameplayChecker.CurrentTime.Contains("Past"))
                {
                    GameplayChecker.CurrentTime = "Present_Time_Test";
                    gm.LoadScene(GameplayChecker.CurrentTime);
                }

                if(InteractableManager.interactableManager.currentWindow != null)
                InteractableManager.interactableManager.DeactivateInteraction();
            }
        }

        //Activates invisibility mode, vinyls and notes that are not visibile
        public void ActivateInvisibilityFlask()
        {
            var colorBlind = GameObject.Find("Main Camera(Clone)").GetComponent<Colorblind>();
            colorBlind.enabled = !colorBlind.enabled;
            if (GameplayChecker.CurrentTime.Contains("Past") && colorBlind.enabled)
            {
                var vinyls = GameObject.Find("Vinyls").transform;
                for (var i = 0; i < vinyls.childCount; i++)
                {
                    vinyls.GetChild(i).gameObject.SetActive(colorBlind.enabled);
                    GameplayChecker.InvisibilityMode = colorBlind.enabled;
                }
            }
            else if (GameplayChecker.CurrentTime.Contains("Past") && !colorBlind.enabled)
            {
                var vinyls = GameObject.Find("Vinyls").transform;
                for (var i = 0; i < vinyls.childCount; i++)
                {
                    vinyls.GetChild(i).gameObject.SetActive(colorBlind.enabled);
                    GameplayChecker.InvisibilityMode = colorBlind.enabled;
                }
            }
            else if (GameplayChecker.CurrentTime.Contains("Present"))
            {
                var notes = GameObject.Find("Notes").transform;
                for (var i = 0; i < notes.childCount; i++)
                {
                    notes.GetChild(i).gameObject.SetActive(colorBlind.enabled);
                    GameplayChecker.InvisibilityMode = colorBlind.enabled;
                }
            }
            var invisibilityOnOff = colorBlind.enabled ? 1 : 0;
            PlayerPrefs.SetInt("Invisibility", invisibilityOnOff);
        }


        //Activates the puzzle item
        public void ActivatePuzzle()
        {
            var interactableManager = GameObject.Find("Interaction").GetComponent<InteractableManager>();
            if (interactableManager == null) return;
            interactableManager.currentWindow = gameObject;
            interactableManager.Activate("Puzzle");
        }
    }
}