using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour
{
    private NavMeshAgent playerAgent;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    //Checks was the mouse clicked to get interaction
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            GetInteraction();
    }

    //if the tag equals to the interactable objects performs interaction related to the object
    void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.CompareTag("Interactable Object"))
            {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
        }
    }
}
