using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    private float rotationSpeed = 0f;
    Vector3 rotateSideWaysVector = new Vector3(0, 0, -1f);
    GameObject thePlayer;
    PlayerControllerX playerScript;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player");
        playerScript = thePlayer.GetComponent<PlayerControllerX>();
        Debug.Log("rs: " + rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.engineInertia > 0)
            rotationSpeed = playerScript.engineInertia * 1000f;
        transform.Rotate(rotateSideWaysVector * Time.deltaTime * rotationSpeed);
    }
}
