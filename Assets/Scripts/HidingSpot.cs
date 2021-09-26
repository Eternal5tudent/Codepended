using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : Interactable
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite interactedSprite_LeftPlayer;
    [SerializeField] Sprite interactedSprite_RightPlayer;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    protected override void Interact()
    {
        base.Interact();
        if(player.Type == PlayerController.PlayerType.Left)
            spriteRenderer.sprite = interactedSprite_LeftPlayer;
        else
            spriteRenderer.sprite = interactedSprite_RightPlayer;
        player.Hide(transform.position);
    }

    protected override void StopInteraction()
    {
        base.StopInteraction();
        spriteRenderer.sprite = defaultSprite;
        player.UnHide();
    }
}
