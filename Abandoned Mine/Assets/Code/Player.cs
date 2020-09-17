﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceAdded = 30000.0f;
    public float veloX;
    public float veloY;
    public float calibrateValue = 2.5f;
    public Vector2 pos;
    public Vector2 pos2;
    public float torque;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        veloX = rb.velocity.x;
        veloY = rb.velocity.y;
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
        }
        else if (Input.GetKey(KeyCode.Z)){
            rb.AddTorque(torque * -1);
            rb.AddForce(transform.up * forceAdded / calibrateValue);

        }
        else if (Input.GetKey(KeyCode.M)){
            rb.AddTorque(torque );
            rb.AddForce(transform.up * forceAdded / calibrateValue);

        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x < Screen.width/2 && Input.GetTouch(0).position.y < Screen.height/2)
            {
                rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos);
            }
            else if (Input.GetTouch(0).position.x > Screen.width/2 && Input.GetTouch(0).position.y < Screen.height/2)
            {
                rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos2);
            }

            if (Input.GetTouch(1).position.x < Screen.width/2 && Input.GetTouch(1).position.y < Screen.height/2)
            {
                rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos);
            }
            else if (Input.GetTouch(1).position.x > Screen.width/2 && Input.GetTouch(1).position.y < Screen.height/2)
            {
                rb.AddForceAtPosition(transform.up * forceAdded, rb.position + pos2);
            }
        }
    }
}
