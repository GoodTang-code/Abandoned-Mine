using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCamp : MonoBehaviour
{
    public playerSO player;
    public string campName;
    public int person = 0;
    public GameEvent repairAll;
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

    void Start()
    {
        TransferMenu.SetActive(false);
        btn.SetActive(false);

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
            person += player.nowPassenger;
            player.nowPassenger = 0;
        }
        // Refuel
        if (refuelToggle.isOn)
        {
            player.Refuel();
        }

        // Repair
        if (repairToggle.isOn)
        {
            repairAll.Raise();
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
