using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    protected bool interacted;
    protected List<GameObject> deleteOnStopInteraction = new List<GameObject>();
    protected PlayerController player;

    public bool TryInteract(PlayerController player)
    {
        if (!interacted)
        {
            this.player = player;
            Interact();
            return true;
        }
        return false;
    }

    protected virtual void Interact()
    {
        interacted = true;
    }

    protected virtual void StopInteraction()
    {
        interacted = false;
    }

    public bool TryStopInteraction(PlayerController player)
    {
        if (interacted)
        {
            this.player = player;
            StopInteraction();
            return true;
        }
        return false;
    }
}
