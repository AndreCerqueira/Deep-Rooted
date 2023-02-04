using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float force = 100.0f;
    public float horizontalSpeed = 5.0f;

    private Rigidbody2D rigidBody2D;
    private Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Obstacle")))
        {
            rigidBody2D.AddForce(new Vector2(0, -force));
        }
        

        float horizontal = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(horizontal * horizontalSpeed, rigidBody2D.velocity.y);
    }


}
