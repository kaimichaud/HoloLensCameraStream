using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimationInterpolator : MonoBehaviour
{

    public JointManager jm;
    Vector3[] current_points = new Vector3[30];
    Vector3[] previous_points = new Vector3[30];
    Vector3[] desired_points = new Vector3[30];

    float time_between_frames;
    float current_time_counter;

    bool animated_the_step;


    void Start()
    {
        current_points = jm.GetPoints();//set all the pooitns information here
        previous_points = current_points;
        time_between_frames = 0.1f;//initial guess
    }

    
    void Update()
    {
        current_time_counter += Time.deltaTime;//keep the timer counting

        if(current_time_counter > (time_between_frames / 2.0f) && !animated_the_step)
        {
            CalculateNewDesiredPositions();


            animated_the_step = true;//set flag to true so we dont keep setting them after the timer has reached halfway

        }

    }


    public void CalculateNewDesiredPositions()//this calculates new positiosn for the half step and sets the older point values then sets the desired points
    {

        for (int i = 0; i < current_points.Length; i++)//new positions
        {
            desired_points[i] = Vector3.Lerp(previous_points[i], current_points[i], 2f);
        }

        previous_points = current_points;//set the older points

        //setting desired points
        jm.inputPoints = desired_points;
        jm.SetJoints();
        jm.SetBodyFromPoints();
    }

    public void ReceiveUpdate()//this updates the newest point information
    {
        current_points = jm.GetPoints();//get newest points //might have to move this if it takes time for the information to be processed we'll see
        time_between_frames = 0.2f;
        current_time_counter = 0;
        animated_the_step = false;//set flag to false so we can move the half step again
    }
}
