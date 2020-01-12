using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D player;

    public float speed = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var moveVertical = Input.GetAxis("Vertical") * speed;
        var moveHorizontal = Input.GetAxis("Horizontal") * speed;

        player.velocity = new Vector2(moveHorizontal, moveVertical);
    }
}
