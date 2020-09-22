using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; //Math.Round
    
public class UIStatus : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public GameObject playerObj;
    public Text velo;
    public Text fuelLeft;

    private Player player;

    void Start()
    {
        player = playerObj.GetComponent<Player>();
    }

    void Update()
    {
        velo.text = "velocity x :" + Math.Round(playerRb.velocity.x * 10, 2) + "\n" +
                    "velocity y :" + Math.Round(playerRb.velocity.y * 10, 2) + "\n" +
                    "velocity :";
        fuelLeft.text = "Fuel left : " + Math.Round(player.fuelTank,2);
    }
}
