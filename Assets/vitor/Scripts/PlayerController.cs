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

    //public AudioSource pickup;

    private Rigidbody2D rigidBody2D;
    private Collider2D playerCollider;
    private Animator animator;
    public bool isDead = false;

    private Tool tool = Tool.Pa;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Resource")) {
            
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GetComponent<TileDestroyer>().stopGrow = true;
            collision.gameObject.GetComponent<Animator>().SetTrigger("Destroy");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GetComponent<TileDestroyer>().stopGrow = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Resource"))
        {
            print(this.gameObject.tag);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Destroy");
        }
    }

    private void FixedUpdate()
    {
        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Obstacle")))
        {
            if (!isDead)
            rigidBody2D.AddForce(new Vector2(0, -force));
        }

        float horizontal = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(horizontal * horizontalSpeed, rigidBody2D.velocity.y);

        // flip spriteSheet player
        GetComponent<SpriteRenderer>().flipX = horizontal < 0;

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
