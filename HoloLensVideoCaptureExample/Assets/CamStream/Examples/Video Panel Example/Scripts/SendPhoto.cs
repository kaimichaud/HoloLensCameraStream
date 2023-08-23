using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPhoto : MonoBehaviour
{

    public float time_between_frames;
    private float counter;

    public TCPClient client;
    public VideoPanelApp vpa;

    public BodyAnimationInterpolator bai;
    LegJointAngles lja;

        
    void Start()
    {
        counter = -1;
    }

    void Update()
    {
        if(client.Connected && counter >= 0)//check if the client is connected and not waiting for data
        {
            if(counter < time_between_frames)
            {
                counter += Time.deltaTime;//wait
            }
            else
            {
                counter = -1;
                vpa.SendSingleFrameNoAsync();//send data
                //lja.CalculateJointAngles();//NOT WORKING!! 
            }
        }
    }


    public void SendAPhoto()
    {
        if (client.Connected)
        {
            counter = 0.00001f;//reset the timer

            if(bai != null)//update the poitns
            {
                bai.ReceiveUpdate();
            }
        }
    }
}
