using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] EnemyStateController controller;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Shoot()
    {
        var arrow = Instantiate(controller.archerController.arrowPrefab, controller.archerController.arrowSpawnPoint.position, Quaternion.identity);
        Vector3 dir = controller.player.transform.position - arrow.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        arrow.GetComponent<Rigidbody2D>().velocity = (controller.player.transform.position - arrow.transform.position) * 2;

        anim.SetBool("Waiting", true);
    }

}
