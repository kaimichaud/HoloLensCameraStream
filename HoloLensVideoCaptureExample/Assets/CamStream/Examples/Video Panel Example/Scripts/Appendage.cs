using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appendage : MonoBehaviour
{
    //public Transform tdappendage;

    private JointManager jm;

    Vector3 appOffsetx = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 appOffsety = new Vector3(0.0f, 0.0f, 0.0f);

    public Vector3 tv1, tv2;
    Vector3 lv1, lv2;


    private void Start()
    {
        jm = GameObject.FindGameObjectWithTag("BodyManager").GetComponent<JointManager>();
    }


    // Update is called once per frame
    void Update()
    {
        //SetAll(cy1);

        if(true)//lv1 != Vector3.zero
        {
            lv1 = Vector3.Lerp(lv1, tv1, 0.1f);
        }

        if(true)
        {
            lv2 = Vector3.Lerp(lv2, tv2, 0.1f);
        }
        GoToPosition(lv1, lv2);

    }

    public void SetPosition(Vector3 v1, Vector3 v2)
    {
        tv1 = v1;
        tv2 = v2;
    }

    public void GoToPosition(Vector3 v1, Vector3 v2)
    {
        appOffsetx = jm.offsetx;
        appOffsety = jm.offsety;

        v1.y = v1.y * -1;
        v2.y = v2.y * -1;
        if (v2.y < v1.y)
        {
            Vector3 temp = v2;
            v2 = v1;
            v1 = temp;
        }
        // Debug.Log(appOffset);

        if (((v1.x == -appOffsetx.x) && (v1.y == -appOffsety.y)) || ((v2.x == -appOffsetx.x) && (v2.y == -appOffsety.y)))
        {
            Debug.Log("hiding: " + this.gameObject.name);
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        //Debug.Log("z value" + v2.z);

        Vector3 between = v2 - v1;
        float scalez = Mathf.Sqrt(Mathf.Pow(between.y, 2.0f) + Mathf.Pow(between.x, 2.0f) + Mathf.Pow(between.z, 2.0f));

        Vector3 distance = between / between.magnitude;
        this.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, scalez);
        this.gameObject.transform.position = v1 + (between / 2.0f);
        this.gameObject.transform.LookAt(v2);

    }
}
