using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D player;
    public Transform[] shadowPoints;

    public float speed = 30.0f;

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
        var moveVertical = Input.GetAxis("Vertical") * speed;
        var moveHorizontal = Input.GetAxis("Horizontal") * speed;

        player.velocity = new Vector2(moveHorizontal, moveVertical);
    }
}
