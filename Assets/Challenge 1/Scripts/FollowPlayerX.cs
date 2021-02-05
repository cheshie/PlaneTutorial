using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    //-----Privates variables-----\\
    private Vector3 offset;

    //-----Publics variables-----\\
    [Header("Variables")]
    public GameObject plane;

    [Space]
    [Header("Position")]
    public float camPosX;
    public float camPosY;
    public float camPosZ;

    [Space]
    [Header("Rotation")]
    public float camRotationX;
    public float camRotationY;
    public float camRotationZ;

    public float turnSpeed = 2f;

    //-----Privates functions-----\\
    private void Start()
    {
        offset = new Vector3(0.02f, 6.65f, -11.14f);
        transform.rotation = Quaternion.Euler(camRotationX, camRotationY, camRotationZ);
    }
    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offset;
        }
        transform.position = plane.transform.position + offset;
        transform.LookAt(plane.transform.position);
    }

    /*public GameObject plane;
    
    public float sensitivity = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!Input.GetMouseButtonDown(0))
        {
            transform.position = plane.transform.position + offset;
            var c = Camera.main.transform;
            c.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            c.Rotate(-Input.GetAxis("Mouse Y") * sensitivity, 0, 0);
            c.Rotate(0, 0, -Input.GetAxis("QandE") * 90 * Time.deltaTime);
        }
    }*/
}
