using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public int hpIndex;
    public playerSO player;
    public bool isOk = true;
    public bool thrusterOn = false;

    LayerMask layerMask = 1 << 9; //use bit

    public GameObject fire;
    public GameObject dust;

    public float rayLength = 1f;

    // ------------------------------ Component

    public float minMagnitude = 0.4f;
    public float maxMagnitude = 2.2f;
    float maxDamage;

    public bool isBroken = false;

    void Start()
    {
        maxDamage = player.maxHps[hpIndex];
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = col.relativeVelocity.magnitude;
        if (impact < minMagnitude) impact = minMagnitude;
        if (impact > maxMagnitude) impact = maxMagnitude;
        float damage = ((impact - minMagnitude) / (maxMagnitude - minMagnitude)) * maxDamage;
        maxDamage = player.nowHps[hpIndex] -= damage;

        //foreach (ContactPoint2D contact in col.contacts)  Debug.DrawRay(contact.point, contact.normal, Color.white);
    }

    private void Update()
    {
        if (player.nowFuel <= 0 | player.nowHps[hpIndex] <= 0f)
            isOk = false;
    }
    void FixedUpdate()
    {
        if (player.nowHps[hpIndex] <= 0)
        {
            player.nowHps[hpIndex] = 0;
            spawnBroken();
            gameObject.SetActive(false);
            isOk = false;
        }else
        {
            isOk = true;
        }

        if (thrusterOn && isOk)
        {
            fire.SetActive(true);
            spawnDust();
        }
        else fire.SetActive(false);
    }

    void spawnDust()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -transform.up, rayLength, ~layerMask);
        Debug.DrawLine(transform.position, hit1.point, Color.green);
        if (hit1) Instantiate(dust, hit1.point, dust.transform.rotation);
    }

    void spawnBroken()
    {

    }
}
