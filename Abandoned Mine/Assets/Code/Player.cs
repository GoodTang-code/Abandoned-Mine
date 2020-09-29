using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Math.Round

public class Player : MonoBehaviour
{
    public playerSO playerSO;

    public Rigidbody2D rb;

    public float velo;


    float fuelConsump;

    void Start()
    {

        rb.mass = playerSO.shipWeight;

        playerSO.readyToFly = true;
        playerSO.RepairAll();
        playerSO.Refuel();
    }

    void Update()
    {
        velo = Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2);
        //
        if (playerSO.nowHp<= 0)
        {
            playerSO.nowHp = 0;
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


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = col.relativeVelocity.magnitude;
        if (impact < playerSO.minMagnitude) impact = playerSO.minMagnitude;
        if (impact > playerSO.maxMagnitude) impact = playerSO.maxMagnitude;
        float damage = ((impact - playerSO.minMagnitude) / (playerSO.maxMagnitude - playerSO.minMagnitude)) * playerSO.maxHp;
        playerSO.nowHp -= damage;
        //foreach (ContactPoint2D contact in col.contacts)  Debug.DrawRay(contact.point, contact.normal, Color.white);
    }
}
