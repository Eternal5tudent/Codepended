using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numberpad : Interactable
{
    [SerializeField] Door door;
    [SerializeField] int code;
    [SerializeField] AudioClip codeRejectedSound;
    [SerializeField] AudioClip doorOpeningSound;
    private bool canInteract = true;

    protected override void Interact()
    {
        base.Interact();
        if (!canInteract)
            return;
        if (player.Type == PlayerController.PlayerType.Left)
        {
            UI_Manager.Instance.LeftNumberPad.Open(this);
        }
        else
        {
            UI_Manager.Instance.RightNumberPad.Open(this);
        }
    }

    protected override void StopInteraction()
    {
        base.StopInteraction();
        if (player.Type == PlayerController.PlayerType.Left)
        {
            UI_Manager.Instance.LeftNumberPad.Close();
        }
        else
        {
            UI_Manager.Instance.RightNumberPad.Close();
        }
    }

    public void EvaluateInput(int code)
    {
        print("desired: " + this.code + " | actual: " + code);
        if (code == this.code)
        {
            OpenDoor();
        }
        else
        {
            RejectCode();
        }
    }

    public void OpenDoor()
    {
        door.Open();
        canInteract = false;
        AudioManager.Instance.PlaySFX(doorOpeningSound);
    }

    public void RejectCode()
    {
        AudioManager.Instance.PlaySFX(codeRejectedSound);
    }
}
