using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Math.Round

public class Player : MonoBehaviour
{
    public float hp = 100f;
    public Rigidbody2D rb;
    public float shipWeight = 6000f;
    public float forceAdded = 12000.0f;
    public float fuelTank = 10000f;
    public float fuelConsumpPerUnit = 0.0004f; // unit per fuelConsump
    public float velo;
    public float calibrateValue = 4f; // calibrate from full power to only side power
    public float torque = 1200f; // Torque Power

    public GameObject engineObjLeft;
    public GameObject engineObjRight;
    private Engine engineLeft;
    private Engine engineRight;

    float fuelConsump;

    public float minMagnitude = 0.6f;
    public float maxMagnitude = 2.2f;
    float maxDamage;

    public float standWeight = 20000f;
    public float weightSmooth = 15f;

    void Start()
    {
        engineLeft = engineObjLeft.GetComponent<Engine>();
        engineRight = engineObjRight.GetComponent<Engine>();

        rb.mass = shipWeight;

        maxDamage = hp;
    }

    void Update()
    {
        velo = Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2);
        //
        if (hp <= 0)
        {
            hp = 0;
            //spawnBroken();
            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = false;
            return;
        }
        //
        if (velo == 0f) // Stop on Ground.
        {
            rb.mass = standWeight;
        }
        else if (rb.mass != shipWeight)
        {
            float desiredWeight = Mathf.Lerp(rb.mass, shipWeight, weightSmooth);
            rb.mass = desiredWeight;

        }
    }

    void FixedUpdate()
    {

        if (fuelTank <= 0 | hp <= 0f)
        {
            fuelTank = 0;
            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = false;
            return;
        }

        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.M)){
            engineLeft.thrusterOn = true;
            engineRight.thrusterOn = true;
        }
        else if (Input.GetKey(KeyCode.Z)){
            engineLeft.thrusterOn = true;
            engineRight.thrusterOn = false;
        }
        else if (Input.GetKey(KeyCode.M)){
            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = true;
        }
        else{
            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = false;
        }

        thrusterWork();

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
    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = col.relativeVelocity.magnitude;
        if (impact < minMagnitude) impact = minMagnitude;
        if (impact > maxMagnitude) impact = maxMagnitude;
        float damage = ((impact - minMagnitude) / (maxMagnitude - minMagnitude)) * maxDamage;
        hp -= damage;

        //foreach (ContactPoint2D contact in col.contacts)  Debug.DrawRay(contact.point, contact.normal, Color.white);
    }

    void thrusterWork()
    {
        if (engineLeft.thrusterOn == true && engineRight.thrusterOn == true)
        {
            rb.AddForce(transform.up * forceAdded);
            fuelConsump = forceAdded * fuelConsumpPerUnit;
            fuelTank -= fuelConsump;
        }
        else if (engineLeft.thrusterOn == true && engineRight.thrusterOn == false)
        {
            rb.AddTorque(torque * -1);
            rb.AddForce(transform.up * forceAdded / calibrateValue);
            fuelConsump = (forceAdded / calibrateValue) * fuelConsumpPerUnit + torque * fuelConsumpPerUnit;
            fuelTank -= fuelConsump;
        }
        else if (engineLeft.thrusterOn == false && engineRight.thrusterOn == true)
        {
            rb.AddTorque(torque);
            rb.AddForce(transform.up * forceAdded / calibrateValue);
            fuelConsump = (forceAdded / calibrateValue) * fuelConsumpPerUnit + torque * fuelConsumpPerUnit;
            fuelTank -= fuelConsump;

        }
        else fuelConsump = 0;

    }
}
