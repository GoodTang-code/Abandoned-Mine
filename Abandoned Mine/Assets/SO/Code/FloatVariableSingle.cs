using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FloatVariableSingle")]
public class FloatVariableSingle : ScriptableObject
{
    public float Value;
}

[System.Serializable]
public class FloatReference
{
    public bool useConstant = true;
    public float constantValue;
    public FloatVariableSingle Variable;

    public float Value
    {
        get {
            return useConstant ? constantValue : Variable.Value;
        }

    }

}
