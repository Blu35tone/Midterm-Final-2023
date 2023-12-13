using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Movement : MonoBehaviour
{
    private float horizontal;
    private float xSpeed = 16f;
    private float Jump = 15;
    private bool FacedRight= true;

    int playerOneScore;
    public Text scoreTextP1;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, Jump);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y >0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * xSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (FacedRight && horizontal < 0f || FacedRight && horizontal > 0f)
        {
            FacedRight = !FacedRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) //when a collision happens
    {
        Debug.Log("hit");
        if (collision.collider.CompareTag("Player"))
        {
            scoreTextP1.text = playerOneScore.ToString();
            playerOneScore++;
        }
    }

}
