using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartController : MonoBehaviour
{
    public playerSO player;

    public GameObject gearObjLeft;
    public GameObject gearObjRight;
    private LandingGear gearLeft;
    private LandingGear gearRight;

    void Start()
    {
        gearLeft = gearObjLeft.GetComponent<LandingGear>();
        gearRight = gearObjRight.GetComponent<LandingGear>();

    }

    // Update is called once per frame
    public void UpdateLegStatus()
    {


    }
}
