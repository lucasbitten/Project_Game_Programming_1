using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlayer : MonoBehaviour
{

    [SerializeField] GameObject spotSign;
    [SerializeField] Sprite questionMark, exclamation;

    Enemy enemy;
    PlayerController player;
    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject ){
            if (player.visible){
                spotSign.GetComponent<SpriteRenderer>().sprite = exclamation;
                spotSign.SetActive(true);
                enemy.spotPlayer = true;
                enemy.currentState = State.STATE_CHASING;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject ){
            if (player.visible){
                spotSign.SetActive(true);
                spotSign.GetComponent<SpriteRenderer>().sprite = exclamation;
                enemy.spotPlayer = true;
                enemy.currentState = State.STATE_CHASING;

            } 
            // else{
            //     spotSign.GetComponent<SpriteRenderer>().sprite = questionMark;
            //     enemy.spotPlayer = false;
            //     enemy.chasingPlayer = true;
            // }
        }
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject == player.gameObject ){
    //         spotSign.SetActive(false);
    //         enemy.spotPlayer = false;
    //         enemy.currentState = State.STATE_RETURNING;  //Enemies will return to their initial position
    //     }
    // }

}
