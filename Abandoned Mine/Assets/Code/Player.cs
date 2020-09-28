using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Math.Round

public class Player : MonoBehaviour
{
    public playerSO playerSO;

    public Rigidbody2D rb;
    float torque; // Torque Power
    public float velo;

    public GameObject engineObjLeft;
    public GameObject engineObjRight;
    public GameObject gearObjLeft;
    public GameObject gearObjRight;

    public Engine engineLeft;
    public Engine engineRight;
    public LandingGear gearLeft;
    public LandingGear gearRight;

    float fuelConsump;
    float maxDamage;

    void Start()
    {
        engineLeft = engineObjLeft.GetComponent<Engine>();
        engineRight = engineObjRight.GetComponent<Engine>();
        gearLeft = gearObjLeft.GetComponent<LandingGear>();
        gearRight = gearObjRight.GetComponent<LandingGear>();

        rb.mass = playerSO.shipWeight;
        torque = playerSO.forceAdded / 10;

        maxDamage = playerSO.maxHps[0];
        playerSO.readyToFly = true;
        playerSO.RepairAll();
        playerSO.Refuel();
    }

    void Update()
    {
        velo = Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2);
        //
        if (playerSO.nowHps[0] <= 0)
        {
            playerSO.nowHps[0] = 0;
            //spawnBroken();
            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = false;
            return;
        }
        //


        if (playerSO.isLanded) // Stop on Ground.
        {
            rb.mass = playerSO.standWeight;
        }
        else if (rb.mass != playerSO.shipWeight)
        {
            float desiredWeight = Mathf.Lerp(rb.mass, playerSO.shipWeight, playerSO.weightSmooth);
            rb.mass = desiredWeight;

        }

        if (velo < 0.0001f) playerSO.isLanded = true;
        else playerSO.isLanded = false;
    }

    void FixedUpdate()
    {
        if(playerSO.nowHps[0] <= 0)
        {
            //GAME OVER
        }

        if (playerSO.nowFuel <= 0)
        {
            playerSO.nowFuel = 0;
            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = false;
            playerSO.readyToFly = false;
        }

        if (playerSO.readyToFly)
        {
            ReceiveControl();
            thrusterWork();
        }else
        {
            gearRight.isOk = false;
            gearLeft.isOk = false;
        }

        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).position.x < Screen.width/2 && Input.GetTouch(0).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * playerSO.forceAdded, rb.position + pos);
        //    }
        //    else if (Input.GetTouch(0).position.x > Screen.width/2 && Input.GetTouch(0).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * playerSO.forceAdded, rb.position + pos2);
        //    }

        //    if (Input.GetTouch(1).position.x < Screen.width/2 && Input.GetTouch(1).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * playerSO.forceAdded, rb.position + pos);
        //    }
        //    else if (Input.GetTouch(1).position.x > Screen.width/2 && Input.GetTouch(1).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * playerSO.forceAdded, rb.position + pos2);
        //    }
        //}

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = col.relativeVelocity.magnitude;
        if (impact < playerSO.minMagnitude) impact = playerSO.minMagnitude;
        if (impact > playerSO.maxMagnitude) impact = playerSO.maxMagnitude;
        float damage = ((impact - playerSO.minMagnitude) / (playerSO.maxMagnitude - playerSO.minMagnitude)) * maxDamage;
        playerSO.nowHps[0] -= damage;
        //foreach (ContactPoint2D contact in col.contacts)  Debug.DrawRay(contact.point, contact.normal, Color.white);
    }

    void ReceiveControl()
    {

        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.M))
        {
            engineLeft.thrusterOn = true;
            engineRight.thrusterOn = true;
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            engineLeft.thrusterOn = true;
            engineRight.thrusterOn = false;
        }
        else if (Input.GetKey(KeyCode.M))
        {
            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = true;
        }
        else
        {
            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = false;
        }

    }

    void thrusterWork()
    {
        if (engineLeft.thrusterOn == true && engineRight.thrusterOn == true)
        {
            rb.AddForce(transform.up * playerSO.forceAdded);
            fuelConsump = playerSO.forceAdded * playerSO.fuelConsumpPerUnit;
            playerSO.nowFuel -= fuelConsump;
        }
        else if (engineLeft.thrusterOn == true && engineRight.thrusterOn == false)
        {
            rb.AddTorque(torque * -1);
            rb.AddForce(transform.up * playerSO.forceAdded / playerSO.calibrateValue);
            fuelConsump = (playerSO.forceAdded / playerSO.calibrateValue) * playerSO.fuelConsumpPerUnit + torque * playerSO.fuelConsumpPerUnit;
            playerSO.nowFuel -= fuelConsump;
        }
        else if (engineLeft.thrusterOn == false && engineRight.thrusterOn == true)
        {
            rb.AddTorque(torque);
            rb.AddForce(transform.up * playerSO.forceAdded / playerSO.calibrateValue);
            fuelConsump = (playerSO.forceAdded / playerSO.calibrateValue) * playerSO.fuelConsumpPerUnit + torque * playerSO.fuelConsumpPerUnit;
            playerSO.nowFuel -= fuelConsump;

        }
        else fuelConsump = 0;

    }
}
