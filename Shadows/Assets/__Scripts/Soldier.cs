using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{

    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;



    // Update is called once per frame
    void  Update()
    {
        CallStateMachine();

    }


    override protected void PatrolMovement()
    {
        float step = 3 * Time.deltaTime;

        if(isFacingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step); 

            if ( Mathf.Approximately(transform.position.x , endPosition.position.x))
            {
                Flip();
            }


        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition.position, step); 
            if ( Mathf.Approximately(transform.position.x , startPosition.position.x))
            {
                Flip();
            }
        }



    }


}
