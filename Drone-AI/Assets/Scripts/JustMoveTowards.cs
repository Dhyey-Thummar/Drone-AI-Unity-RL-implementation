using UnityEngine;

public class JustMoveTowards : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 directionToTarget;
    Rigidbody droneRb;
    [SerializeField] float speedDrone = 10f;

    void Start()
    {
        target = this.transform.parent.GetComponentInChildren<target>().transform;
        droneRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        directionToTarget = target.localPosition - transform.localPosition;
        
        droneRb.velocity = directionToTarget.normalized*speedDrone*Time.fixedDeltaTime;
        transform.forward = droneRb.velocity;
    }
}
