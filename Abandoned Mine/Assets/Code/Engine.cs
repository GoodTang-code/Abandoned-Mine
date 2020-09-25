using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public float hpMax = 100f;
    public float hp = 100f;
    public bool isOk = true;
    LayerMask layerMask = 1 << 9; //use bit
    public GameObject fire;
    public GameObject dust;
    public float rayLength = 1f;

    // ------------------------------ Component
    public bool thrusterOn = false;

    public float minMagnitude = 0.4f;
    public float maxMagnitude = 2.2f;
    float maxDamage;

    public bool isBroken = false;
    //private Player player;
    public ProgressBar hpBar;

    void Start()
    {
        maxDamage = hp;

        //UI
        hpBar.maximum = hp;
        hpBar.current = hp;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = col.relativeVelocity.magnitude;
        if (impact < minMagnitude) impact = minMagnitude;
        if (impact > maxMagnitude) impact = maxMagnitude;
        float damage = ((impact - minMagnitude) / (maxMagnitude - minMagnitude)) * maxDamage;
        hp -= damage;

        //UI
        hpBar.current = hp;

        //foreach (ContactPoint2D contact in col.contacts)  Debug.DrawRay(contact.point, contact.normal, Color.white);
    }

    void FixedUpdate()
    {
        if (hp <= 0)
        {
            hp = 0;
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
