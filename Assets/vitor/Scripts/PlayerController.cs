using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;


public enum Tool
{
    Pa,
    Picareta
}


public class PlayerController : MonoBehaviour
{

    public float force = 100.0f;
    public float horizontalSpeed = 5.0f;

    private Rigidbody2D rigidBody2D;
    private Collider2D playerCollider;

    private Tool tool = Tool.Pa;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 6 && tool == Tool.Picareta)
        {
            print("Ferramenta Errada" + tool);
            force = force * 0.5f;
        }
        else if (other.gameObject.layer == 7 && tool == Tool.Pa)
        {
            print("Ferramenta Errada" + tool);
            force = force * 0.5f;
        }
        else
        {
            print("Ferramenta Certa" + tool);
            force = force;
        }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tool == Tool.Picareta)
            {
                tool = Tool.Pa;
                print("Trocou Ferramenta " + tool);
            }
            else
            {
                tool = Tool.Picareta;
                print("Trocou Ferramenta " + tool);
            }

        }
    }


}
