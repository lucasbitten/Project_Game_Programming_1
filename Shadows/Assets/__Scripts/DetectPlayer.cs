using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DetectPlayer : MonoBehaviour
{

    PlayerController player;
    Light2D light2D;
    float startAngle, endAngle;
    Transform[] playerShadowPoints;
    [SerializeField] LayerMask shadowCastersLayer;
    void Awake()
    {
        shadowCastersLayer = LayerMask.GetMask("ShadowCaster");
        player = FindObjectOfType<PlayerController>();
        light2D = GetComponent<Light2D>();
        playerShadowPoints = player.shadowPoints;
    }

    void Update()
    {
        IsOnRange();
    }

    void GetAngles(){
        // Angles where the cone of light begins and ends
        startAngle = 90 - light2D.pointLightOuterAngle/2 +  transform.eulerAngles.z;
        endAngle = 90 + light2D.pointLightOuterAngle/2 +  transform.eulerAngles.z;

        // Keep the angles always between 0 and 360
        // if(startAngle >= 360)
        // {
        //     startAngle -= 360;
        // }

        // if(endAngle >= 360)
        // {
        //     endAngle -= 360;
        // }
    }

    void IsOnRange(){

        GetAngles();

        for (int i = 0; i < playerShadowPoints.Length; i++)
        {
            // Converting the radius and the angle to polar coordinates
            float playerDistanceToLight = Mathf.Sqrt(Mathf.Pow(playerShadowPoints[i].position.x - transform.position.x,2) + Mathf.Pow(playerShadowPoints[i].position.y - transform.position.y,2));

            float angleBetweenPlayerAndLight = 180 + (Mathf.Atan2(transform.position.y - playerShadowPoints[i].position.y,
                transform.position.x - playerShadowPoints[i].position.x )) * Mathf.Rad2Deg;
        
            if(playerDistanceToLight < light2D.pointLightOuterRadius) // If The player is inside the light radius
            { 

                if (startAngle < endAngle)
                {
                    if (startAngle < angleBetweenPlayerAndLight && angleBetweenPlayerAndLight < endAngle)
                    {

                        CheckIfInShadow(); // One corner is spottable, so the player is also spottable 
                        return;
                    }

                } else if (angleBetweenPlayerAndLight > startAngle && angleBetweenPlayerAndLight < endAngle)
                {
                    CheckIfInShadow(); // One corner is spottable, so the player is also spottable 
                    return;

                }
            }
        }
        // None of the corners is spottable so the player is not spottable
    }

    void CheckIfInShadow(){

        for (int j = 0; j < playerShadowPoints.Length; j++)
        {

            //If the player corner is not behind something that cast shadows, isVisible = true and return the method
            if (!Physics2D.Raycast(playerShadowPoints[j].position, transform.position - playerShadowPoints[j].position, 
                Vector3.Distance(playerShadowPoints[j].position, transform.position ), shadowCastersLayer))
            {
                Debug.DrawLine(playerShadowPoints[j].position, transform.position, Color.red);
                
                if (!player.lights.Contains(this.gameObject)){
                    player.lights.Add(this.gameObject);
                }
                return;
            }     
        }
        // If any of the corners are visible, the player is not visible, so isVisible = false
        if (player.lights.Contains(this.gameObject)){
            player.lights.Remove(this.gameObject);
        }
    }    
}
