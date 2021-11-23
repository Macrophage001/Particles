using Particles.Scripts.ScriptableObjects.Variables;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Bool", fileName = "Bool")]
[System.Serializable]
public class BoolVariable : Variable<bool>
{
    public override bool Equals(bool _value, bool _other)
    {
        return _value == _other;
    }
}
