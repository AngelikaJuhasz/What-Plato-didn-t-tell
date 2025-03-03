using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize diagonal movement
        if (movement.magnitude > 1)
            movement.Normalize();

        // Set animator parameters
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        // Flip character for left movement
        if (movement.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(movement.x), 1, 1);
    }

    void FixedUpdate()
    {
        // Move the player
        rb.linearVelocity = movement * speed;
    }
}
