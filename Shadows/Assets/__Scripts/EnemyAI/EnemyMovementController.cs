using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [Header("Enemy Movement Info")]
    public List<Transform> waypoints;
    public int nextWaypoint = 1;

    private Animator animator;
    private Rigidbody2D rBody;
    private bool isRight = true;
    private Vector2 forwardVector;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check orientation of the enemy
        if (rBody.velocity.x > 0 && !isRight)
        {
            Flip();
        }
        else if (rBody.velocity.x < 0 && isRight)
        {
            Flip();
        }

        // Set parameters in the animator

    }

    public void Move(Vector3 target, float speed)
    {
        forwardVector = target - transform.position;
        forwardVector.Normalize();

        // Move towards my target
        // TODO: Add State speed
        rBody.velocity = forwardVector * speed;
    }

    private void Flip()
    {
        isRight = !isRight;
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
}
