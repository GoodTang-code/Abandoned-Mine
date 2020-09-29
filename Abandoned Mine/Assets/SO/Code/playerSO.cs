using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "SO/Player")]
public class playerSO : ScriptableObject
{
    public float maxHp;
    public float nowHp;
    public bool isOk;
    public int maxPassenger;
    public int nowPassenger;
    public float maxFuel;
    public float nowFuel;

    public float shipWeight = 6000f;
    public float forceAdded = 12000.0f;
    public bool isLanded = false;
    public bool readyToFly = true;

    public float fuelConsumpPerUnit = 0.0004f; // unit per fuelConsump
    public float calibrateValue = 4f; // calibrate from full power to only side power

    //Start Fly
    public float standWeight = 20000f;
    public float weightSmooth = 15f;

    // Impact
    public float minMagnitude = 0.6f;
    public float maxMagnitude = 2.2f;

    public void RepairAll()
    {
            nowHp = maxHp;
    }

    public void Refuel()
    {
        nowFuel = maxFuel;
    }

}
