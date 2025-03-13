using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private float velocity = 5f;
    public float jumpForce; // [APLICAR VOO]
    private Animator animator;
    private float horizontalMovement;

    void Start()
    {
        // Aplica a física
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Impede o player de deslizar, setando a velocidade inicial para 0
        rb.velocity = Vector2.zero;
        animator = rb.GetComponent<Animator>();
    }


    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Pega o input Horizontal, que pode ser mudado em: Edit -> Project Settings -> Input Manager -> Axis -> Horizontal
        // Pega o valor absoluto dos inputs, sem que o horizontalMovement cresça até 1, mas sim é setado em um enquanto segurar a tecla
        if (Input.GetKey("d"))
        {
            transform.localScale = new Vector3(2f, 2f, 1f);
            horizontalMovement = 1f;
        }
        else if (Input.GetKey("a"))
        {
            transform.localScale = new Vector3(-2f, 2f, 1f);
            horizontalMovement = -1f;
        }
        else
        {
            horizontalMovement = 0f;
        }


        // faz com que somente se mova no eixo X, por isso o Y é 0
        Vector2 movement = new Vector2(horizontalMovement * velocity, rb.velocity.y);

        // Ajusta a velocidade do rigidbody, mantendo a velocidade do y
        rb.velocity = movement;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        

    }
}
