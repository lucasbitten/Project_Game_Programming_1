using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{

    PlayerController player;
    public bool spottedPlayer;
    public GameObject archerSprites;
    public GameObject fieldViewRoot;
    public Animator animator;
    public Transform arrowSpawnPoint;
    public GameObject arrowPrefab;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void Flip()
    {
        if (archerSprites.transform.localScale.x == 1)
        {
            archerSprites.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            archerSprites.transform.localScale = new Vector3(1, 1, 1);

        }
    }
}
