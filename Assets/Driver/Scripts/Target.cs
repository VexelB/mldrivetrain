using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private DriveAgent driveAgent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        driveAgent = other.gameObject.transform.parent.parent.GetChild(0).GetComponent("DriveAgent") as DriveAgent;
        driveAgent.AddReward(1f);
        driveAgent.Res();
        driveAgent.Target.localPosition = new Vector3(Random.Range(-27f,30f),0,Random.Range(-68f,5f));
    }
}
