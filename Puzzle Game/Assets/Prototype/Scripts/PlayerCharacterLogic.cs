using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What this script does:
/// Handles the player logic: Movement, Crouching, Jumping, Running
/// </summary>
public class PlayerCharacterLogic : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpForce;

    private Rigidbody rb = null;
    private Vector3 userInput = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    //Checks & Flag Variables
    [Header("Ground Check Variables")]
    [SerializeField]
    private float groundDist;
    [SerializeField]
    private bool isGrounded = true;
    [SerializeField]
    private Transform groundCheck = null;
    [SerializeField]
    private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player isGrounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundLayer, QueryTriggerInteraction.Ignore);

        //Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// Handle getting the player input & jumping
    /// </summary>
    private void Movement()
    {
        userInput = Vector3.zero;
        userInput.x = Input.GetAxisRaw("Horizontal");
        userInput.z = Input.GetAxisRaw("Vertical");

        moveDir = (userInput.z * transform.forward) + (userInput.x * transform.right);
        moveDir = moveDir.normalized * movementSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + moveDir);
    }

    private void FixedUpdate()
    {
        Movement();
    }
}
