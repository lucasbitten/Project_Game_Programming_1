using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [Header("Enemy Movement Info")]
    public List<Transform> waypoints;
    public int nextWaypoint = 1;
    public Transform eyes;
    public LayerMask playerLayer;
    public Transform attackPoint;

    public Animator animator;
    public Rigidbody2D rBody;
    private bool isRight = true;
    private Vector2 forwardVector;
    [HideInInspector] public Transform chaseTarget;

    public Sprite exclamation;
    public Sprite questionMark;
    public GameObject sign;


    // Start is called before the first frame update
    void Start()
    {
        sign.SetActive(false);
        rBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // Check orientation of the enemy
        if(rBody.velocity.x > 0 && !isRight)
        {
            Flip();
        }
        else if (rBody.velocity.x < 0 && isRight)
        {
            Flip();
        }
        // Set parameters in the animator
        animator.SetFloat("xSpeed", Mathf.Abs(rBody.velocity.x));
    }

    public void Move(Vector3 target, float speed)
    {
        forwardVector = target - transform.position;
        forwardVector.Normalize();

        // Move towards my target
        // TODO: Add State speed
        rBody.velocity = new Vector3(forwardVector.x, 0,0 )* speed;
    }

    public void Flip()
    {

        isRight = !isRight;
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
        sign.transform.localScale = new Vector3(-sign.transform.localScale.x, sign.transform.localScale.y, sign.transform.localScale.z);

    }

}
