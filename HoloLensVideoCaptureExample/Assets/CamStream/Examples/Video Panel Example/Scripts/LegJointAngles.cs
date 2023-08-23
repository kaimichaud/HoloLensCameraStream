using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegJointAngles : MonoBehaviour
{
    public Text RKFE, LKFE, KSSR, KSSL, IRR, IRL, RFFE, LFFE;

    public Transform hip_r, hip_l, hip_mid, spine, ankle_r, ankle_l, knee_r, knee_l, heel_r, heel_l, toe_r, toe_l;

    public float knee_fe_angle_r, knee_fe_angle_l, knee_ss_angle_r, knee_ss_angle_l, internal_rotation_r, internal_rotation_l;
    public float foot_fe_angle_r, foot_fe_angle_l;

    private Vector3 shin_side_plane_r, shin_side_plane_l;//planes representing the side orientation of each shin
    private Vector3 shin_forward_plane_r, shin_forward_plane_l;//planes perpandicular to the side shin planes but pointing out of the front of the knee 
    private Vector3 thigh_side_plane_r, thigh_side_plane_l;
    private Vector3 thigh_forward_plane_r, thigh_forward_plane_l;//same as the shin planes but for thighs
    private Vector3 hips_forward_plane_r, hips_forward_plane_l;
    private Vector3 shin_r, shin_l;//vectors representing the direction of the shins
    private Vector3 foot_r, foot_l;//vectors representing the direction of the feet

    void Start()
    {
        
    }

    
    void Update()
    {
        //if (Input.GetKeyDown("j"))
        {
            CalculateJointAngles();
        }
    }

    public void CalculateJointAngles()
    {
        CalculateShinPlanes();
        CalculateThighPlanes();
        CalculateHipsPlanes();
        CalcualteFootPlanes();


        //calculating knee flexion/extension angles ----------------------------

        //right first -------
        knee_fe_angle_r = Vector3.Angle(thigh_forward_plane_r, shin_forward_plane_r);
        //then left ---------
        knee_fe_angle_l = Vector3.Angle(thigh_forward_plane_l, shin_forward_plane_l);


        //calculating knee side-to-side? angles ----------------------------

        //right first -------
        knee_ss_angle_r = Vector3.Angle(-thigh_side_plane_r, shin_side_plane_r);
        //then left ---------
        knee_ss_angle_l = Vector3.Angle(-thigh_side_plane_l, shin_side_plane_l);


        //calculating knee internal rotations ------------------------------
        //right first --------
        internal_rotation_r = Vector3.Angle(hips_forward_plane_r, thigh_forward_plane_r);
        //then left --------
        internal_rotation_l = Vector3.Angle(hips_forward_plane_l, thigh_forward_plane_l);


        //calculate foot flexion/extension
        //right first --------
        foot_fe_angle_r = Vector3.Angle(shin_r, foot_r);
        //then left --------
        foot_fe_angle_l = Vector3.Angle(shin_l, foot_l);


        //set text values
        RKFE.text = knee_fe_angle_r.ToString();
        LKFE.text = knee_fe_angle_l.ToString();

        KSSR.text = knee_ss_angle_r.ToString();
        KSSL.text = knee_ss_angle_l.ToString();

        IRR.text = internal_rotation_r.ToString();
        IRL.text = internal_rotation_l.ToString();

        RFFE.text = foot_fe_angle_r.ToString();
        LFFE.text = foot_fe_angle_l.ToString();

    }


    void CalculateShinPlanes()
    {
        //right first

        Vector3 A = knee_r.GetComponent<Joint>().tv1 - heel_r.GetComponent<Joint>().tv1;//vector between knee and heel
        A.Normalize();

        Vector3 B = toe_r.GetComponent<Joint>().tv1 - heel_r.GetComponent<Joint>().tv1;//vector beteween toe and heel
        B.Normalize();

        Vector3 C = Vector3.Cross(A, B).normalized;

        shin_side_plane_r = C;


        //then left

        A = knee_l.GetComponent<Joint>().tv1 - heel_l.GetComponent<Joint>().tv1;//vector between knee and heel
        A.Normalize();

        B = toe_l.GetComponent<Joint>().tv1 - heel_l.GetComponent<Joint>().tv1;//vector beteween toe and heel
        B.Normalize();

        C = Vector3.Cross(A, B).normalized;

        shin_side_plane_l = C;


        //forward planes
        //right first

        A = knee_r.GetComponent<Joint>().tv1 - ankle_r.GetComponent<Joint>().tv1;
        A.Normalize();

        B = shin_side_plane_r;

        C = Vector3.Cross(A, B).normalized;

        shin_forward_plane_r = C;


        //then left

        A = knee_l.GetComponent<Joint>().tv1 - ankle_l.GetComponent<Joint>().tv1;
        A.Normalize();

        B = shin_side_plane_l;

        C = Vector3.Cross(A, B).normalized;

        shin_forward_plane_l = C;


    }

    void CalculateThighPlanes()
    {
        //right first

        Vector3 A = hip_r.GetComponent<Joint>().tv1 - knee_r.GetComponent<Joint>().tv1;//vector from knee to hip
        A.Normalize();

        Vector3 B = shin_side_plane_r;//vector pointing out of the side of the knee
        B.Normalize();

        Vector3 C = Vector3.Cross(A, B).normalized;//vector coming out of the front of the thigh

        thigh_forward_plane_r = C;


        //then left

        A = hip_l.GetComponent<Joint>().tv1 - knee_l.GetComponent<Joint>().tv1;//vector from knee to hip
        A.Normalize();

        B = shin_side_plane_l;
        B.Normalize();

        C = Vector3.Cross(A, B).normalized;

        thigh_forward_plane_l = C;


        //then calcualte side planes for the thighs
        //right first

        A = hip_r.GetComponent<Joint>().tv1 - knee_r.GetComponent<Joint>().tv1;//vector from knee to hip
        A.Normalize();

        B = thigh_forward_plane_r;
        B.Normalize();

        C = Vector3.Cross(A, B).normalized;

        thigh_side_plane_r = C;

        //now left

        A = hip_l.GetComponent<Joint>().tv1 - knee_l.GetComponent<Joint>().tv1;//vector from knee to hip
        A.Normalize();

        B = thigh_forward_plane_l;
        B.Normalize();

        C = Vector3.Cross(A, B).normalized;

        thigh_side_plane_l = C;

    }

    void CalculateHipsPlanes()
    {
        Vector3 pelvis = hip_r.GetComponent<Joint>().tv1 - hip_l.GetComponent<Joint>().tv1;
        pelvis.Normalize();

        //right first
        Vector3 A = hip_r.GetComponent<Joint>().tv1 - knee_r.GetComponent<Joint>().tv1;

        Vector3 C = Vector3.Cross(pelvis, A);

        hips_forward_plane_r = C;

        //then left
        A = hip_l.GetComponent<Joint>().tv1 - knee_l.GetComponent<Joint>().tv1;

        C = Vector3.Cross(pelvis, A);

        hips_forward_plane_l = C;
    }

    void CalcualteFootPlanes()
    {
        shin_r = knee_r.transform.GetComponent<Joint>().tv1 - ankle_r.transform.GetComponent<Joint>().tv1;
        shin_r.Normalize();
        shin_l = knee_l.transform.GetComponent<Joint>().tv1 - ankle_l.transform.GetComponent<Joint>().tv1;
        shin_l.Normalize();

        foot_r = toe_r.transform.GetComponent<Joint>().tv1 - heel_r.transform.GetComponent<Joint>().tv1;
        foot_r.Normalize();
        foot_l = toe_l.transform.GetComponent<Joint>().tv1 - heel_l.transform.GetComponent<Joint>().tv1;
        foot_l.Normalize();
    }
}
