using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.Rendering.Universal;


public class DetectShadow : MonoBehaviour
{

    Light2D[] lights;

    
    [SerializeField] LayerMask shadowCasters; // Layer masks with all the objects that cast shadows
    [SerializeField] Transform[] detectShadowPoints; // Corners of the player to detect if he is at shadow or visible
    [SerializeField] Transform checkAngle;



    private bool IsVisible;
    public bool isVisible
    {
        get { return IsVisible ; }
        set
        {
            if( value == IsVisible )
                return ;
    
            IsVisible = value ;
            if( IsVisible )
            {
                Debug.Log("The player is visible");
            } else{
                Debug.Log("The player is not visible");
            }    
        }    
    }


    void Start()
    {
        lights = FindObjectsOfType<Light2D>();
    }

    void Update()
    {
        CheckIfInShadow();

        if(isVisible)
        {

            GetPlayerPosition();
        }
    }

    void GetPlayerPosition()
    {
        Debug.Log($"Player Position real time: {transform.position}");
    }

    bool InLightSpot(Light2D light, Transform detectPoint) // Method to check if the player is at the light range

    { 
        // Angles where the cone of light begins and ends
        float startAngle = 90 - light.pointLightOuterAngle/2 +  light.transform.eulerAngles.z;
        float endAngle = 90 + light.pointLightOuterAngle/2 +  light.transform.eulerAngles.z;

        // Keep the angles always between 0 and 360
        if(startAngle >= 360)
        {
            startAngle -= 360;
        }

        if(endAngle >= 360)
        {
            endAngle -= 360;
        }

        // Converting the radius and the angle to polar coordinates
        float r = Mathf.Sqrt(Mathf.Pow(detectPoint.position.x - light.transform.position.x,2) + Mathf.Pow(detectPoint.position.y - light.transform.position.y,2));

        float angleBetweenPlayerAndLight = 180 + (Mathf.Atan2(light.transform.position.y - detectPoint.position.y,
            light.transform.position.x - detectPoint.position.x )) * Mathf.Rad2Deg;

        // Checking if the light can spot the player. If any corner is spottable  the whole player is spottable 

        if(r < light.pointLightOuterRadius) // If The player is inside the light radius
        { 

            if (startAngle < endAngle)
            {
                if (startAngle < angleBetweenPlayerAndLight && angleBetweenPlayerAndLight < endAngle)
                {

                    return true; // One corner is spottable, so the player is also spottable 

                }

            } else if (angleBetweenPlayerAndLight > startAngle && angleBetweenPlayerAndLight < endAngle)
            {

                return true; // One corner is spottable, so the player is also spottable 
            
            }
        }
        // None of the corners is spottable so the player is not spottable
        return false;

    }

    void CheckIfInShadow() // Method to check if the player is at any shadow
    {
        // Check if all corners are at shadow
        for (int j = 0; j < detectShadowPoints.Length; j++)
        {

            // Check if the player is at shadow for all lights on scene
            for (int i = 0; i < lights.Length ; i++)
            {

                if (InLightSpot(lights[i], detectShadowPoints[j])) // Check for shadows only if the player is at the light range
                {
                    //If the player corner is behind something that cast shadows, continue the loop
                    if (Physics2D.Raycast(detectShadowPoints[j].position, lights[i].transform.position - detectShadowPoints[j].position, shadowCasters))
                    {

                        Debug.DrawLine(detectShadowPoints[j].position, lights[i].transform.position, Color.red);
                        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

                    } else // If the player corner is not behind something the whole player is visible, so isVisible = true and stop the method
                    {
                        Debug.DrawLine(detectShadowPoints[j].position, lights[i].transform.position, Color.blue);
                        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        isVisible = true;
                        return;
                    }   
                }
            }    
        }

        // If any of the corners are visible, the player is not visible, so isVisible = false
        isVisible = false;

    }
}
