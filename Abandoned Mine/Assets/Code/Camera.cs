using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Rigidbody2D player;
    public float smooth = 0.125f; // 0 to 1
    public Vector3 offset;

    public float desiredCamSize;
    public float velo;
    public float maxThruster = 10f;
    public float[] camSize = new float[3];

    void FixedUpdate()
    {
        Vector3 desiredPos = new Vector3(player.position.x, player.position.y, -5f) + offset;
        //Vector3 desiredPos = player.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smooth);
        transform.position = smoothedPos;

        //transform.LookAt(player);

        velo = Mathf.Pow(player.velocity.x, 2) + Mathf.Pow(player.velocity.y,2);
        desiredCamSize = ((velo / maxThruster) * (camSize[2] - camSize[1])) + camSize[1];
        float smoothCamSize = Mathf.Lerp(UnityEngine.Camera.main.orthographicSize, desiredCamSize, smooth); 



        UnityEngine.Camera.main.orthographicSize = smoothCamSize;
    }
}
