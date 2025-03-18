using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private float velocity = 5f;
    private float jumpForce = 10f; // [APLICAR PULO]
    private Animator animator;
    Rigidbody2D rb;

    void Start()
    {
        // Aplica a física
        rb = GetComponent<Rigidbody2D>();
        // Impede o player de deslizar, setando a velocidade inicial para 0
        rb.velocity = Vector2.zero;
        animator = rb.GetComponent<Animator>();
    }


    void Update()
    {
        PlayerMovement();
        PlayerJump();
    }

    void PlayerMovement()
    {
        // Stores in a variable the value of horizontal movement, with no smoothing (only possible values: -1, 0, 1)
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        // Aplies tthe direction and speed into the rigidbody when pressing the horizontal keys
        rb.velocity = new Vector2(horizontalMovement * velocity, rb.velocity.y);

        // Used to change character skin facing direction
        if (horizontalMovement == 1)
        {
            transform.localScale = new Vector3(2f, 2f, 1f);
        }
        else if (horizontalMovement == -1) {

            transform.localScale = new Vector3(-2f, 2f, 1f);
        }

        // The Mathf.Abs make the Animator "walk" animation work, togheter with the LocalScale manipulation, the animation is applied to the left and to the right
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Pass the jump amount straight to the rigidbody, without needing to declare a new vector2
            // ForceMode2D applies physics forces, in a incremental way, and Impulse make this forces apply an immediate impulse to the rigidbody
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
