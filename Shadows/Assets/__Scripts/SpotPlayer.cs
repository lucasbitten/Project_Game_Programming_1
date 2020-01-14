using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlayer : MonoBehaviour
{

    [SerializeField] GameObject spotSign;
    [SerializeField] Sprite questionMark, exclamation;

    PlayerController player;
    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject ){
            if (player.visible){
                spotSign.SetActive(true);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject ){
            if (player.visible){
                spotSign.SetActive(true);
                spotSign.GetComponent<SpriteRenderer>().sprite = exclamation;
            } else{
                spotSign.GetComponent<SpriteRenderer>().sprite = questionMark;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject ){
            spotSign.SetActive(false);
        }
    }

}
