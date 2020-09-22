using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class LightTop : MonoBehaviour
{
    public Rigidbody2D playerRb;

    Vector2 direction;
    LayerMask layerMask = 1 << 8; //use bit
    private float degMinus = 90f;

    public float rayLength = 1f;
    public float xVeloLimit = 0.5f; // Limit to change light from vertical to horizontal
    public float smooth = 5.0f; // Camera rotation smooth time

    void FixedUpdate()
    {
        if (playerRb.velocity != new Vector2(0f, 0f))
        {
            if (playerRb.velocity.x > -xVeloLimit && playerRb.velocity.x < xVeloLimit) LightY();
            else LightX();
            //LightX();
        }
        else
        {
            transform.Rotate(0, 0, 20 * Time.deltaTime); //rotates 50 degrees per second around z axis
        }
    }

    void LightX()
    {
        //---------------------------------------------------------------------- Light from Velocity Direction
        //targetPos.x = lightTransform.position.x + playerRb.velocity.x * posMultiply;
        //targetPos.y = lightTransform.position.y + playerRb.velocity.y * posMultiply;

        //---------------------------------------------------------------------- 3 Raycasts
        int sideCal;
        if (playerRb.velocity.x > 0f) sideCal = 1;
        else sideCal = -1;

        RaycastHit2D[] hitArray = new RaycastHit2D[6];
        Vector2 startPos = playerRb.position + new Vector2(0.4f * sideCal, 0f); // Left or Right
        float y = 1f;

        float minDis = 1000000f;
        int targetHit = -1;
        int i = -1;

        for (int n = 0; n <= 5; n++)
        {
            hitArray[n] = Physics2D.Raycast(startPos, new Vector2(1f * sideCal, y), rayLength, layerMask);
            //Debug.DrawRay(startPos, new Vector2(1f * sideCal, y) * rayLength, Color.red);
            y += - 0.5f;

            i++;
            if (hitArray[n].distance < minDis && hitArray[n].distance != 0)
            {
                minDis = hitArray[n].distance;
                targetHit = i;
            }
        }

        if (targetHit != -1)
        {
            direction = hitArray[targetHit].point - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - degMinus;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);

            //Debug.DrawLine(transform.position, hitArray[targetHit].point, Color.green);
        }
        else LightOpposite(sideCal * -1);

    }
    void LightOpposite(int sideCal)
    {
        Vector2 startPos = playerRb.position + new Vector2(0.4f * sideCal, 0f); // Left or Right
        RaycastHit2D[] hitArray = new RaycastHit2D[6];
        float y = 1f;
        for (int n = 0; n <= 2; n++)
        {
            hitArray[n] = Physics2D.Raycast(startPos, new Vector2(1f * sideCal, y), rayLength, layerMask);
            y += -0.5f;
        }

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
            direction = hitArray[targetHit].point - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - degMinus;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);

            //Debug.DrawRay(transform.position, hitArray[targetHit].point, Color.green);
        }
    }
    void LightY()
    {
        float x = -1f;

        int sideCal = -1; //----------------------------------------- Below
        Vector2 startPos = playerRb.position + new Vector2(0.0f, 0.4f * sideCal); // Below
        RaycastHit2D[] hitBelow = new RaycastHit2D[3];
        float minBelow = 1000000f;
        int targetHitBelow = -1;

        for (int i = 0; i <= 2; i++)
        {
            Vector2 stopPos = new Vector2(x, 1f * sideCal);
            hitBelow[i] =   Physics2D.Raycast   (startPos, stopPos, rayLength, layerMask);
            x += 1f;
            //-------- Distance
            if (hitBelow[i].distance < minBelow && hitBelow[i].distance != 0)
            {
                minBelow = hitBelow[i].distance;
                targetHitBelow = i;
            }
        }

        x = -1f;
        sideCal = 1; //----------------------------------------- Above
        startPos = playerRb.position + new Vector2(0.0f, 0.4f * sideCal); // Above
        RaycastHit2D[] hitAbove = new RaycastHit2D[3];

        float   minAbove        = 1000000f;
        int     targetHitAbove  = -1;

        for (int i = 0; i <= 2; i++)
        {
            Vector2 stopPos = new Vector2(x, 1f * sideCal);
            hitAbove[i] =   Physics2D.Raycast   (startPos, stopPos, rayLength, layerMask);
            x += 1f;
            //-------- Distance
            if (hitAbove[i].distance < minAbove && hitAbove[i].distance != 0)
            {
                minAbove = hitAbove[i].distance;
                targetHitAbove = i;
            }
        }

        if (targetHitAbove > -1 && targetHitBelow == -1)
        {
            direction = hitAbove[targetHitAbove].point - new Vector2(transform.position.x, transform.position.y);
        }
        else if (targetHitBelow > -1 && targetHitAbove == -1)
        {
            direction = hitBelow[targetHitBelow].point - new Vector2(transform.position.x, transform.position.y);
        }
        else if (targetHitBelow > -1 && targetHitAbove > -1)
        {
            if(minAbove < minBelow)
                direction = hitAbove[targetHitAbove].point - new Vector2(transform.position.x, transform.position.y);
            else
                direction = hitBelow[targetHitBelow].point - new Vector2(transform.position.x, transform.position.y);
        }
        else { }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - degMinus;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);

    }
}
