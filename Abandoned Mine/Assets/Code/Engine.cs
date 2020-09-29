using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public EngineSO engine;
    public GameObject fire;
    public GameObject dust;
    public GameEvent EngineBrokenEvent;
    //public GameEvent changeStatus;

    public float rayLength = 1f;

    LayerMask layerMask = 1 << 9; //use bit

    private void Update()
    {
        if (engine.nowHp < 0)
        {
            Explosion();
            engine.isBroken = true;
            engine.nowHp = 0;
            EngineBrokenEvent.Raise();
        }
    }

    void FixedUpdate()
    {

        if (engine.thrusterOn)
        {
            fire.SetActive(true);
            spawnDust();
        }
        else fire.SetActive(false);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = col.relativeVelocity.magnitude;
        if (impact < engine.minMagnitude) impact = engine.minMagnitude;
        if (impact > engine.maxMagnitude) impact = engine.maxMagnitude;
        float damage = ((impact - engine.minMagnitude) / (engine.maxMagnitude - engine.minMagnitude)) * engine.maxHp;
        engine.nowHp -= damage;

        //foreach (ContactPoint2D contact in col.contacts)  Debug.DrawRay(contact.point, contact.normal, Color.white);
    }

    void spawnDust()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -transform.up, rayLength, ~layerMask);
        Debug.DrawLine(transform.position, hit1.point, Color.green);
        if (hit1) Instantiate(dust, hit1.point, dust.transform.rotation);
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
                rb.AddForce((rb.position - (Vector2)transform.position) * engine.explosionForce);
                rb.AddTorque(engine.explosionTorque);
            }
        }

        engine.explode.pos = transform.position;
        engine.explode.rotation = transform.rotation;
        engine.explode.explodeSpawn();

    }
}
