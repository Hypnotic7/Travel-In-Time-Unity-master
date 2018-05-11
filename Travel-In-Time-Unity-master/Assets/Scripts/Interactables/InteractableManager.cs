using System;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.DataAccess.Repository.JsonRepository;
using Assets.Scripts.Interactables.Craft;
using Assets.Scripts.Interactables.Gramophone;
using Assets.Scripts.Interactables.Piano;
using Assets.Scripts.Interactables.Pictures;
using Assets.Scripts.Interactables.Puzzle;
using Interactables.Safe;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager interactableManager;
    public List<GameObject> interactionWindows;
    public GameObject currentWindow;
    public GameObject interactionPanel;
    public GameObject disableButton;

    public string Title { get; set; }
    public string Description { get; set; }
    public string FinalResult { get; set; }

    // Use this for initialization
    private void Start()
    {
        var manager = this.transform.GetComponent<InteractableManager>();
        interactableManager = manager;
        interactionPanel.SetActive(false);
    }

    //Activates the interaction window
    public void Activate(string interactableString)
    {
        if (interactionPanel != null) interactionPanel.SetActive(true);

        currentWindow = InstantiateIfNotFound(interactionWindows, interactableString);
        currentWindow.transform.SetParent(interactionPanel.transform, false);

    }

    //Initializes if the interaction window is not found
    public GameObject InstantiateIfNotFound(IEnumerable<GameObject> objects, string interactableString)
    {
        var tempGameObject = new GameObject();

        foreach (var currentObject in objects)
        {
            if (CheckForCorrectInteractableString(interactableString, currentObject.name))
            {
                var tempGameObj = GameObject.Find($"{currentObject.name}(Clone)");
                return tempGameObj ?? Instantiate(currentObject);
            }
        }
        return tempGameObject;
    }

    //Checks the interactable names do they match
    private bool CheckForCorrectInteractableString(string interactableString, string gameObjectsName)
    {
        return interactableString.Contains(gameObjectsName.Split('_')[0]);
    }

    //Deactivates the interaction window
    public void DeactivateInteraction()
    {
        GameplayChecker.PlayerHasTeleported = false;
        var currentInteraction = GetCurrentInteractionComponent(currentWindow.name.Split('_')[0]);
        currentInteraction.Clean();
        Destroy(currentWindow);
        interactionPanel.SetActive(false);
    }

    //Gets current interaction window.
    public IPuzzle GetCurrentInteractionComponent(string interactionString)
    {
        switch (interactionString)
        {
            case "Safe":
                return currentWindow.GetComponent<Safe>();

            case "Pictures":
                return currentWindow.GetComponent<PicturesManager>();

            case "Piano":
                return currentWindow.GetComponent<PianoInteraction>();

            case "Craft":
                return currentWindow.GetComponent<CraftVisiblityFlask>();

            case "Gramophone":
                return currentWindow.GetComponent<GramophoneInteraction>();

            case "Vinyl":
                return currentWindow.GetComponent<UnlockPuzzle>();

            case "Puzzle":
                return currentWindow.GetComponent<PuzzleInteraction>();

        }
        return null;
    }

}