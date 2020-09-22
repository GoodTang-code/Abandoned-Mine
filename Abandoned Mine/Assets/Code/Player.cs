using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceAdded = 12000.0f;
    public float fuelTank = 10000f;
    public float fuelConsumpPerUnit = 0.0004f; // unit per fuelConsump
    public float calibrateValue = 4f; // calibrate from full power to only side power
    public float torque = 1200f; // Torque Power

    public GameObject engineObjLeft;
    public GameObject engineObjRight;
    private Engine engineLeft;
    private Engine engineRight;

    float fuelConsump;

    void Start()
    {
        engineLeft = engineObjLeft.GetComponent<Engine>();
        engineRight = engineObjRight.GetComponent<Engine>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
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
