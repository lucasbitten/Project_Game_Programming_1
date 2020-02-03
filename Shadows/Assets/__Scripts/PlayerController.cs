using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckRadius = 0.15f;
    
    private Rigidbody2D player;
    public Transform[] shadowPoints;

    public bool isGrounded;
    private bool isFacingRight = false;

    public List<GameObject> lights = new List<GameObject>();
    public bool visible;

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


    }

    void FixedUpdate()
    {
        // var moveVertical = Input.GetAxis("Vertical") * speed;
        // var moveHorizontal = Input.GetAxis("Horizontal") * speed;

        // player.velocity = new Vector2(moveHorizontal, moveVertical);

        Vector2 movement = Vector2.zero;
        float horizontalMovement = Input.GetAxis("Horizontal");

        isGrounded = (CheckGround() && player.velocity.y < 0.1f);

        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            player.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        player.velocity = new Vector2(horizontalMovement * speed, player.velocity.y);

        if (isFacingRight && player.velocity.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && player.velocity.x > 0)
        {
            Flip();
        }
        // Animations sections
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
}
