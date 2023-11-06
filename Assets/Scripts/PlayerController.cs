using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    public GameObject playerFollow;

    protected Rigidbody2D rb;

    protected BoxCollider2D bc;

    [SerializeField] protected LayerMask Jumpable;
    [SerializeField] protected TrailRenderer tr;

    protected Vector3 speed;

    protected float jumpForce = 8f;
    protected float moviment;
    protected float gravity = 4;

    protected float dashCooldown = .5f;
    protected float dashVelocity = 18f;
    private float dashDuration = .1f;
    private bool podeDash = true;

    protected bool isGrounded;
    
    public int direction;

    protected void Run()
    {
        moviment = Input.GetAxis("Horizontal");

        if (moviment != 0)
            transform.position += moviment * Time.deltaTime * speed;
        
        if (moviment < 0)
        {
            direction = -1;
            playerFollow.GetComponent<PlayerController>().direction = direction;
            DirecaoMudou(direction);
        }
        else if (moviment > 0)
        {
            direction = 1;
            playerFollow.GetComponent<PlayerController>().direction = direction;
            DirecaoMudou(direction);
        }
    }

    protected virtual void DirecaoMudou(int direction)
    {
        if (direction == -1)
        {
            playerFollow.transform.position = transform.position + new Vector3(Mathf.Abs(transform.position.x - playerFollow.transform.position.x), 0, 0);
            //fazer as rotações dos personagens em 180
        }
        else if (direction == 1)
        {
            playerFollow.transform.position = transform.position + new Vector3(Mathf.Abs(transform.position.x - playerFollow.transform.position.x) * -1, 0, 0);
            //fazer as rotações dos personagens em 0
        }
    }

    protected void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsJumpable())
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    protected bool IsJumpable()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, Jumpable);
    }

    protected void ManagerCollision(GameObject col)
    {

        if (col.CompareTag("Pilha"))
        {
            print("Pegou pilha");
        }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        ManagerCollision(other.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        ManagerCollision(other.gameObject);
    }

    public int GetDirection()
    {
        return direction;
    }

    public void Dash()
    {
        if (Input.GetButtonDown("Fire3") && podeDash)
        {
            rb.gravityScale = 0f;
            podeDash = false;
            if (direction == 1)
            {
                rb.velocity = Vector2.right * dashVelocity;
                tr.emitting = true;
            }
            if (direction == -1)
            {
                rb.velocity = Vector2.left * dashVelocity;
                tr.emitting = true;
            }
            StartCoroutine(dashTempo());
        }
    }

    public IEnumerator dashTempo()
    {
        yield return new WaitForSeconds(dashDuration);
        tr.emitting = false;
        rb.gravityScale = gravity;
        yield return new WaitForSeconds(dashCooldown);
        podeDash = true;
    }
}
