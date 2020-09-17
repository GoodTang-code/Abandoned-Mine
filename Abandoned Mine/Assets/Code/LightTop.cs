using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTop : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector3 targetPos;
    public Transform lightTransform;
    public Vector2 lightPos;
    public Vector2 direction;
    int posMultiply = 50;
    public float degMinus = 90f;

    void Update()
    {
        targetPos.x = lightTransform.position.x + rb.velocity.x * posMultiply;
        targetPos.y = lightTransform.position.y + rb.velocity.y * posMultiply;

        direction = targetPos - lightTransform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - degMinus;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;



        lightPos = lightTransform.position;
        Debug.DrawRay(lightTransform.position, direction, Color.green);
    }
}
