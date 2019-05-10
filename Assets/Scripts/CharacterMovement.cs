using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float maxSpeed = 6.0f;
    public bool facingRight = true;
    public float moveDirection;
    private Rigidbody rigidbody;
    private Animator animator;

    public float jumpSpeed = 300.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;


    void Awake()
    {
        groundCheck = GameObject.Find("GroundCheck").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent <Rigidbody> ();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
        }
    }

    void FixedUpdate() {
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (moveDirection > 0.0f && !facingRight)
        {
            Flip();
        } else if (moveDirection < 0.0f && facingRight)
        {
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f);
    }
}
