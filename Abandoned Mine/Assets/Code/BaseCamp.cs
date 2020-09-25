using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCamp : MonoBehaviour
{
    public string campName;
    public int person = 0;
    //public float fuelLeft;
    // Start is called before the first frame update

    public GameObject TransferMenu;
    public GameObject btn;
    public Text campNameBtn; // for btn
    public Text campNameText;
    public Text campPerson;
    public Toggle personToggle;
    public Toggle repairToggle;
    public Toggle refuelToggle;

    Player player;

    void Start()
    {
        TransferMenu.SetActive(false);
        btn.SetActive(false);

        GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Player");
        player = playerObj[0].GetComponent<Player>();

        campNameBtn.text = campName;
        campNameText.text = campName;
        campPerson.text = person.ToString() + " person(s)";

        refuelToggle.isOn = false;
        repairToggle.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.isLanded && !TransferMenu.activeSelf)
        {
            btn.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        TransferMenu.SetActive(false);
        btn.SetActive(false);

    }

    public void UpdateCamp()
    {
        // Send Passenger
        if (personToggle.isOn)
        {
            person = player.passenger;
            player.passenger = 0;
        }
        // Refuel
        if (refuelToggle.isOn)
        {
            player.fuelTank = player.fuelTankMax;
            player.UpdateStatusBar();
        }

        // Repair
        if (repairToggle.isOn)
        {
            player.hp = player.hpMax;
            player.engineLeft.hp = player.engineLeft.hpMax;
            player.engineRight.hp = player.engineRight.hpMax;
            player.gearLeft.hp = player.gearLeft.hpMax;
            player.gearRight.hp = player.gearRight.hpMax;

            player.engineObjLeft.SetActive(true);
            player.engineObjRight.SetActive(true);
            player.gearObjLeft.SetActive(true);
            player.gearObjRight.SetActive(true);

            player.UpdateStatusBar();
        }

        campPerson.text = person.ToString() + " person(s)";

        refuelToggle.isOn = false;
        personToggle.isOn = false;
        repairToggle.isOn = false;

        TransferMenu.SetActive(false);
        btn.SetActive(true);
        player.readyToFly = true;


    }

    public void OpenStationMenu()
    {
        TransferMenu.SetActive(true);
        btn.SetActive(false);
        player.readyToFly = false;


    }
}
