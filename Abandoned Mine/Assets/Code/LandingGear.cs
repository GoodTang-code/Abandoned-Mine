using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingGear : MonoBehaviour
{
    public float hp = 100f;
    public Vector2 landingPos;
    public float rayLength = 1.2f;
    public float speed = 0.5f;
    Vector2 flyingPos = new Vector2(0f, 0f);
    //bool gearIsWorking = false;
    //bool gearUp;

    LayerMask layerMask = 1 << 8; //use bit

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);
        Debug.DrawLine(transform.position, hit1.point, Color.blue);
        if (hit1 && LandingGearStatus() != "Land")
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, landingPos, Time.deltaTime * speed);
        }
        if (!hit1 && LandingGearStatus() != "Fly")
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, flyingPos, Time.deltaTime * speed);
        }

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
