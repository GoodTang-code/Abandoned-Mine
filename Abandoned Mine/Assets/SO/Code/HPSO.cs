using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPSO", menuName = "SO/HP", order = 1)]
public class HPSO : ScriptableObject
{
    public float maxHp;
    public float nowHp;
}
