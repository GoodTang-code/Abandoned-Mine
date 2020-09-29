using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingGear : MonoBehaviour
{
    public int hpIndex;
    public playerSO player;
    public EffectSO explode;
    public FloatVariable explosionForce;

    public bool isOk = true;

    public Vector2 landingPos;
    public float rayLength = 1.2f;
    public float speed = 0.5f;

    Vector2 flyingPos = new Vector2(0f, 0f);

    LayerMask layerMask = 1 << 9; //use bit
    //private Player player;

    public float minMagnitude = 0.25f;
    public float maxMagnitude = 1.65f;
    float maxDamage;

    void Start()
    {
        //GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Player");
        //player = playerObj[0].GetComponent<Player>();

        maxDamage = player.maxHps[hpIndex];

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = col.relativeVelocity.magnitude;
        if (impact < minMagnitude) impact = minMagnitude;
        if (impact > maxMagnitude) impact = maxMagnitude;
        float damage = ((impact - minMagnitude) / (maxMagnitude - minMagnitude)) * maxDamage;
        player.nowHps[hpIndex] -= damage;


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (player.nowHps[hpIndex] < 0 && isOk)
        {
            player.nowHps[hpIndex] = 0;
            isOk = false;

            Explosion();


        }
        else if  (player.nowHps[hpIndex] > 0)
        {
            //if (gameObject.activeSelf == false) gameObject.SetActive(true);
            isOk = true;
        }

        if (!isOk) return;

        if (player.isLanded) return;
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -transform.up, rayLength, ~layerMask);
        Debug.DrawLine(transform.position, hit1.point, Color.blue);
        if (hit1 && LandingGearStatus() != "Land") transform.localPosition = Vector2.MoveTowards(transform.localPosition, landingPos, Time.deltaTime * speed);
        if (!hit1 && LandingGearStatus() != "Fly") transform.localPosition = Vector2.MoveTowards(transform.localPosition, flyingPos, Time.deltaTime * speed);

    }

    string LandingGearStatus()
    {
        if (localPosV2() == landingPos) return "Land";
        else if (localPosV2() == flyingPos) return "Fly";
        else return "Working";
    }

    void Explosion()
    {
        float m = hpIndex % 2;
        int rotateDirection = 1;
        if (m > 0) rotateDirection = -1;

        // Addforce
        Collider2D[] cols = Physics2D.OverlapCircleAll((Vector2)transform.position, 5);
        foreach(Collider2D nearbyObj in cols)
        {
            Rigidbody2D rb = nearbyObj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce((rb.position - (Vector2)transform.position) * explosionForce.value[0]);
                rb.AddTorque(explosionForce.value[1] * rotateDirection);
            }
        }
        
        explode.pos = transform.position;
        explode.rotation = transform.rotation;
        explode.explodeSpawn();

    }

    Vector2 localPosV2()
    {
        Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        return pos;
    }

}
