using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DroneAgent : Agent
{
    [SerializeField] private Transform target;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private Material neutralMat;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    public float maxAngle = 30f;
    public float multiplier = 20f;

    public float moveSpeed = 1f;
    public float clock;
    public int test = 0;


    public override void OnEpisodeBegin()
    {
        test++;
        Debug.Log("test = "+test);
        floorMeshRenderer.material = neutralMat;
        float x = Random.Range(-80, 80);
        float y = Random.Range(-80, 80);
        float z = Random.Range(-80, 80);
        transform.localPosition = new Vector3(x,y,z);
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<TrailRenderer>().Clear();
        clock = 0.0f;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
        /*foreach(Bullet bullet in FindObjectsOfType<Bullet>())
        {
            sensor.AddObservation(bullet.transform.position);
        }*/
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveY = actions.ContinuousActions[2];
        transform.localPosition += new Vector3(moveX, moveY, moveZ) * Time.fixedDeltaTime * moveSpeed;

        Vector3 angles = new Vector3(moveZ, 0f, moveX) * multiplier;
        angles = new Vector3(Mathf.Clamp(angles.x, -maxAngle, maxAngle), angles.y, Mathf.Clamp(angles.z, -maxAngle, maxAngle)); 
        transform.localEulerAngles = angles;
        transform.LookAt(target);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<target>(out target target))
        {
            SetReward(5000f);
            Debug.Log(this.GetCumulativeReward());
            Debug.Log("Hit Target Yes");
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        else if (other.TryGetComponent<wall>(out wall wall))
        {
            SetReward(-1000f);
            Debug.Log("Hit Wall");
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
        else if (other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            SetReward(-1000f);
            Debug.Log("Bullet Hit");
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        continuousActions[2] = Input.GetKeyDown(KeyCode.LeftShift) ? 1 : 0;
    }

    /*float distanceA = 9999999f,distNew;*/
    void FixedUpdate()
    {
        clock += Time.deltaTime;
        
        if (clock >= 60f)
        {
            SetReward(-5000f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
        /*AddReward(-5f);
        distNew = (this.transform.localPosition - target.localPosition).magnitude;
        AddReward(-distNew * 100);
        AddReward(2000f/distNew);*/
    }
}
