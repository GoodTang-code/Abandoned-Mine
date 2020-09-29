using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "SO/Effect")]
public class EffectSO : ScriptableObject
{

    public GameObject effect;
    public Vector3 pos;
    public Quaternion rotation;

    public void explodeSpawn()
    {
        Instantiate(effect, pos, rotation);
    }
}
