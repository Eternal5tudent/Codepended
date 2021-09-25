using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PlayerType { Left, Right }

    [SerializeField] private float movementSpeed;
    [SerializeField] private PlayerType playerType;

    private InputManager inputManager;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private Interactable collidingInteractable;
    private int lookDirection = 1;
    private bool isHiding;
    private bool currentlyInteracting;
    private Vector3 positionBeforeHiding;

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
        if (playerType == PlayerType.Left)
        {
            if (inputManager.Interact_L)
            {
                if (collidingInteractable != null )
                {
                    if (!currentlyInteracting)
                        currentlyInteracting = collidingInteractable.TryInteract(this);
                    else
                        currentlyInteracting = collidingInteractable.TryStopInteraction(this);
                }
            }
        }
        else
        {
            if (inputManager.Interact_R)
            {
                if (collidingInteractable != null)
                {
                    if (!currentlyInteracting)
                        currentlyInteracting = collidingInteractable.TryInteract(this);
                    else
                        currentlyInteracting = collidingInteractable.TryStopInteraction(this);
                }
            }
        }
    }

    private void FixedUpdate()
    {
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
        // a state machine would be really useful here :(
        if (isHiding)
            return;

        direction.Normalize();
        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
            animator.SetBool("side", true);
        else if(Mathf.Abs(rb.velocity.x) < Mathf.Abs(rb.velocity.y))
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
        if (collision.CompareTag("Interactable"))
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable != null)
            {
                collidingInteractable = interactable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable == collidingInteractable)
            {
                collidingInteractable.TryStopInteraction(this);
                collidingInteractable = null;
            }
        }
    }

    public void Hide(Vector3 hidePos)
    {
        if (!isHiding)
        {
            spriteRenderer.enabled = false;
            rb.velocity = Vector2.zero;
            positionBeforeHiding = transform.position;
            isHiding = true;
            transform.position = hidePos;
            Debug.Log("shhhh, I'm hiding");
        }
    }

    public void UnHide()
    {
        if (isHiding)
        {
            spriteRenderer.enabled = true;
            isHiding = false;
            transform.position = positionBeforeHiding;
            Debug.Log("I ain't hiding anymore");
        }
    }

}
