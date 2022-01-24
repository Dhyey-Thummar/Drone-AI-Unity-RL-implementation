using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float shootingInterval = 4f;

    private float shootingTimer;
    [SerializeField]
    private GameObject drone;
    [SerializeField]
    private KalmanFilterVector3 kalmanFilterClass;
    [SerializeField]
    private Vector3 KalmanTarget;
    [SerializeField] private Vector3[] vector3s;
    [SerializeField] private Vector3 vecTest;
    private Rigidbody drine;
    [SerializeField]
    private GameObject tracker;

    [SerializeField] private float kalSens = 1f;
    [SerializeField] private float shootingdistance = 3f;
    [SerializeField] private ObjectPooler objectPooler;

    void OnEnable()
    {
        objectPooler = this.transform.parent.transform.Find("ObjectPooler").gameObject.GetComponent<ObjectPooler>();
        shootingTimer = Random.Range(0, shootingInterval);
        vector3s = new Vector3[3];
        kalmanFilterClass.Reset();
        tracker = this.transform.parent.transform.Find("Sphere").gameObject;
        drone = this.transform.parent.transform.Find("Drone").gameObject;
        drine = drone.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        this.transform.LookAt(drone.transform.position);
        shootingTimer -= Time.fixedDeltaTime;
        vector3s[0] = drine.transform.position;
        vector3s[1] = drine.GetRelativePointVelocity(this.transform.parent.position);
        vector3s[2] = Drone.force / drine.mass;

        KalmanTarget = kalmanFilterClass.KUpdate(vector3s, true);
        /*            vecTest = drone.transform.position;

                    KalmanTarget = kalmanFilterClass.KUpdate(vecTest);*/
        /*
        Debug.Log(KalmanTarget.ToString()+" is the Kalman Output");
        Debug.Log(drone.transform.position + " is the current position");*/
        tracker.transform.position = KalmanTarget * kalSens;

        if (shootingTimer <= 0 && Vector3.Distance(transform.position, drone.transform.position) <= shootingdistance)
        {
            shootingTimer = shootingInterval;
            GameObject bullet = objectPooler.GetBullet(false);
            bullet.transform.position = this.transform.position;
            bullet.transform.forward = (tracker.transform.position - this.transform.position).normalized;
            /*bullet.transform.forward = (drone.transform.position + drone.GetComponent<Rigidbody>().velocity - this.transform.position).normalized;*/
        }
    }

    
}
