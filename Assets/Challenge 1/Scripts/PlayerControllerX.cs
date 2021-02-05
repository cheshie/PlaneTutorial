using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    //public AnimationCurve c;
    public float speed = 15;
    public float rotationSpeed = 100;
    public float tiltspeed = 100;
    public float verticalInput;
    public float horzInput;
    public float mouseInputV;
    public float mouseInputH;
    public float engineInertia = 0.0f;
    public bool started = false;
    public float maxDrag = 3f;
    public float startingEngineCoeff = 0.08f;
    public float maxSpeed = 3f;
    // Describes how much force is used to pull plane downwards by gravity
    public float massForce = 1f;
    // Vector needed to rotate sideways on left/right arrows
    Vector3 rotateSideWaysVector = new Vector3(0, 0, -1f);


    Rigidbody rgbd;
    Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        //c.Evaluate(startingEngineCoeff);

        rgbd = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical input
        verticalInput  = Input.GetAxis("Vertical");
        horzInput      = Input.GetAxis("Horizontal");
        mouseInputV    = Input.GetAxis("Mouse Y");
        mouseInputH    = Input.GetAxis("Mouse X");

        engineInertia = Mathf.Clamp(engineInertia + verticalInput * startingEngineCoeff * Time.fixedDeltaTime, 0, maxSpeed);

        rgbd.drag = Mathf.Min(rgbd.drag, maxDrag);

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseRotation = new Vector3(mouseInputV, 0f, -mouseInputH);
            // tilt the plane up/down based on up/down mouse move
            transform.Rotate(Vector3.right, tiltspeed * Time.deltaTime * mouseInputV);
            // rotate sideways  
            transform.Rotate(mouseRotation * rotationSpeed * Time.deltaTime);
        }

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * speed * engineInertia * Time.deltaTime);
        rgbd.drag += engineInertia;
        // rotate sideways
        transform.Rotate(rotateSideWaysVector * rotationSpeed * Time.deltaTime * horzInput);
    }

    // If collided during the flight
    private void OnCollisionEnter(Collision collision)
    {
        if (started)
        {
            // disable engine and create force straight down
            rgbd.drag = 0;
            engineInertia = 0;
            transform.Translate(Vector3.down * speed * engineInertia * Time.deltaTime);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!started)
        {
            if (engineInertia >= 10f)
            {
                if (pos.position.y > 3f)
                {
                    started = true;
                }
            }
        }
    }
}
