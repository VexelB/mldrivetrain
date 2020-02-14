using UnityStandardAssets.Vehicles.Car;
using MLAgents;
using UnityEngine;

public class DriveAgent : Agent
{
    [SerializeField] private Transform road;
    [SerializeField] private CarController carCtrl;
    [SerializeField] private CollisionDetection collisionDetection;
    [SerializeField] private RoadDetection[] roadDetection;
    [SerializeField] private float speedRewardMultiplier = 0.0025f;
    [SerializeField] public Transform Target;
    [SerializeField] public GameObject Lights;
    private Transform environment;
    private Resetter resetter;
    private Vector3 localCarPos;
    private int collisionCount;
    
    public override void InitializeAgent()
    {
        environment = transform.parent.parent;
        resetter = new Resetter(environment);
        collisionDetection.CollisionCallback = OnObstacleCollision;
    }

    public override void CollectObservations()
    {
        AddVectorObs(Sigmoid(carCtrl.LocalVelocity)); // 3
        AddVectorObs(Sigmoid(carCtrl.LocalAngularVelocity)); // 3
        AddVectorObs(carCtrl.NormalizedSteerAngle); // 1
        AddVectorObs(Vector3.Distance(carCtrl.transform.localPosition,Target.localPosition)); // 3
        AddVectorObs(this.transform.localPosition); // 3
        AddVectorObs(Target.transform.localPosition); // 3
        //AddVectorObs(carCtrl.transform.rotation); // 3
    }

    public override void AgentAction(float[] vectorAction)
    {
        carCtrl.Move(vectorAction[0], vectorAction[1]);
        IsOnRoad();
        AddReward(-0.001f);
        if (GetCumulativeReward() < -2f || collisionCount > 1)
        {
            Res();
        }
    }

    private void IsOnRoad()
    {
        for (int i = 1; i < 5; i++)
        {
            if (!roadDetection[i].IsOnRoad(out Vector3 position))
            {
                AddReward(-0.03f);
            }
        }
    }

    private void OnObstacleCollision()
    {
        AddReward(-1f);
        collisionCount++;
        Res();
    }

    private static float Sigmoid(float val)
    {
        return val / (1f + Mathf.Abs(val));
    }

    private static Vector3 Sigmoid(Vector3 v3)
    {
        v3.x = Sigmoid(v3.x);
        v3.y = Sigmoid(v3.y);
        v3.z = Sigmoid(v3.z);
        return v3;
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
    public void Res()
    {
        AddReward(0.1f/Vector3.Distance(carCtrl.transform.position,Target.localPosition));
        resetter.Reset();
        Done();
        collisionCount = 0;
        
        // float t = Random.Range(0,3f);
        // if (t <= 1f)
        // {
        //     Target.localPosition = new Vector3(53f,0,-82f);
        //     t = Random.Range(0,1f);
        //     if (t <= 0.5f)
        //     {
        //         carCtrl.transform.localPosition = new Vector3(8f,0,10f);
        //     }
        //     else 
        //     {
        //         carCtrl.transform.localPosition = new Vector3(-36f,0,-82f);
        //     }
        // }
        // else if ( t > 1f && t <= 2f)
        // {
        //     Target.localPosition = new Vector3(-39f,0,-82f);
        //     t = Random.Range(0,1f);
        //     if (t <= 0.5f)
        //     {
        //         carCtrl.transform.localPosition = new Vector3(8f,0,10f);
        //     }
        //     else 
        //     {
        //         carCtrl.transform.localPosition = new Vector3(53f,0,-82f);
        //     }
        // }
        // else if (t > 2f)
        // {
        //     Target.localPosition = new Vector3(8f,0,10f);
        //     t = Random.Range(0,1f);
        //     if (t <= 0.5f)
        //     {
        //         carCtrl.transform.localPosition = new Vector3(-36f,0,-82f);
        //     }
        //     else 
        //     {
        //         carCtrl.transform.localPosition = new Vector3(53f,0,-82f);
        //     }
        // }
        //Target.localPosition = new Vector3(Random.Range(-35f,53f),0,-82);
    }
}
