using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementHandler : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravityForce = 2f;
    public bool canMove;
    protected CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        if (canMove) 
        { 
            Movement(); 
        }
        
    }

    public void ChangeMove()
    {
        canMove = !canMove;
    }
    private void Movement()
    {
        Vector2 movementInput = InputHandler.instance.movementInput;
        Vector3 movementDirection = transform.forward * movementInput.y + transform.right * movementInput.x;
        movementDirection.y = -gravityForce;

        _characterController.Move(movementDirection * Time.fixedDeltaTime * movementSpeed);
    }
}
