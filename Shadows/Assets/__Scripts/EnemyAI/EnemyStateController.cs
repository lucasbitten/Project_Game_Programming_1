using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public EnemyStats stats;

    [Header("State Information")]
    //Current State
    public State currentState;
    //Dummy state (to represent the same state)
    public State sameState;

    // [Header("Movement Information")]
    [HideInInspector] public EnemyMovementController enemyMovementController;
    [HideInInspector] public float nextAttack = 0f;
    [HideInInspector] public float timeLookingForPlayer = 0;
    [HideInInspector] public bool attacking;
    [SerializeField] Animator anim;

    [HideInInspector] public PlayerController player;
    [HideInInspector] public bool detectedPlayer;

    [Header("Archer")]
    public ArcherController archerController;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        enemyMovementController = GetComponent<EnemyMovementController>();
        currentState.InitState(this);
        stats.currentHealth = stats.maxHealth;
        timeLookingForPlayer = stats.timeToSearch;
    }

    void Update()
    {
        if (player.visible == false)
        {
            detectedPlayer = false;
        }
        currentState.UpdateState(this);
        if (attacking)
        {
            nextAttack -= Time.deltaTime;
        }
    }

    //Transitions the state machine to a new state
    public void TransitionToState(State nextState)
    {
        if(nextState != sameState)
        {
            // Transition to a new state!
            currentState = nextState;

            // Initialize new state
            currentState.InitState(this);
        }
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hurt");
        stats.currentHealth -= damage;

        if(stats.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<EnemyMovementController>().sign.SetActive(false);
        anim.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        Destroy(gameObject,2);
        this.enabled = false;
    }

}
