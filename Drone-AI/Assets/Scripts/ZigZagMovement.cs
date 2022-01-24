using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 directionToTarget;
    Rigidbody droneRb;
    [SerializeField] float speedDrone = 10f;
    [SerializeField] Vector3 VelocityComponentTowardsTarget;
    [SerializeField] Vector3 ZigZagComponentVelocity;
    [SerializeField] float frequency = 20;
    [SerializeField] float Amplitude = 4f;

    void Awake()
    {
        target = this.transform.parent.GetComponentInChildren<target>().transform;
        droneRb = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        
        VelocityComponentTowardsTarget = (target.localPosition - transform.localPosition).normalized;
        ZigZagComponentVelocity = Vector3.Cross(VelocityComponentTowardsTarget, Vector3.up).normalized;
        directionToTarget = VelocityComponentTowardsTarget + (ZigZagComponentVelocity * Mathf.Sin(Time.fixedTime*frequency))*Amplitude;
        droneRb.velocity = directionToTarget * speedDrone * Time.fixedDeltaTime;
        transform.forward = droneRb.velocity;
    }

}
