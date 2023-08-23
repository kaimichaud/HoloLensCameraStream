using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    public GameObject JointObject;
    public Vector3 tv1;
    Vector3 lv1;

    public bool pointed;
    public Transform prev, next;

    public Transform tdappendage;

    // Update is called once per frame
    void Update()
    {
        if(true)//lv1 != Vector3.zero
        {
            lv1 = Vector3.Lerp(lv1, tv1, 0.1f);
        }
        GoToPointer(lv1);
    }

    public void SetPointer(Vector3 v1)
    {
        tv1 = v1;
    }

    void GoToPointer(Vector3 v1)
    {
        v1.y = v1.y * -1;
        //transform.position = v1;
        JointObject.transform.position = v1;

        if (tdappendage != null)
        {
            tdappendage.position = transform.position;//copy the position of this appendage object to the 3d models bones

            if (pointed)
            {
                Vector3 diff = next.transform.position - transform.position;//calcualte the direction to the next joint
                diff.Normalize();
                tdappendage.transform.up = diff;

            }

            

           
            //Transform temp = new GameObject().transform;
            //temp.position = transform.position;
            //temp.rotation = transform.rotation;
            //temp.Rotate(new Vector3(0, 90, 0));
            //tdappendage.rotation = temp.rotation;//copy the rotation of this appendage object to the 3d models bones
        }
    }

}
