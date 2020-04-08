using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{

    PlayerController player;
    public bool spottedPlayer;
    [SerializeField] GameObject archerSprites;
    [SerializeField] GameObject fieldViewRoot;
    [SerializeField] Animator animator;
    [SerializeField] Transform arrowSpawnPoint;
    [SerializeField] GameObject arrowPrefab;
    float z;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }


    public void PlayerSpotted(bool spotted, Vector3 position)
    {
        spottedPlayer = spotted;
        animator.SetBool("SpotPlayer", spottedPlayer);

        if (spotted)
        {
            if (position.x < transform.position.x)
            {
                archerSprites.transform.localScale = new Vector3(-1, 1, 1);

            }
            else
            {
                archerSprites.transform.localScale = new Vector3(1, 1, 1);

            }

            StartCoroutine(ShootArrow(position));
        }
    }

    IEnumerator ShootArrow(Vector3 position)
    {
        yield return new WaitForSeconds(0.4f);
        var arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        Vector3 dir = position - arrow.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        arrow.GetComponent<Rigidbody2D>().velocity = (position - arrow.transform.position) * 2;
    }

    // Update is called once per frame
    void Update()
    {
        //z = -130 - Mathf.PingPong(Time.time*10, 100);
        //Debug.Log(z);
        //fieldViewRoot.transform.eulerAngles = new Vector3(0, 0, z);
        //if (z < -180)
        //{
        //    archerSprites.transform.localScale = new Vector3(-1, 1, 1);
        //} else
        //{
        //    archerSprites.transform.localScale = new Vector3(1, 1, 1);

        //}
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
