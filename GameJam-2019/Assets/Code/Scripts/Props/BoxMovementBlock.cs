using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovementBlock : MonoBehaviour
{

    private Rigidbody2D rigidBody;

    public Sound impactSound;

    public Sound moveSound;

    private bool falling;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(rigidBody.velocity.x) > 0.01f)
        {
            this.impactSound.Play(transform, rigidBody);
        }
        if (falling)
        {
            if (rigidBody.velocity.y >= -0.01)
            {
                this.falling = false;
                this.impactSound.Play(transform,rigidBody);
            }
        }

        if (rigidBody.velocity.y < -0.05)
        {
            falling = true;
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BigController>())
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BigController>())
            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }
}
