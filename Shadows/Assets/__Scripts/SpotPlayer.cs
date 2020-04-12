using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlayer : MonoBehaviour
{


    ArcherController archer;
    PlayerController player;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        archer = GetComponentInParent<ArcherController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            if (player.visible)
            {
                archer.GetComponent<EnemyStateController>().detectedPlayer = true;
                //archer.PlayerSpotted(true, other.transform.position);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            if (player.visible)
            {
                archer.GetComponent<EnemyStateController>().detectedPlayer = true;
            }
        }
    }

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject == player.gameObject)
    //    {
    //        archer.PlayerSpotted(false, other.transform.position);
    //    }
    //}

}
