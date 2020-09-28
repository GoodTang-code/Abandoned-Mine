using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; //Math.Round
    
public class UIStatus : MonoBehaviour
{
    public ProgressBar[] bars;
    public ProgressBar passengerBar;
    public ProgressBar fuelBar;
    //public ProgressBar leftEngineHPBar;
    //public ProgressBar rightEngineHPBar;
    //public ProgressBar leftGearHPBar;
    //public ProgressBar rightGearHPBar;

    ////SO Value
    public playerSO player;
    //public HPSO leftEngineHP;
    //public HPSO rightEngineHP;
    //public HPSO leftGearHP;
    //public HPSO rightGearHP;


    void Start()
    {
    }

    void Update()
    {

        for (int i = 0; i < player.nowHps.Length; i++)
        {
            bars[i].maxHp = player.maxHps[i];
            bars[i].hp = player.nowHps[i];
        }

        passengerBar.maxHp = player.maxPassenger;
        passengerBar.hp = player.nowPassenger;

        fuelBar.maxHp = player.maxFuel;
        fuelBar.hp = player.nowFuel;

    }
}
