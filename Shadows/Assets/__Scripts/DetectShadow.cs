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

    private bool IsVisible ;

    public Transform playerPosition;




    [SerializeField] Transform checkAngle;

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

    // Update is called once per frame
    void Update()
    {
        CheckIfInShadow();

        // if(isVisible){

        //     SendPlayerPositionIfOnSun();
        // }

        Debug.Log("Is visible = " + isVisible);

    }

    void SendPlayerPositionIfOnSun()
    {
        Debug.Log($"Player Position real time: {playerPosition.position}");
    }

    bool InLightSpot(Light2D light, Transform detectPoint){

        for (int i = 0; i < lights.Length ; i++)
        {

            Vector2 direction = transform.position - light.transform.position;
            float startAngle = 90 - light.pointLightOuterAngle/2 +  light.transform.eulerAngles.z;
            float endAngle = 90 + light.pointLightOuterAngle/2 +  light.transform.eulerAngles.z;

            if(startAngle >= 360){
                startAngle -= 360;
            }

            if(endAngle >= 360){
                endAngle -= 360;
            }


            float r = Mathf.Sqrt(Mathf.Pow(detectPoint.position.x - light.transform.position.x,2) + Mathf.Pow(detectPoint.position.y - light.transform.position.y,2));
            float a = 180 + (Mathf.Atan2(light.transform.position.y - detectPoint.position.y, light.transform.position.x - detectPoint.position.x )) * Mathf.Rad2Deg;

            // Debug.Log("R from Light " + lights[i].name + " = " + lights[i].pointLightOuterRadius);
            // Debug.Log("r from Light " + lights[i].name + " = " + r);


            if(r < light.pointLightOuterRadius){
                //The player is inside the light radius
                if (startAngle < endAngle){
                    if (startAngle < a && a < endAngle){
                        // Debug.Log("The corner " + detectPoint.name + " is at light " + i + " field of view");
                        return true;
                    } else {

                        // Debug.Log("The corner " + detectPoint.name + " is not at light " + i + " field of view");
                    }
                } else{

                    if (a > startAngle && a < endAngle){
                        // Debug.Log("The corner " + detectPoint.name + " is at light " + i + " field of view");
                        return true;
                    } 
                }

                // Debug.Log("The corner " + detectPoint.name + " is not at light " + i + " field of view");
            } 
            // if (r > lights[i].pointLightOuterRadius){
            //     //The player is outside of the light radius
            //     Debug.Log("Outside slice, too far from the light!");
            //     continue;
            // } else if (startAngle < endAngle){
            //     if ( startAngle < a && a < endAngle){
            //         Debug.Log("Inside slice");
            //     } else{

            //         Debug.Log("Outside slice");
            //         continue;
            //     }

            // } else if (startAngle > endAngle){
            //     if (a > startAngle){
            //         Debug.Log("Inside slice");
            //     } else if ( a < endAngle){
            //         Debug.Log("Inside slice");
            //     } else{
            //         Debug.Log("Outside slice");
            //         continue;
            //     }
            // } else {
            //     Debug.Log("Outside slice");
            //     continue;
            // }

            // Debug.Log("start: " + startAngle);
            // Debug.Log("end: " + endAngle);
            // Debug.Log("Angle: " + (180 + Mathf.Atan2(lights[i].transform.position.y - checkAngle.position.y, lights[i].transform.position.x - checkAngle.position.x) * Mathf.Rad2Deg));
        }

        return false;

    }

    void CheckIfInShadow()
    {

        for (int j = 0; j < detectShadowPoints.Length; j++)
        {

            for (int i = 0; i < lights.Length ; i++){


                if (InLightSpot(lights[i], detectShadowPoints[j]))
                {

                    if (Physics2D.Raycast(detectShadowPoints[j].position, lights[i].transform.position - detectShadowPoints[j].position, shadowCasters))
                    {
                        
                        Debug.DrawLine(detectShadowPoints[j].position, lights[i].transform.position, Color.red);
                        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                        // Debug.Log("The corner " + detectShadowPoints[j].name + " is behind something for the light " + lights[i].name);
                    }
                    else 
                    {
                        Debug.DrawLine(detectShadowPoints[j].position, lights[i].transform.position, Color.blue);
                        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        // Debug.Log("The corner " + detectShadowPoints[j].name + " is visible for the light " + lights[i].name);
                        isVisible = true;
                        return;
                    }   
                }

            }    
        }
        
        isVisible = false;

    }
}
