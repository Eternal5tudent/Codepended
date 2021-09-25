using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : Interactable
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite interactedSprite;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    protected override void Interact()
    {
        base.Interact();
        spriteRenderer.sprite = interactedSprite;
        player.Hide(transform.position);
    }

    protected override void StopInteraction()
    {
        base.StopInteraction();
        spriteRenderer.sprite = defaultSprite;
        player.UnHide();
    }
}
