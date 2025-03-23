using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellControl : MonoBehaviour
{
    [SerializeField] float spellVelocity = 10;
    [SerializeField] float spellDirection;

    void Start()
    {
        // Determinates the direction of the spell from the player scale
        spellDirection = GameObject.FindWithTag("Player").transform.localScale.x;

        if (spellDirection > 0)
        {
            spellDirection = 1;
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        else if(spellDirection < 0)
        {
            spellDirection = -1;
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        // Applies the direction in the movement
        GetComponent<Rigidbody2D>().velocity = new Vector2(spellDirection * spellVelocity, 0);
    }
}
