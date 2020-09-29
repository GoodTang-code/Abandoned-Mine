using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Engine", menuName = "SO/Engine")]
public class EngineSO : ScriptableObject
{
    public float maxHp;
    public float nowHp;
    public EffectSO explode;
    public float explosionForce;
    public float explosionTorque;

    [Tooltip("Only Engine")]        public bool isReady = true;
    [Tooltip("Both Engine, Gear")]  public bool isBroken = false;
    [Tooltip("Only Engine")]        public bool thrusterOn = false;

    public float minMagnitude = 0.4f;
    public float maxMagnitude = 2.2f;


    public void Repair()
    {
        nowHp = maxHp;
        isBroken = false;
    }
}
