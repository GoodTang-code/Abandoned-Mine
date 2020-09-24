using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Station : MonoBehaviour
{
    public string campName;
    public int person;
    public float fuelLeft;
    // Start is called before the first frame update

    public GameObject TransferMenu;
    public GameObject btn;
    public Text campNameText;
    public Text campNameBtn;
    public Text campPerson;
    public Text campFuel;
    public Toggle personToggle;
    public Toggle fuelToggle;

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
        campFuel.text = fuelLeft.ToString() + " Gallons";

        fuelToggle.isOn = false;
        personToggle.isOn = false;
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
        // change player Passenger
        if (personToggle.isOn)
        {
            int loadPerson = player.cap - player.passenger; // 3
            if (person < loadPerson)
                loadPerson = person;

            person -= loadPerson;
            player.passenger += loadPerson;
        }
        // Changer Player Fuel tank
        if (fuelToggle.isOn)
        {
            float loadFuel = player.fuelTankMax - player.fuelTank; // 3
            if (fuelLeft < loadFuel)
                loadFuel = fuelLeft;

            fuelLeft -= loadFuel;
            player.fuelTank += loadFuel;
            player.fuelBar.current = player.fuelTank;
        }

        campPerson.text = person.ToString() + " person(s)";
        campFuel.text = fuelLeft.ToString() + " Gallons";
        fuelToggle.isOn = false;
        personToggle.isOn = false;

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
