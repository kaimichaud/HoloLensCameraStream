using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAngle : MonoBehaviour
{
    /*public Joint first;
    public Joint second;
    public Joint third;*/

    public Appendage first;
    public Appendage second;

    public Transform visual_transform;

    public float joint_angle;
    void Start()
    {
        
    }

    void Update()
    {
        CalculateJointAngle();

        UpdateVisual();
    }


    float CalculateJointAngle()//calculates the angle at a joint based on the joint before and after it
    {
        Vector3 first_vec = first.tv2 - first.tv1;
        Vector3 second_vec = second.tv2 - second.tv1;


        float angle = Vector3.Angle(first_vec, second_vec);

        joint_angle = angle;

        return angle;
    }

    void UpdateVisual()
    {
        visual_transform.localEulerAngles = new Vector3(visual_transform.eulerAngles.x, visual_transform.eulerAngles.y, joint_angle);//update the visuals based on the angle
    }
}
