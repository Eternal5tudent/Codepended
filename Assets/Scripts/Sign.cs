using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable
{
    [SerializeField] private string text;

    protected override void Interact()
    {
        base.Interact();
        GameObject newText = WorldCanvas.Instance.CreateTextElement(text, transform.position);
        deleteOnStopInteraction.Add(newText);
    }

    protected override void StopInteraction()
    {
        base.StopInteraction();
        foreach (GameObject gameObj in deleteOnStopInteraction.ToArray())
        {
            deleteOnStopInteraction.Remove(gameObj);
            Destroy(gameObj);
        }
    }
}
