using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private float runSpeed = 8.00f;
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private int currentHealth = 5;
    
    private Rigidbody2D player;
    [SerializeField] Animator anim;
    public Transform[] shadowPoints;

    public bool isGrounded;
    private bool isFacingRight = false;

    public List<GameObject> lights = new List<GameObject>();
    public bool visible;

    private float horizontalMovement;
    private bool jumping;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lights.Count != 0){
            visible = true;
        }else{
            visible = false;
        }
        //Debug.Log($"speed => {speed}");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }


        horizontalMovement = Input.GetAxis("Horizontal");
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }



    }

    void FixedUpdate()
    {
        // var moveVertical = Input.GetAxis("Vertical") * speed;
        // var moveHorizontal = Input.GetAxis("Horizontal") * speed;

        // player.velocity = new Vector2(moveHorizontal, moveVertical);

        Vector2 movement = Vector2.zero;


        if ( jumping)
        {
            player.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        isGrounded = (CheckGround() && player.velocity.y < 0.1f);



        player.velocity = new Vector2(horizontalMovement * speed, player.velocity.y);

        if (isFacingRight && player.velocity.x > 0)
        {
            Flip();
        }
        else if (!isFacingRight && player.velocity.x < 0)
        {
            Flip();
        }
        // Animations sections
        anim.SetFloat("xSpeed", Mathf.Abs(player.velocity.x));

        anim.SetFloat("ySpeed", player.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Touched the enemy");
        }
    }

    public void TakeDamage(int damage)
    {

        anim.SetTrigger("isHurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        anim.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        Destroy(gameObject, 2);
        this.enabled = false;
    }
}
