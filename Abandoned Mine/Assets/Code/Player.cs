using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Math.Round

public class Player : MonoBehaviour
{
    public float hpMax = 100;
    public float hp = 100f;
    public int passenger = 0;
    public int cap = 5; // Passenger Capacity
    public float fuelTank = 10000f;
    public float fuelTankMax = 10000f;
    public float shipWeight = 6000f;
    public float forceAdded = 12000.0f;
    public bool isLanded = false;
    public bool readyToFly = true;

    public Rigidbody2D rb;
    public float fuelConsumpPerUnit = 0.0004f; // unit per fuelConsump
    public float velo;
    public float calibrateValue = 4f; // calibrate from full power to only side power
    float torque; // Torque Power

    public ProgressBar hpBar;
    public ProgressBar fuelBar;
    //public ProgressBar engineLeftBar;
    //public ProgressBar engineRightBar;

    public GameObject engineObjLeft;
    public GameObject engineObjRight;
    public Engine engineLeft;
    public Engine engineRight;


    public GameObject gearObjLeft;
    public GameObject gearObjRight;
    public LandingGear gearLeft;
    public LandingGear gearRight;

    float fuelConsump;

    // Impact
    public float minMagnitude = 0.6f;
    public float maxMagnitude = 2.2f;
    float maxDamage;

    //Start Fly
    public float standWeight = 20000f;
    public float weightSmooth = 15f;

    void Start()
    {
        engineLeft = engineObjLeft.GetComponent<Engine>();
        engineRight = engineObjRight.GetComponent<Engine>();
        gearLeft = gearObjLeft.GetComponent<LandingGear>();
        gearRight = gearObjRight.GetComponent<LandingGear>();

        rb.mass = shipWeight;
        torque = forceAdded / 10;

        maxDamage = hp;

        //UI
        hpBar.maximum = hp;
        hpBar.current = hp;
        fuelBar.maximum = fuelTankMax;
        fuelBar.current = fuelTank;
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

        if (velo < 0.0001f) isLanded = true;
        else isLanded = false;
    }

    void FixedUpdate()
    {

        if (fuelTank <= 0 | hp <= 0f)
        {
            fuelTank = 0;
            engineLeft.thrusterOn = false;
            engineRight.thrusterOn = false;
            readyToFly = false;
        }

        if (readyToFly)
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

        //UI
        UpdateStatusBar();
        //foreach (ContactPoint2D contact in col.contacts)  Debug.DrawRay(contact.point, contact.normal, Color.white);
    }

    public void UpdateStatusBar()
    {
        hpBar.current = hp;
        fuelBar.current = fuelTank;
        engineLeft.hpBar.current = engineLeft.hp;
        engineRight.hpBar.current = engineRight.hp;
        gearLeft.hpBar.current = gearLeft.hp;
        gearRight.hpBar.current = gearRight.hp;

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
            rb.AddForce(transform.up * forceAdded);
            fuelConsump = forceAdded * fuelConsumpPerUnit;
            fuelTank -= fuelConsump;
            fuelBar.current = fuelTank;
        }
        else if (engineLeft.thrusterOn == true && engineRight.thrusterOn == false)
        {
            rb.AddTorque(torque * -1);
            rb.AddForce(transform.up * forceAdded / calibrateValue);
            fuelConsump = (forceAdded / calibrateValue) * fuelConsumpPerUnit + torque * fuelConsumpPerUnit;
            fuelTank -= fuelConsump;
            fuelBar.current = fuelTank;
        }
        else if (engineLeft.thrusterOn == false && engineRight.thrusterOn == true)
        {
            rb.AddTorque(torque);
            rb.AddForce(transform.up * forceAdded / calibrateValue);
            fuelConsump = (forceAdded / calibrateValue) * fuelConsumpPerUnit + torque * fuelConsumpPerUnit;
            fuelTank -= fuelConsump;
            fuelBar.current = fuelTank;

        }
        else fuelConsump = 0;

    }
}
