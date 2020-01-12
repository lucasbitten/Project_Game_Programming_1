using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.Rendering.Universal;


public class DetectShadow : MonoBehaviour
{

    Light2D[] lights;

    [SerializeField] LayerMask shadowCasters;
    [SerializeField] Transform[] detectShadowPoints;

    private bool IsOnShadow ;
    
    public bool isOnShadow
    {
        get { return IsOnShadow ; }
        set
        {
            if( value == IsOnShadow )
                return ;
    
            IsOnShadow = value ;
            if( IsOnShadow )
            {
                Debug.Log("On Shadow");
            } else{
                Debug.Log("On Sun");
            }    
        }    
    }


    void Start()
    {
        lights = FindObjectsOfType<Light2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfInShadow();
    }

    void CheckIfInShadow()
    {

        for (int j = 0; j < detectShadowPoints.Length; j++)
        {
            
            for (int i = 0; i < lights.Length ; i++)
            {
                if (!Physics2D.Raycast(detectShadowPoints[j].position, lights[i].transform.position - detectShadowPoints[j].position, shadowCasters))
                {
    
                    // Debug.DrawLine(detectShadowPoints[j].position, lights[i].transform.position, Color.red);
                    gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                    isOnShadow = false;
                    return;
                }
                else 
                {
                    // Debug.DrawLine(detectShadowPoints[j].position, lights[i].transform.position, Color.blue);
                    gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                }   
            }
        }
        isOnShadow = true;

    }
}
