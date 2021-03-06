using UnityEngine;

class MomentumMovement : MonoBehaviour
{
    /*public GameObject playerCamera;*/
    public GameObject playerModel;

    Rigidbody rb;
    float speed = 400f;
    Vector3 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastVelocity = rb.velocity;
    }

    Vector3 ScaleDirectionVector(Vector3 direction)
    {
        float multiplier = 1 / (Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
        return new Vector3(
            direction.x * multiplier,
            0,
            direction.z * multiplier
        );
    }

    /*    void Move()
        {
            Vector3 moveVector = ScaleDirectionVector(playerCamera.transform.forward) * Input.GetAxis("Vertical");
            moveVector += ScaleDirectionVector(playerCamera.transform.right) * Input.GetAxis("Horizontal");
            moveVector *= speed * Time.deltaTime;
            controller.SimpleMove(moveVector);
            playerModel.transform.position = transform.position;
        }*/

    void RotateToVelocity()
    {
        Vector3 lookAt = transform.position + rb.velocity.normalized;
        Vector3 targetPosition = new Vector3(lookAt.x, transform.position.y, lookAt.z);
        if (targetPosition - transform.position != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 500 * Time.deltaTime);
        }

    }

    Vector3 CalculateTilt(Vector3 acceleration)
    {
        acceleration.y = 0;
        Vector3 tiltAxis = Vector3.Cross(acceleration, Vector3.up);
        float angle = Mathf.Clamp(-acceleration.magnitude, -60, 60);
        Quaternion targetRotation = Quaternion.AngleAxis(angle, tiltAxis) * transform.rotation;
        return targetRotation.eulerAngles;
    }

    void TiltToAcceleration()
    {
        Vector3 centerOfMass = rb.centerOfMass;
        Vector3 acceleration = rb.velocity / Time.deltaTime - lastVelocity;
        Vector3 tilt = CalculateTilt(acceleration);
        Quaternion targetRotation = Quaternion.Euler(tilt);
        playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, targetRotation, 10 * Time.deltaTime);
    }

    void FixedUpdate()
    {
        /*Move();*/
        RotateToVelocity();
        TiltToAcceleration();
        lastVelocity = rb.velocity / Time.deltaTime;
    }
}