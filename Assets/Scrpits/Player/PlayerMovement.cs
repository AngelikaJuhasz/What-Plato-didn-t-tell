using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector3 originalScale; // Store the original scale

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale; // Save the initial scale
    }

    void Update()
    {
        // Get movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize diagonal movement (prevents faster movement when moving diagonally)
        if (movement.sqrMagnitude > 1)
            movement.Normalize();

        // Set animator parameters
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetBool("IsMoving", movement.sqrMagnitude > 0); // Detects if moving

        // Flip character for left movement while keeping original scale
        if (movement.x != 0)
            transform.localScale = new Vector3(originalScale.x * -Mathf.Sign(movement.x), originalScale.y, originalScale.z);
    }

    void FixedUpdate()
    {
        // Move the player
        rb.linearVelocity = movement * speed;
    }
}
