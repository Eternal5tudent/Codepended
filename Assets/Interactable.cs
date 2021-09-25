using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool interacted;
    [SerializeField] private string text;
    private List<GameObject> deleteOnStopInteraction = new List<GameObject>();

    public void Interact()
    {
        if (!interacted)
        {
            interacted = true;
            GameObject textBox = WorldCanvas.Instance.CreateTextElement(text, transform.position);
            deleteOnStopInteraction.Add(textBox);
        }
    }

    public void StopInteraction()
    {
        foreach (GameObject gameObj in deleteOnStopInteraction)
        {
            Destroy(gameObj);
        }
        interacted = false;
    }
}
