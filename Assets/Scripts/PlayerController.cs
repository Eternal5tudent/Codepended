using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerType { Left, Right }

    [SerializeField] private float movementSpeed;
    [SerializeField] private PlayerType playerType;
    public PlayerType Type { get { return playerType; } }

    private InputManager inputManager;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private int lookDirection = 1;
    private Vector3 positionBeforeHiding;
    public bool IsHiding { get; private set; }
    public bool IsDead { get; private set; }

    private Interactable collidingInteractable;
    private Interactable currentInteractable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        if (IsDead)
            return;
        if (playerType == PlayerType.Left)
        {
            if (inputManager.Interact_L)
            {
                InteractWithObject();
            }
        }
        else
        {
            if (inputManager.Interact_R)
            {
                InteractWithObject();
            }
        }
    }

    private void InteractWithObject()
    {
        if (IsDead)
            return;
        Interactable collidingInteractable = this.collidingInteractable;
        if (currentInteractable == null && collidingInteractable != null) //if not currently interacting with anything
        {
            bool interactionSuccessful = collidingInteractable.TryInteract(this);
            if (interactionSuccessful)
            {
                currentInteractable = collidingInteractable;
            }
        }
        else if(currentInteractable != null)
        {
            bool stopInteractionSuccessful = currentInteractable.TryStopInteraction(this);
            if (stopInteractionSuccessful)
            {
                currentInteractable = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (IsDead)
            return;
        if (playerType == PlayerType.Left)
        {
            Move(inputManager.AxisInput_L);
        }
        else
        {
            Move(inputManager.AxisInput_R);
        }
    }

    private void Move(Vector2 direction)
    {
        if (IsDead)
            return;
        // a state machine would be really useful here :( //
        if (IsHiding)
            return;

        direction.Normalize();
        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
            animator.SetBool("side", true);
        else if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(rb.velocity.y))
            animator.SetBool("side", false);
        if (lookDirection > 0)
        {
            if (direction.x < 0)
            {
                Flip();
            }
        }
        else
        {
            if (direction.x > 0)
            {
                Flip();
            }
        }
        rb.velocity = direction * movementSpeed;
    }

    private void Flip()
    {
        lookDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDead)
            return;
        if (collision.CompareTag("Interactable"))
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            collidingInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsDead)
            return;
        if (collision.CompareTag("Interactable"))
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable == collidingInteractable)
            {
                collidingInteractable = null;
            }
            if (currentInteractable == interactable)
            {
                bool stopInteractionSuccessful = currentInteractable.TryStopInteraction(this);
                if (stopInteractionSuccessful)
                {
                    currentInteractable = null;
                }
            }
        }
    }

    public void Hide(Vector3 hidePos)
    {
        if (!IsHiding)
        {
            spriteRenderer.enabled = false;
            rb.velocity = Vector2.zero;
            positionBeforeHiding = transform.position;
            IsHiding = true;
            transform.position = hidePos;
            Debug.Log("shhhh, I'm hiding");
        }
    }

    public void UnHide()
    {
        if (IsHiding)
        {
            spriteRenderer.enabled = true;
            IsHiding = false;
            transform.position = positionBeforeHiding;
            Debug.Log("I ain't hiding anymore");
        }
    }

    public void Die()
    {
        if (IsDead)
            return;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("die");
        if(currentInteractable != null)
        {
            currentInteractable.TryStopInteraction(this);
        }
        IsDead = true;
    }
}
