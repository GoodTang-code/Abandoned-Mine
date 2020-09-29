using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; //Math.Round
    
public class UIStatus : MonoBehaviour
{
    public ProgressBar[] bars;

    public playerSO player;
    public EngineSO[] part;

    void FixedUpdate()
    {
        bars[0].maxHp = player.maxHp;
        bars[0].hp = player.nowHp;

        for (int i = 0; i < part.Length; i++)
        {
            bars[i+1].maxHp = part[i].maxHp;
            bars[i+1].hp = part[i].nowHp;
        }

        bars[5].maxHp = (float)player.maxPassenger;
        bars[5].hp = (float)player.nowPassenger;

        bars[6].maxHp = player.maxFuel;
        bars[6].hp = player.nowFuel;

    }
}
