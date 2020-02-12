using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    NO_STATE,
    STATE_PATROLLING,
    STATE_CHASING,
    STATE_SEARCHING,
    STATE_RETURNING
};

public abstract class Enemy : MonoBehaviour
{

    public State currentState;
    private int health;
    private bool SpotPlayer;
    public bool spotPlayer
    {
        get { return SpotPlayer ; }
        set
        {
            if( value == SpotPlayer )
                return ;

            SpotPlayer = value ;
            if( SpotPlayer )
            {
                CallEnemies();
            }
        }    
    }

    [SerializeField] bool canMoveVertically;

    [SerializeField] LayerMask enemies;

    [SerializeField] float callEnemiesRadius;
    [SerializeField] float sightViewRadius;
    PlayerController player;    
    protected bool isFacingRight = false;
    private Vector3 enemyInitPos;

    protected virtual void Start()
    {
        currentState = State.STATE_PATROLLING;
        player = FindObjectOfType<PlayerController>();
        enemyInitPos = new Vector3(transform.position.x, transform.position.y);
    }

    protected virtual void CallStateMachine()
    {
        switch (currentState)
        {

            case State.STATE_PATROLLING:

                PatrolMovement();

                if (spotPlayer)
                {
                    currentState = State.STATE_CHASING;
                }

            break;
            case State.STATE_CHASING:
                Debug.Log("Chasing");
                ChasePlayer();

            break;
            case State.STATE_SEARCHING:
                Debug.Log("Searching");
                searchPlayer();
            break;

            case State.STATE_RETURNING:
                Debug.Log("Stopped Chasing");
                StopChasing();
            break;

        }
    }



    bool PlayerOnRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > sightViewRadius)
        {
            return false;
        } else
        {
            return true;
        }
    }

    protected virtual void PatrolMovement()
    {

    }

    protected virtual void ChasePlayer()
    {

        float step = 3 * Time.deltaTime;

        if(canMoveVertically)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step); 
        } else
        {
            Vector3 newPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.y);

            transform.position = Vector3.MoveTowards (transform.position, new Vector3(newPos.x,transform.position.y,transform.position.z), step);
        }


        if (player.transform.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        if (player.transform.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        CallEnemies(); 
    }

    void CallEnemies()
    {

        Collider2D[] results;
        results = Physics2D.OverlapCircleAll(transform.position, callEnemiesRadius, enemies);

        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].GetComponent<Enemy>().currentState != State.STATE_CHASING)
            {
                StartCoroutine(StartToChase(results[i].GetComponent<Enemy>()));

            }
        }
    }

    protected virtual void StopChasing()
    {
        float retreat = 2 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, enemyInitPos, retreat);

        if (enemyInitPos.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        if (enemyInitPos.x < transform.position.x && isFacingRight)
        {
            Flip();
        }

        if (transform.position == enemyInitPos)
        {
            currentState = State.STATE_PATROLLING;
        }
    }

    void searchPlayer()
    {
        if (!isSearching)
        {
            playerLastPos = new Vector3(player.transform.position.x, player.transform.position.y);
            isSearching = true;
        }
        float searchSpd = 2.5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerLastPos, searchSpd);

        if (playerLastPos.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        if (playerLastPos.x < transform.position.x && isFacingRight)
        {
            Flip();
        }

        if (transform.position == playerLastPos)
        {
            currentState = State.STATE_RETURN;
            isSearching = false;
        }
    }

    IEnumerator StartToChase(Enemy enemy)
    {
        yield return new WaitForSeconds(.5f);
        enemy.spotPlayer = true;
    }

    protected void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
        isFacingRight = !isFacingRight;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, callEnemiesRadius);
    }
}
