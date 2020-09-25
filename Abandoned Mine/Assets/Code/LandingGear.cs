using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingGear : MonoBehaviour
{
    public float hpMax = 100f;
    public float hp = 100f;
    public Vector2 landingPos;
    public float rayLength = 1.2f;
    public float speed = 0.5f;
    public bool isOk = true;
    Vector2 flyingPos = new Vector2(0f, 0f);

    LayerMask layerMask = 1 << 9; //use bit
    //private Player player;

    public float minMagnitude = 0.25f;
    public float maxMagnitude = 1.65f;
    float maxDamage;

    public ProgressBar hpBar;

    void Start()
    {
        //GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Player");
        //player = playerObj[0].GetComponent<Player>();

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

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (hp <= 0)
        {
            hp = 0;
            spawnBroken();
            gameObject.SetActive(false); // FIX This !!!
            isOk = false;
        }else
        {
            //if (gameObject.activeSelf == false) gameObject.SetActive(true);
            isOk = true;
        }

        if (!isOk) return;

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

    void spawnBroken()
    {

    }

    Vector2 localPosV2()
    {
        Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        return pos;
    }

}
