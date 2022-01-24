using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public float moveSpeed = 4;

    public Transform orientation;

    public float upSens = 100f;
    [SerializeField]
    public static Vector3 force;
    float x, y;
    public float speedMultiplier = 1f;
    private Rigidbody player;
    public float gravityMultiplier = 10f;
    void Awake()
    {
        player = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void FixedUpdate()
    {
        Movement();
        Lift();
    }

    void Update()
    {
        MyInput();
    }
    void Movement()
    {
        Vector3 up = Vector3.down *  gravityMultiplier;
        player.AddRelativeForce(up);
        Vector3 forward = orientation.transform.forward * y * moveSpeed *  speedMultiplier;
        player.AddRelativeForce(forward);
        Vector3 right = orientation.transform.right * x * moveSpeed *  speedMultiplier;
        player.AddRelativeForce(right);
        force = (up + forward + right);
    }
    void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }
    void Lift()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.AddRelativeForce(player.transform.up * upSens);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            player.AddRelativeForce(player.transform.up * -upSens);
        }
    }
}

