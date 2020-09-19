using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyRaycast : MonoBehaviour
{
    //public Transform objectToMove;
    Vector2 mousePos;
    public Vector2 direction;

    void Update()
    {
        Vector2 nowPos = new Vector2(transform.position.x, transform.position.y);
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        direction = UnityEngine.Camera.main.ScreenToWorldPoint(mousePos) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(nowPos, direction, 100, 1 << 8);

        //Debug.DrawRay(nowPos, direction);
        Debug.DrawLine(transform.position, hit.point, Color.red);

        if (hit)
        {
            Debug.Log("Hit : " + hit.distance);
        }

        //Raycast Til Hit
        //var hit : RaycastHit;
        //var ray = transform.TransformDirection(Vector3.forward);
        //Physics.Raycast(transform.position, ray, hit, 100);
        //Debug.DrawLine(transform.position, hit.point, Color.red);
    }
}
