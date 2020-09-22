using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Rigidbody2D player;
    public float smooth = 0.125f; // 0 to 1
    public Vector3 offset;

    public float maxThruster = 10f; // Start from 0, for widest screen
    public float idleCamSize = 4f;
    public float minCamSize = 5f;
    public float maxCamSize = 6.5f;

    float desiredCamSize;
    float velo;

    void FixedUpdate()
    {
        Vector3 desiredPos = new Vector3(player.position.x, player.position.y, -5f) + offset;
        //Vector3 desiredPos = player.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smooth);
        transform.position = smoothedPos;

        //transform.LookAt(player);

        velo = Mathf.Pow(player.velocity.x, 2) + Mathf.Pow(player.velocity.y,2);

        desiredCamSize = ((velo / maxThruster) * (maxCamSize - minCamSize)) + minCamSize;
        float smoothCamSize = Mathf.Lerp(UnityEngine.Camera.main.orthographicSize, desiredCamSize, smooth); 

        UnityEngine.Camera.main.orthographicSize = smoothCamSize;
    }
}
