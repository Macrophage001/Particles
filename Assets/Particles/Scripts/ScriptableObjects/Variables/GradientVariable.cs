using Particles.Scripts.ScriptableObjects.Variables;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Gradient", fileName = "Gradient")]
public class GradientVariable : Variable<Gradient>
{
    public override bool Equals(Gradient _value, Gradient _other)
    {
        return _value.Equals(_other);
    }
}
