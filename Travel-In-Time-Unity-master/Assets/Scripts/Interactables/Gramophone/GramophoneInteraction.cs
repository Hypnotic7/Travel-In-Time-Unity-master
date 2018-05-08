using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interactables.Gramophone
{
    public class GramophoneInteraction : MonoBehaviour, IPuzzle
    {
        public const string Answer = "3241";
        private const int coolDownPeriodInSeconds = 3;
        public TMP_Text GramophoneText;
        private readonly int[] input = new int[4];
        private bool IsCoolingDown;
        public List<GameObject> outputSlots;
        public List<Sprite> sprites;
        private float timeStamp;
        public List<GameObject> vinylButtons;

        //Sets output values with the corresponding image and checks if the puzzle is solved
        public void SetOutputSlot(int vinylClicked)
        {
            for (var i = 0; i < outputSlots.Count; i++)
            {
                var currentSprite = outputSlots[i].transform.GetChild(0).GetComponent<Image>().sprite;
                if (currentSprite == null)
                {
                    outputSlots[i].transform.GetChild(0).gameObject.SetActive(true);
                    outputSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = sprites[vinylClicked];
                    input[i] = vinylClicked + 1;
                    if (i == outputSlots.Count - 1)
                    {
                        if (CheckIfSolved())
                        {
                            outputSlots.ForEach(f => f.GetComponent<Image>().color = Color.green);
                            GameplayChecker.GramophonePuzzle = true;
                            GramophoneText.text =
                                "You have successfully played records. The window will close in 3 seconds.";
                            reward(10);
                        }
                        else
                        {
                            GramophoneText.text =
                                "Unfortunately, the records that you have played are incorrect. The puzzle will restart in 3 seconds.";
                            outputSlots.ForEach(f => f.GetComponent<Image>().color = Color.red);
                        }
                        timeStamp = Time.time + coolDownPeriodInSeconds;
                        IsCoolingDown = true;
                    }
                    return;
                }
            }
        }

        //Adds the reward item do the inventory
        private void reward(int itemID)
        {
            GameObject.Find("Inventory").GetComponent<Inventory.Inventory>().AddItem(itemID);
        }

        //Cleans the output values
        public void Clean()
        {
            for (var i = 0; i < outputSlots.Count; i++)
            {
                outputSlots[i].transform.GetChild(0).gameObject.SetActive(false);
                //outputSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = new Sprite();
                input[i] = 0;
            }
        }


        //Checks was the puzzle solved if yes it Deactivates the interaction and cleans it
        //if it was not solved it restarts the puzzle
        private void Update()
        {
            if (IsCoolingDown)
                if (timeStamp <= Time.time)
                    if (GameplayChecker.GramophonePuzzle)
                    {
                        Clean();
                        Destroy(gameObject);
                        GameObject.Find("Interaction").GetComponent<InteractableManager>().DeactivateInteraction();
                        IsCoolingDown = false;
                    }
                    else
                    {
                        Clean();
                        IsCoolingDown = false;
                        outputSlots.ForEach(f => f.GetComponent<Image>().color = Color.white);
                        GramophoneText.text =
                            "Play the records in the correct order to the notes that you can find on the wall by the gramophone.";
                    }
        }

        //Checks was the puzzle solved
        private bool CheckIfSolved()
        {
            var inputAnswer = string.Join("", input);
            Debug.Log(inputAnswer);
            return inputAnswer.Equals(Answer);
        }
    }
}