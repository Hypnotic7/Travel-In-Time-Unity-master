using System;
using Assets.Scripts.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interactables.Piano
{
    public class PianoInteraction : Interactable, IAudio, IPuzzle
    {
        private const string output = "F5B5A5B5F5";
        private const int coolDownPeriodInSeconds = 3;
        public AudioClip[] audioClips = new AudioClip[12];
        public AudioSource audioSource;
        public string[] inputValues = new string[5];
        private bool IsCoolingDown;
        public GameObject[] outputButtons = new GameObject[5];
        public string[] outputValuesPianoKeys = new string[12];
        public GameObject[] pianoKeys = new GameObject[12];
        public GameObject pianoText;
        private bool solved;
        private float timeStamp;


        //Plays the sound of the note
        public void Play()
        {
            audioSource.Play();
        }

        //Initializes the piano puzzle interaction window
        private void Start()
        {
            if (GameObject.Find("PianoKeysPanel") != null)
            {
                for (var i = 0; i < pianoKeys.Length; i++)
                {
                    pianoKeys[i] = GameObject.Find("PianoKeysPanel").transform.GetChild(i).gameObject;
                    Debug.Log(pianoKeys[i].transform.GetChild(0).GetComponent<TMP_Text>().text);
                    outputValuesPianoKeys[i] = pianoKeys[i].transform.GetChild(0).GetComponent<TMP_Text>().text;
                    pianoKeys[i].transform.GetChild(0).GetComponent<TMP_Text>().enabled = false;
                    Debug.Log(audioClips[i].name);
                }

                if (GameObject.Find("PianoOutputPanel") != null)
                    for (var i = 0; i < outputButtons.Length; i++)
                        outputButtons[i] = GameObject.Find("PianoOutputPanel").transform.GetChild(i).gameObject;
                pianoText = GameObject.Find("PianoText");
            }
        }

        //Displays output in the output slots
        public void DisplayOutput(int keyNumber)
        {
            audioSource.clip = audioClips[keyNumber - 1];
            Play();
            for (var i = 0; i < outputButtons.Length; i++)
                if (outputButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().text.Equals(string.Empty))
                    if (inputValues[i].Equals(string.Empty))
                    {
                        Debug.Log(audioSource.clip.name.Substring(5));
                        outputButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().text =
                            audioSource.clip.name.Substring(5);

                        inputValues[i] = audioSource.clip.name.Substring(5);


                        if (i == inputValues.Length - 1)
                        {
                            solved = CheckOutput(inputValues);
                            GameplayChecker.PianoPuzzleSolved = solved;
                            Solved(solved);
                        }
                        return;
                    }
        }

        //Checks if the piano puzzle was solved. If it was solved it will add the item else it will reset it.
        public void Solved(bool solved)
        {
            foreach (var outputBtn in outputButtons)
                outputBtn.GetComponent<Image>().color = solved ? Color.green : Color.red;

            pianoText.GetComponent<TMP_Text>().text = solved
                ? "You have played the melody in correct order. The window will close in 3 seconds."
                : "Unfortunately, the melody that you have played is incorrect. The puzzle will restart in 3 seconds.";

            timeStamp = Time.time + coolDownPeriodInSeconds;
            IsCoolingDown = true;
            if (solved)
            {
                var inv = GameObject.Find("Inventory").GetComponent<Inventory.Inventory>();
                if (!inv.items.Exists(f => f.ID == 5))
                    inv.AddItem(5);
            }
        }


        //If the cooldown is finished it restarts the piano puzzle else it cleans it.
        private void Update()
        {
            if (IsCoolingDown)
                if (timeStamp <= Time.time)
                    if (GameplayChecker.PianoPuzzleSolved)
                    {
                        Clean();
                        Destroy(GameObject.Find("Piano_Interaction_Panel(Clone)"));

                        GameObject.Find("Interaction").GetComponent<InteractableManager>().DeactivateInteraction();
                        IsCoolingDown = false;
                    }
                    else
                    {
                        Clean();
                        IsCoolingDown = false;
                    }
        }


        //Cleans the puzzle
        public void Clean()
        {
            pianoText.GetComponent<TMP_Text>().text =
                "Play the sound with the keyboard in correct sequence. Some interactive objects are disabled and need to be activated.";
            for (var i = 0; i < inputValues.Length; i++)
            {
                inputValues[i] = string.Empty;
                outputButtons[i].GetComponent<Image>().color = Color.white;
                outputButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
            }
        }


        //Checks the input is the same as final result
        public bool CheckOutput(string[] inputValues)
        {
            var input = "";

            for (var i = 0; i < inputValues.Length; i++)
                input += inputValues[i];

            if (input.Equals(output))
                return true;

            return false;
        }
    }
}