using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggered : MonoBehaviour
{
    private DriveAgent driveAgent;
    public void OnTriggerEnter(Collider other)
    {
        // driveAgent = other.gameObject.transform.parent.parent.GetChild(0).GetComponent("DriveAgent") as DriveAgent;
        // driveAgent.Lights = gameObject;
        // if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        // {
        //     driveAgent.AddReward(2f);
        // }
        // if (gameObject.GetComponent<Renderer>().material.color == Color.red)
        // {
        //     driveAgent.AddReward(-5f);
        // }
        //other.GetChild(0).Done();
    }
}
