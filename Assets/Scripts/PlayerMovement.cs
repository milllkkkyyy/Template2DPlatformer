using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    /// <summary>
    /// Tranform that tells us the bottom of the player
    /// </summary>
    [SerializeField] Transform bottomOfPlayer;

    /// <summary>
    /// Layer mask that collides only with the ground layer
    /// </summary>
    [SerializeField] LayerMask groundLayerMask;

    /// <summary>
    /// Speed of the player.
    /// </summary>
    [SerializeField] float speed = 3.5f;

    /// <summary>
    /// Rigid body compenent of the player.
    /// </summary>
    Rigidbody2D rb;

    /// <summary>
    /// jump force of the player
    /// </summary>
    float jumpForce = 10;

    /// <summary>
    /// Max velocity in the X direction
    /// </summary>
    float maxVelocityX = 10;

    /// <summary>
    /// direction the player wants to move in
    /// </summary>
    Vector2 direction = Vector2.zero;

    /// <summary>
    /// does the player want to jump
    /// </summary>
    bool jump = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    /// <summary>
    /// Handles the input of the player
    /// </summary>
    void HandleInput()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0f);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            jump = true;
    }

    /// <summary>
    /// Handles left and right movement
    /// </summary>
    void HandleMovement()
    {
        if (jump)
        {
            rb.velocity += Vector2.up * jumpForce;
            jump = false;
        }

        rb.velocity += direction * speed;

        rb.velocity = Vector2.right * Mathf.Clamp(rb.velocity.x, -maxVelocityX, maxVelocityX) + Vector2.up * rb.velocity.y;
    }

    /// <summary>
    /// Is the player currently on the ground
    /// </summary>
    /// <returns> wether or not the player is on the ground </returns>
    bool IsGrounded()
    {
        bool debug = Physics2D.OverlapCircle(bottomOfPlayer.position, 0.2f, groundLayerMask);
        Debug.Log(debug);
        return debug;
    }
}