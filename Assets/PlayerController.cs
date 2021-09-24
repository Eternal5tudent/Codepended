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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void FixedUpdate()
    {
        if (playerType == PlayerType.Left)
        {
            Move(inputManager.axisInput_L);
        }
        else
        {
            Move(inputManager.axisInput_R);
        }
    }

    private void Move(Vector2 direction)
    {
        direction.Normalize();
        rb.velocity = direction * movementSpeed;
    }
}
