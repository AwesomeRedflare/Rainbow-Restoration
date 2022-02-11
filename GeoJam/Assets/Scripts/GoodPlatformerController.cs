using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodPlatformerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform bottomRight;
    public Transform topLeft;
    public LayerMask whatIsGround;

    public float hangTime = .2f;
    private float hangCounter;

    public float jumpBufferLength = .1f;
    private float jumpBufferCount;

    private Animator anim;
    public bool left = false;

    public bool paused = false;
    private float ogGravity;

    public GameObject explosion;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        ogGravity = rb.gravityScale;
    }

    private void FixedUpdate()
    {
        if(paused == false)
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, whatIsGround);

        //hangtime
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        //jump buffer
        if(paused == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpBufferCount = jumpBufferLength;
            }
            else
            {
                jumpBufferCount -= Time.deltaTime;
            }
        }

        //flipping sprite
        
        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
      

        //jumping
        if (hangCounter > 0f && jumpBufferCount >= 0)
        {
            rb.velocity = Vector2.up * jumpForce;

            jumpBufferCount = 0;

            FindObjectOfType<AudioManager>().Play("Jump");
        }

        //reseting
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<GameManager>().Death();
            Death();
        }

        //pausing
        if (Input.GetKeyDown(KeyCode.P))
        {
            FindObjectOfType<GameManager>().PauseScreen();
            rb.gravityScale = 0;
            paused = true;
        }

        if(paused == true)
        {
            rb.velocity = Vector2.zero;
        }

        //switch controls
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<BetterJump>().arrowKey = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<BetterJump>().arrowKey = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("spike"))
        {
            FindObjectOfType<GameManager>().Death();
            Death();
        }
    }

    public void Win()
    {
        FindObjectOfType<AudioManager>().Play("Rainbow");
        FindObjectOfType<AudioManager>().Play("Win");
        GetComponent<GoodPlatformerController>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        if(left == true)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Unpause()
    {
        paused = false;
        rb.gravityScale = ogGravity;
    }

    void Death()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Death");
        gameObject.SetActive(false);
    }
}
