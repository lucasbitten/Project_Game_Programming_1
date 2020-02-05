using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum State
    {
        STATE_PATROLLING,
        STATE_CHASING,
        STATE_SEARCHING
    };
public class Enemy : MonoBehaviour
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

    [SerializeField] LayerMask enemies;


    [SerializeField] float callEnemiesRadius;
    [SerializeField] float sightViewRadius;
    PlayerController player;    
    private bool isFacingRight = false;

    void Start()
    {
        currentState = State.STATE_PATROLLING;
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {

        switch (currentState)
        {

            case State.STATE_PATROLLING:

                Debug.Log("Patrolling");

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
    void ChasePlayer()
    {

        float step = 3 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step); 

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
        Debug.Log("Found " + results.Length + " enemies");

        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].GetComponent<Enemy>().currentState != State.STATE_CHASING)
            {
                StartCoroutine(StartToChase(results[i].GetComponent<Enemy>()));

            }
        }
    }

    IEnumerator StartToChase(Enemy enemy)
    {
        yield return new WaitForSeconds(.5f);
        enemy.spotPlayer = true;
    }

    private void Flip()
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
