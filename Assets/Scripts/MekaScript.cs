using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MekaScript : PlayerController
{
    public Transform grabDetect;
    public Transform boxHolder;

    private bool pegou;
    public float rayDist;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bc = GetComponent<BoxCollider2D>();

        tr = GetComponent<TrailRenderer>();

        rb.gravityScale = 4f;

        speed = new Vector3(8f, 0, 0);

        jumpForce = 18f;

        isGrounded = false;

        pegou = false;
    }

    void Update()
    {
        Jump();
        Run();
        Dash();
        GrabObject();

    }

    void GrabObject()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right, rayDist);

        if (grabCheck.collider != null && grabCheck.collider.tag == "Box")
        {
            if (Input.GetKey(KeyCode.G))
            {
                pegou = !pegou;
                if (pegou)
                {
                    grabCheck.collider.gameObject.transform.parent = boxHolder;
                    grabCheck.collider.gameObject.transform.position = boxHolder.position;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                }
                else
                {
                    grabCheck.collider.gameObject.transform.parent = null;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }
        }
    }

    protected override void DirecaoMudou(int direction)
    {
        if (direction == -1)
        {
            playerFollow.transform.position = transform.position + new Vector3(Mathf.Abs(transform.position.x - playerFollow.transform.position.x), 0, 0);
            grabDetect.position = transform.position + new Vector3(Mathf.Abs(transform.position.x - grabDetect.position.x) * -1, -0.4f, 0);
            boxHolder.position = transform.position + new Vector3(Mathf.Abs(transform.position.x - boxHolder.position.x) * -1, 0, 0);
        }
        else if (direction == 1)
        {
            playerFollow.transform.position = transform.position + new Vector3(Mathf.Abs(transform.position.x - playerFollow.transform.position.x) * -1, 0, 0);
            grabDetect.position = transform.position + new Vector3(Mathf.Abs(transform.position.x - grabDetect.position.x), -0.4f, 0);
            boxHolder.position = transform.position + new Vector3(Mathf.Abs(transform.position.x - boxHolder.position.x), 0, 0);
        }
    }
}
