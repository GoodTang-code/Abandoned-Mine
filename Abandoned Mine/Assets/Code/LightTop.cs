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

    public float rayLength = 1f;
    public float hitupDistance;
    public float hitdownDistance;
    public float xVeloLimit = 0.5f;
    public float smooth = 5.0f;


    //public GameObject playerObj;
    //private Player player;

    //void Start()
    //{
    //    player = playerObj.GetComponent<Player>();
    //}

    void Update()
    {
        if (rb.velocity.x > -xVeloLimit && rb.velocity.x < xVeloLimit) LightY();
        else LightX();
        //LightX();

    }

    void LightX()
    {
        //---------------------------------------------------------------------- Light to Velocity Direction
        //targetPos.x = lightTransform.position.x + rb.velocity.x * posMultiply;
        //targetPos.y = lightTransform.position.y + rb.velocity.y * posMultiply;

        //direction = targetPos - lightTransform.position;

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - degMinus;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);

        //lightPos = lightTransform.position;
        //Debug.DrawRay(lightTransform.position, direction * rayLength, Color.green);

        //---------------------------------------------------------------------- 3 Raycasts
        int sideCal;
        if (rb.velocity.x > 0f) sideCal = 1;
        else sideCal = -1;

        Vector2 startPos = rb.position + new Vector2(0.8f * sideCal, 0f);
        RaycastHit2D hit1 = Physics2D.Raycast(startPos, new Vector2(1f * sideCal, 1f), rayLength);
        Debug.DrawRay(startPos, new Vector2(1f * sideCal, 1f) * rayLength, Color.red);

        RaycastHit2D hit2 = Physics2D.Raycast(startPos, new Vector2(1f * sideCal, 0f), rayLength);
        Debug.DrawRay(startPos, new Vector2(1f * sideCal, 0f) * (rayLength*1.35f), Color.red);

        RaycastHit2D hit3 = Physics2D.Raycast(startPos, new Vector2(1f * sideCal, -1f), rayLength);
        Debug.DrawRay(startPos, new Vector2(1f * sideCal, -1f) * rayLength, Color.red);

        RaycastHit2D[] hitArray = {hit1, hit2, hit3};
        float minDis = 1000000f;
        int targetHit = -1;

        int i = -1;
        foreach (RaycastHit2D hit in hitArray)
        {
            i++;
            if (hit.distance < minDis && hit.distance != 0)
            {
                minDis = hit.distance;
                targetHit = i;
            }
        }
        if (targetHit != -1)
        {
            direction = hitArray[targetHit].point - new Vector2(lightTransform.position.x, lightTransform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - degMinus;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);
        }
        else LightOpposite(sideCal * -1);

        Debug.Log(targetHit);
    }
    void LightOpposite(int sideCal)
    {
        //float sideCal = 1;

        Vector2 startPos = rb.position + new Vector2(0.8f * sideCal, 0f);
        RaycastHit2D hit1 = Physics2D.Raycast(startPos, new Vector2(1f * sideCal, 1f), rayLength);
        Debug.DrawRay(startPos, new Vector2(1f * sideCal, 1f) * rayLength, Color.red);

        RaycastHit2D hit2 = Physics2D.Raycast(startPos, new Vector2(1f * sideCal, 0f), rayLength);
        Debug.DrawRay(startPos, new Vector2(1f * sideCal, 0f) * (rayLength * 1.35f), Color.red);

        RaycastHit2D hit3 = Physics2D.Raycast(startPos, new Vector2(1f * sideCal, -1f), rayLength);
        Debug.DrawRay(startPos, new Vector2(1f * sideCal, -1f) * rayLength, Color.red);

        RaycastHit2D[] hitArray = { hit1, hit2, hit3 };
        float minDis = 1000000f;
        int targetHit = -1;

        int i = -1;
        foreach (RaycastHit2D hit in hitArray)
        {
            i++;
            if (hit.distance < minDis && hit.distance != 0)
            {
                minDis = hit.distance;
                targetHit = i;
            }
        }
        if (targetHit != -1)
        {
            direction = hitArray[targetHit].point - new Vector2(lightTransform.position.x, lightTransform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - degMinus;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);
        }
    }
    void LightY()
    {
        Vector2 startPos = rb.position + new Vector2(0.0f, 0.8f);
        RaycastHit2D hitUp = Physics2D.Raycast(startPos, Vector2.up, rayLength);
        Debug.DrawRay(startPos, Vector2.up * rayLength, Color.red);

        startPos = rb.position + new Vector2(0.0f, -0.8f);
        RaycastHit2D hitDown = Physics2D.Raycast(startPos, Vector2.down, rayLength);
        Debug.DrawRay(startPos, Vector2.down * rayLength, Color.red);

        bool upH = false;
        bool downH = false;

        if (hitUp && hitDown)
        {
            if (hitUp.distance > hitDown.distance) upH = true;
            else downH = true;

        }

        if (hitUp ^ upH)
        {
            Quaternion rotation = Quaternion.AngleAxis(0, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);
            hitupDistance = hitUp.distance;
        }
        else hitupDistance = -1;

        if (hitDown ^ downH)
        {
            Quaternion rotation = Quaternion.AngleAxis(180, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);
            hitdownDistance = hitDown.distance;
        }
        else hitdownDistance = -1;

        if (!hitUp && !hitDown) LightX();
    }
}
