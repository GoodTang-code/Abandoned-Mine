using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public int hpIndex;
    public playerSO player;
    public EffectSO explode;
    public FloatVariable explosionForce;

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

    void FixedUpdate()
    {
        if (player.nowHps[hpIndex] < 0 && isOk)
        {
            player.nowHps[hpIndex] = 0;
            Explosion();
            isOk = false;
        }
        else if (player.nowHps[hpIndex] > 0)
        {
            isOk = true;
        }

        if (player.nowFuel <= 0)
            isOk = false;


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

    void Explosion()
    {
        float m = hpIndex % 2;
        int rotateDirection = 1;
        if (m > 0) rotateDirection = -1;

        // Addforce
        Collider2D[] cols = Physics2D.OverlapCircleAll((Vector2)transform.position, 30);
        Debug.Log("1");
        foreach (Collider2D nearbyObj in cols)
        {

            Debug.Log("2");
            Rigidbody2D rb = nearbyObj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log(rb);
                rb.AddForce((rb.position - (Vector2)transform.position) * explosionForce.value[0]);
                rb.AddTorque(explosionForce.value[1] * rotateDirection);
            }
        }

        explode.pos = transform.position;
        explode.rotation = transform.rotation;
        explode.explodeSpawn();

    }
}
