using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public playerSO player;
    public Transform playerTransform;
    public GameObject[] otherPart = new GameObject[4];
    public Rigidbody2D playerRigidbody;
    public EngineSO engineLeft;
    public EngineSO engineRight;
    public EngineSO gearLeft;
    public EngineSO gearRight;



    private float torque;

    void Start()
    {

        torque = player.forceAdded / 10;

        //For Testing
        player.Refuel();
        RepairAll();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player Fuel
        if (player.nowFuel <= 0) return;

        switch (KeyboardInput())
        {
            //0 Engine Ready and Broken
            //1 Add Force
            //2 Use Fuel
            case "Both":
                if (engineLeft.isReady && !engineLeft.isBroken && engineRight.isReady && !engineRight.isBroken)
                {
                    engineLeft.thrusterOn = true;
                    engineRight.thrusterOn = true;
                    playerRigidbody.AddForce(playerTransform.up * player.forceAdded);
                    float fuelConsump = player.forceAdded * player.fuelConsumpPerUnit;
                    player.nowFuel -= fuelConsump;
                }
                break;

            case "Left":
                if (engineLeft.isReady && !engineLeft.isBroken)
                {
                    engineLeft.thrusterOn = true;
                    engineRight.thrusterOn = false;
                    playerRigidbody.AddTorque(torque * -1);
                    playerRigidbody.AddForce(playerTransform.up * player.forceAdded / player.calibrateValue);
                    float fuelConsump = (player.forceAdded / player.calibrateValue) * player.fuelConsumpPerUnit + torque * player.fuelConsumpPerUnit;
                    player.nowFuel -= fuelConsump;
                }
                break;

            case "Right":
                if (engineRight.isReady && !engineRight.isBroken)
                {
                    engineLeft.thrusterOn = false;
                    engineRight.thrusterOn = true;
                    playerRigidbody.AddTorque(torque * 1);
                    playerRigidbody.AddForce(playerTransform.up * player.forceAdded / player.calibrateValue);
                    float fuelConsump = (player.forceAdded / player.calibrateValue) * player.fuelConsumpPerUnit + torque * player.fuelConsumpPerUnit;
                    player.nowFuel -= fuelConsump;
                }
                break;

            default:
                if (engineLeft.thrusterOn) engineLeft.thrusterOn = false;
                if (engineRight.thrusterOn) engineRight.thrusterOn = false;
                break;
        }


    }
    string KeyboardInput()
    {
        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.M)) return ("Both");
        else if (Input.GetKey(KeyCode.Z)) return ("Left");
        else if (Input.GetKey(KeyCode.M)) return ("Right");
        else return ("None");
    }

    public void RepairAll()
    {
        player.RepairAll();
        engineLeft.Repair();
        engineRight.Repair();
        gearLeft.Repair();
        gearRight.Repair();
    }
    public void PartChangeStatus()
    {
        if(engineLeft.nowHp > 0) otherPart[0].SetActive(true);
        else otherPart[0].SetActive(false);
        if (engineRight.nowHp > 0) otherPart[1].SetActive(true);
        else otherPart[1].SetActive(false);
        if (gearLeft.nowHp > 0) otherPart[2].SetActive(true);
        else otherPart[2].SetActive(false);
        if (gearRight.nowHp > 0) otherPart[3].SetActive(true);
        else otherPart[3].SetActive(false);
    }

    void TouchInput()
    {

        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).position.x < Screen.width/2 && Input.GetTouch(0).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * player.forceAdded, rb.position + pos);
        //    }
        //    else if (Input.GetTouch(0).position.x > Screen.width/2 && Input.GetTouch(0).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * player.forceAdded, rb.position + pos2);
        //    }

        //    if (Input.GetTouch(1).position.x < Screen.width/2 && Input.GetTouch(1).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * player.forceAdded, rb.position + pos);
        //    }
        //    else if (Input.GetTouch(1).position.x > Screen.width/2 && Input.GetTouch(1).position.y < Screen.height/2)
        //    {
        //        rb.AddForceAtPosition(transform.up * player.forceAdded, rb.position + pos2);
        //    }
        //}
    }
}
