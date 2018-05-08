using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{

    public NavMeshAgent playerAgent;

    public virtual void MoveToInteraction(NavMeshAgent playerAgent)
    {
        Interact();
    }

    //Base interact method
    public virtual void Interact()
    {
    }


}
