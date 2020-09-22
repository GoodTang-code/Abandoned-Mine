using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    LayerMask layerMask = 1 << 8; //use bit
    public GameObject fire;
    public GameObject dust;
    public float rayLength = 1f;

    // ------------------------------ Component
    public bool thrusterOn = false;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (thrusterOn)
        {
            fire.SetActive(true);
            spawnDust();
        }
        else fire.SetActive(false);
    }

    void spawnDust()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);
        Debug.DrawLine(transform.position, hit1.point, Color.green);
        if (hit1) Instantiate(dust, hit1.point, dust.transform.rotation);
    }
}
