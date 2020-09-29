using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingGear : MonoBehaviour
{
    public EngineSO gear;
    public playerSO player;
    public GameEvent gearBrokenEvent;

    public Vector2 landingPos;
    public float rayLength = 1.2f;
    public float speed = 0.5f;
    bool isBroken = false;

    Vector2 flyingPos = new Vector2(0f, 0f);

    LayerMask layerMask = 1 << 9; //use bit

    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = col.relativeVelocity.magnitude;
        if (impact < gear.minMagnitude) impact = gear.minMagnitude;
        if (impact > gear.maxMagnitude) impact = gear.maxMagnitude;
        float damage = ((impact - gear.minMagnitude) / (gear.maxMagnitude - gear.minMagnitude)) * gear.maxHp;
        gear.nowHp -= damage;
    }

    void FixedUpdate()
    {
        if (gear.nowHp < 0)
        {
            gear.nowHp = 0;
            gear.isBroken = true;
            Explosion();
            transform.localPosition = flyingPos;
            isBroken = true;
            gearBrokenEvent.Raise();
            
        }

        // Auto Landing GEAR
        if (!player.isLanded && player.nowFuel > 0 && !gear.isBroken)
        {
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -transform.up, rayLength, ~layerMask);
            Debug.DrawLine(transform.position, hit1.point, Color.blue);
            if (hit1 && LandingGearStatus() != "Land") transform.localPosition = Vector2.MoveTowards(transform.localPosition, landingPos, Time.deltaTime * speed);
            if (!hit1 && LandingGearStatus() != "Fly") transform.localPosition = Vector2.MoveTowards(transform.localPosition, flyingPos, Time.deltaTime * speed);
        }

        if (isBroken && gear.isBroken == false) JustFixed();
    }

    void JustFixed()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, landingPos, Time.deltaTime * speed);
    }

    void Explosion()
    {
        // Addforce
        Collider2D[] cols = Physics2D.OverlapCircleAll((Vector2)transform.position, 30);
        foreach (Collider2D nearbyObj in cols)
        {
            Rigidbody2D rb = nearbyObj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log(rb);
                rb.AddForce((rb.position - (Vector2)transform.position) * gear.explosionForce);
                rb.AddTorque(gear.explosionTorque);
            }
        }

        gear.explode.pos = transform.position;
        gear.explode.rotation = transform.rotation;
        gear.explode.explodeSpawn();

    }


    string LandingGearStatus()
    {
        if (localPosV2() == landingPos) return "Land";
        else if (localPosV2() == flyingPos) return "Fly";
        else return "Working";
    }

    Vector2 localPosV2()
    {
        Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        return pos;
    }

}
