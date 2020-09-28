using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "SO/Float", order = 1)]
public class FloatVariable : ScriptableObject
{
    public float value;
}


//public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
//{
//    public float value;

//    [NonSerialized]
//    public float runtimeValue;

//    public void OnAfter
//}