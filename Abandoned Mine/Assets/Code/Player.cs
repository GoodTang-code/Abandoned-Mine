using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceAdded = 12000.0f;
    public float fuelTank = 10000;
    public float fuelConsump;
    public float fuelConsumpPerUnit = 0.0004f; // unit per fuelConsump
    //public float veloX;
    //public float veloY;
    public float calibrateValue = 4f;
    public Vector2 pos;
    public Vector2 pos2;
    public float torque = 1200;

    public GameObject engineObjLeft;
    public GameObject engineObjRight;
    private Engine engineLeft;
    private Engine engineRight;

    // Start is called before the first frame update
    void Start()
    {

        engineLeft = engineObjLeft.GetComponent<Engine>();
        engineRight = engineObjRight.GetComponent<Engine>();
    }

    // Update is called once per frame
    void Update()
    {
        //veloX = rb.velocity.x;
        //veloY = rb.velocity.y;
        
    }

    void FixedUpdate()
    {
        //if (Input.GetKey(KeyCode.Z))  //Input.GetAxis("Horizontal") < 0.0f
        //{
        //    // Add force * press time
        //    // addForceAtPosition
        //    //rb.AddForce(transform.up * forceAdded);
        //    if (Input.GetKey(KeyCode.M)) rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos);
        //    else rb.AddForceAtPosition(transform.up * forceAdded / calibrateValue, rb.position + pos);
        //}
        //if (Input.GetKey(KeyCode.M))
        //{
        //    if (Input.GetKey(KeyCode.Z)) rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos2);
        //    else rb.AddForceAtPosition(transform.up * forceAdded / calibrateValue, rb.position + pos2);
        //}

        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.M)){
            rb.AddForce(transform.up * forceAdded);
            fuelConsump = forceAdded * fuelConsumpPerUnit;
            fuelTank -= fuelConsump;

            engineLeft.thrusterOn = true;
            engineRight.thrusterOn = true;
        }
        else if (Input.GetKey(KeyCode.Z)){
            rb.AddTorque(torque * -1);
            rb.AddForce(transform.up * forceAdded / calibrateValue);
            fuelConsump = (forceAdded / calibrateValue) * fuelConsumpPerUnit + torque * fuelConsumpPerUnit;
            fuelTank -= fuelConsump;
            engineLeft.thrusterOn = true;
        }
        else if (Input.GetKey(KeyCode.M)){
            rb.AddTorque(torque );
            rb.AddForce(transform.up * forceAdded / calibrateValue);
            fuelConsump = (forceAdded / calibrateValue) * fuelConsumpPerUnit + torque * fuelConsumpPerUnit;
            fuelTank -= fuelConsump;
            engineRight.thrusterOn = true;
        }
        else
        {
            fuelConsump = 0;

            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = false;
        }

        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).position.x < Screen.width/2 && Input.GetTouch(0).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos);
        //    }
        //    else if (Input.GetTouch(0).position.x > Screen.width/2 && Input.GetTouch(0).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos2);
        //    }

        //    if (Input.GetTouch(1).position.x < Screen.width/2 && Input.GetTouch(1).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos);
        //    }
        //    else if (Input.GetTouch(1).position.x > Screen.width/2 && Input.GetTouch(1).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos2);
        //    }
        //}
    }
}
