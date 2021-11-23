using Particles.Scripts.ScriptableObjects.Variables;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Color")]
public class ColorVariable : Variable<Color>
{
    public override bool Equals(Color _value, Color _other)
    {
        return _value.Equals(_other);
    }
}
