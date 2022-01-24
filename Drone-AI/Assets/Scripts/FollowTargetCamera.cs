using UnityEngine;

public class FollowTargetCamera : MonoBehaviour
{
    public Transform Target;
    public float PositionFollowForce = 5f;
    public float RotationFollowForce = 5f;
    void FixedUpdate()
	{
        Vector3 vector = Vector3.forward;
        Vector3 dir = Target.rotation * Vector3.forward;
		dir.y = 0f;
        if (dir.magnitude > 0f) vector = dir / dir.magnitude;

        transform.position = Vector3.Lerp(transform.position, Target.position, PositionFollowForce * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vector), RotationFollowForce * Time.deltaTime);
	}
}

