using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    [SerializeField] float velocity = 5;

    [SerializeField] float groundDistance = 1;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float buttonPressingTime = 0.3f;
    [SerializeField] float jumpForce = 10;
    bool isGrounded;
    float jumpTime;
    bool jump;

    float cooldown = 0.5f;
    float spellRecharge;

    public GameObject fireballPrefab;


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
        Fireball();
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
        else if (horizontalMovement == -1)
        {

            transform.localScale = new Vector3(-2f, 2f, 1f);
        }

        // The Mathf.Abs make the Animator "walk" animation work, togheter with the LocalScale manipulation, the animation is applied to the left and to the right
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
    }

    void PlayerJump()
    {
        // Checks if the groundLayer is below the player, and if the distance between both is the same as groundDistance
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);

        // If the space button is pressed, the jump bool becomes true, increasing the vertical velocity of the rigidbody, making it go up
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
            jumpTime = 0;
        }

        if (jump)
        {
            // Maintains de horizontal velocity and applies the vertical force
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTime += Time.deltaTime;
            animator.SetBool("Jump", true);
        }

        // If the space is no longer pressed or if the time of the jump runs out, the player no longer go up
        if (Input.GetKeyUp(KeyCode.Space) || jumpTime > buttonPressingTime)
        {
            jump = false;
            animator.SetBool("Jump", false);
        }
    }

    void Fireball()
    {
        spellRecharge += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && spellRecharge > cooldown)
        {
            Instantiate(fireballPrefab, transform.position, transform.rotation);
            animator.SetBool("Attack", true);
            spellRecharge = 0;
        }
        else
        {
            animator.SetBool("Attack", false);
        }   
    }
}
