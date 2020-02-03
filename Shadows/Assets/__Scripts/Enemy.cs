using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

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

    public bool chasingPlayer;

    [SerializeField] float callEnemiesRadius;
    [SerializeField] float sightViewRadius;
    PlayerController player;    
    private bool isFacingRight = false;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(chasingPlayer)
        {
            if (PlayerOnRange())
            {
                ChasePlayer();
            } else 
            {
                spotPlayer = false;
                chasingPlayer = false;
            }
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

        float step = 10 * Time.deltaTime;
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
            if (!results[i].GetComponent<Enemy>().chasingPlayer)
            {
                StartCoroutine(StartToChase(results[i].GetComponent<Enemy>()));

            }
        }
    }

    IEnumerator StartToChase(Enemy enemy)
    {
        yield return new WaitForSeconds(.5f);
        enemy.spotPlayer = true;
        enemy.chasingPlayer = true;
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
