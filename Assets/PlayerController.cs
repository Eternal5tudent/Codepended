using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PlayerType { Left, Right }

    [SerializeField] private float movementSpeed;
    [SerializeField] private PlayerType playerType;

    private InputManager inputManager;
    private Rigidbody2D rb;

    private Interactable collidingInteractable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
                if (collidingInteractable != null)
                {
                    collidingInteractable.Interact();
                }
            }
        }
        else 
        {
            if(inputManager.Interact_R)
            {
                if (collidingInteractable != null)
                {
                    collidingInteractable.Interact();
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
        direction.Normalize();
        rb.velocity = direction * movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable != null)
            {
                Debug.Log("time to interact");
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
                Debug.Log("can't interact no more");
                collidingInteractable.StopInteraction();
                collidingInteractable = null;
            }
        }
    }
}
